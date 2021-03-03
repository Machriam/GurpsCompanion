using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using GurpsCompanion.Client.ExtensionMethods;
using GurpsCompanion.Shared.Core;
using GurpsCompanion.Shared.Extensions;
using GurpsCompanion.Shared.Features.Authentication;

namespace GurpsCompanion.Client.JsInterop
{
    public enum ContentTypes
    {
        [Description("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
        Spreadsheet,

        [Description("application/vnd.openxmlformats-officedocument.wordprocessingml.document")]
        OfficeDocument,

        [Description("data:text/tsv;charset=utf-8,")]
        TSV
    }

    public interface IJsFunctionCallerService : IDisposable
    {
        IJsClipboardService ClipboardService { get; }

        ValueTask ExportGridAsCsv();

        ValueTask SelectGridRow(int position);

        ValueTask UpdateGrid(string content, params string[] items);

        Task<bool> CheckHttpResponse(HttpResponseMessage message, string potentialReason = "");

        Task<bool> ConfirmPassword(string description = "");

        Task Prompt(string message);

        Task Prompt(List<ValidationResult> messages);

        void Download(string base64string, string fileName, ContentTypes contentType);

        Task<bool> ConfirmDeletion(string message);

        Task<bool> Confirm(string message);

        Task<string> GetUserInput(string message);

        Task<bool> ModelIsValid(object model, bool automaticPrompt = true);
    }

    public class JsFunctionCallerService<T> : IJsFunctionCallerService where T : class
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly string _namespace;
        private string JsInitialize => _namespace + ".initialize";
        private string JsUpdate => _namespace + ".updateData";
        private string JsSelect => _namespace + ".select";
        private string JsCsvExport => _namespace + ".getCsvExport";
        private readonly DotNetObjectReference<T> _objectReference;
        private readonly IPasswordCryptologizer _cryptologizer;
        private readonly IObjectValidator _objectValidator;
        private readonly HttpClient _http;
        private bool _initialized = false;
        public IJsClipboardService ClipboardService { get; }

        public JsFunctionCallerService(string @namespace, IJSRuntime jSRuntime, T objectReference, IPasswordCryptologizer cryptologizer,
            HttpClient http, IJsClipboardService jsClipboardService, IObjectValidator objectValidator)
        {
            _namespace = @namespace;
            _jsRuntime = jSRuntime;
            _objectReference = DotNetObjectReference.Create<T>(objectReference);
            _cryptologizer = cryptologizer;
            _http = http;
            ClipboardService = jsClipboardService;
            _objectValidator = objectValidator;
        }

        public ValueTask UpdateGrid(string content, params string[] items)
        {
            if (_initialized) return _jsRuntime.InvokeVoidAsync(JsUpdate, content, items);
            _initialized = true;
            return _jsRuntime.InvokeVoidAsync(JsInitialize, content, _objectReference, items);
        }

        public ValueTask SelectGridRow(int position)
        {
            return _jsRuntime.InvokeVoidAsync(JsSelect, position);
        }

        public ValueTask ExportGridAsCsv()
        {
            return _jsRuntime.InvokeVoidAsync(JsCsvExport);
        }

        public async Task<bool> CheckHttpResponse(HttpResponseMessage message, string potentialReason = "")
        {
            return await message.CheckIfResponseIsOK(_jsRuntime, potentialReason).ConfigureAwait(false);
        }

        public async Task<bool> ConfirmPassword(string description = "")
        {
            var model = await _http.GetFromJsonAsync<GlobalPasswordHashModel>("api/authentication/globalpasswordhash").ConfigureAwait(false);
            return await _jsRuntime.ConfirmPassword(model, _cryptologizer, description).ConfigureAwait(false);
        }

        public async Task Prompt(string message)
        {
            await _jsRuntime.Prompt(message).ConfigureAwait(false);
        }

        public async Task<bool> Confirm(string message)
        {
            return await _jsRuntime.Confirm(message).ConfigureAwait(false);
        }

        public async Task<bool> ConfirmDeletion(string message)
        {
            return await _jsRuntime.ConfirmDeletion(message).ConfigureAwait(false);
        }

        public void Dispose()
        {
            _objectReference?.Dispose();
        }

        public async void Download(string base64String, string fileName, ContentTypes contentType)
        {
            await _jsRuntime.InvokeAsync<object>("downloadFromByteArray", new
            {
                ByteArray = base64String,
                FileName = fileName,
                ContentType = contentType.GetDescription(),
            }).ConfigureAwait(false);
        }

        public async Task Prompt(List<ValidationResult> messages)
        {
            await Prompt(string.Join("\n", messages.Select(vr => vr.ErrorMessage))).ConfigureAwait(false);
        }

        public async Task<string> GetUserInput(string message)
        {
            return await _jsRuntime.GetUserString(message).ConfigureAwait(false);
        }

        public async Task<bool> ModelIsValid(object model, bool automaticPrompt = true)
        {
            if (!_objectValidator.ModelIsValid(model, out var results))
            {
                await Prompt(results).ConfigureAwait(false);
                return false;
            }
            return true;
        }
    }
}
