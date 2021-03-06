﻿using System.Net.Http;
using Microsoft.JSInterop;
using GurpsCompanion.Shared.Core;
using GurpsCompanion.Shared.Features.Authentication;

namespace GurpsCompanion.Client.JsInterop
{
    public interface IJsFunctionCallerServiceFactory
    {
        IJsFunctionCallerService Create<T>(T objectRef) where T : class;
    }

    public class JsFunctionCallerServiceFactory : IJsFunctionCallerServiceFactory
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly IPasswordCryptologizer _cryptologizer;
        private readonly HttpClient _httpClient;
        private readonly IJsClipboardService _clipboardService;
        private readonly IObjectValidator _objectValidator;

        public JsFunctionCallerServiceFactory(IJSRuntime jsRuntime, IPasswordCryptologizer cryptologizer, HttpClient httpClient,
            IJsClipboardService clipboardService, IObjectValidator objectValidator)
        {
            _jsRuntime = jsRuntime;
            _cryptologizer = cryptologizer;
            _httpClient = httpClient;
            _clipboardService = clipboardService;
            _objectValidator = objectValidator;
        }

        public IJsFunctionCallerService Create<T>(T objectRef) where T : class
        {
            return new JsFunctionCallerService<T>(_jsRuntime, objectRef, _cryptologizer, _httpClient, _clipboardService, _objectValidator);
        }
    }
}
