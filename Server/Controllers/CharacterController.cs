using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GurpsCompanion.Server.Core;
using GurpsCompanion.Shared.DataModel;
using GurpsCompanion.Shared.DataModel.DataContext;
using GurpsCompanion.Shared.FeatureModels;
using GurpsCompanion.Shared.Features.Authentication;

namespace GurpsCompanion.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ILogger<CharacterController> _logger;
        private readonly DataContext _dataContext;
        private readonly IAuthenticationVerifier _authenticationVerifier;

        public CharacterController(ILogger<CharacterController> logger, DataContext dataContext, IAuthenticationVerifier authenticationVerifier)
        {
            _logger = logger;
            _dataContext = dataContext;
            _authenticationVerifier = authenticationVerifier;
        }

        [HttpGet]
        public IEnumerable<CharacterModel> Get(string hash, string salt)
        {
            var authenticated = _authenticationVerifier.AuthenticationIsGranted(new GlobalPasswordHashModel() { Hash = hash, Salt = salt });
            if (authenticated)
            {
                return _dataContext.Characters.Select(c => new CharacterModel(c));
            }
            else
            {
                return _dataContext.Characters.Where(c => c.IsPlayer != 0).Select(c => new CharacterModel(c));
            }
        }

        [HttpGet("characterinformation")]
        public CharacterInformationModel GetCharacterInformationModel(long id)
        {
            return new CharacterInformationModel()
            {
                Advantages = _dataContext.CharacterAdvantageAssociations
                    .Where(caa => caa.CharacterFk == id).Select(caa => new AdvantageModel(caa.AdvantageFkNavigation)),
                Items = _dataContext.CharacterItemAssociations
                    .Where(caa => caa.CharacterFk == id).Select(caa => new ItemModel(caa.ItemFkNavigation)),
                Skills = _dataContext.CharacterSkillAssociations
                    .Where(caa => caa.CharacterFk == id).Select(caa => new SkillModel(caa.SkillFkNavigation)),
                CharacterModel = new CharacterModel(_dataContext.Characters.First(c => c.Id == id))
            };
        }

        [HttpPost]
        public ItemModel PostItem([FromBody] ItemModel model)
        {
            var item = new Item(model);
            _dataContext.Items.Add(item);
            _dataContext.SaveChanges();
            return new ItemModel(item);
        }

        [HttpPut]
        public ItemModel PutItem([FromBody] ItemModel model)
        {
            var item = _dataContext.Items.First(item => item.Id == model.Id);
            _dataContext.Entry(item).CurrentValues.SetValues(model);
            _dataContext.SaveChanges();
            return new ItemModel(item);
        }
    }
}
