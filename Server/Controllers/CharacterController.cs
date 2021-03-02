using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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
        private readonly DataContext _dataContext;
        private readonly IAuthenticationVerifier _authenticationVerifier;

        public CharacterController(DataContext dataContext, IAuthenticationVerifier authenticationVerifier)
        {
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
                    .Where(caa => caa.CharacterFk == id)
                    .Select(caa => new AdvantageModel(caa.AdvantageFkNavigation) { Level = caa.Level })
                    .AsEnumerable()
                    .OrderBy(a => a.Name),
                Items = _dataContext.CharacterItemAssociations
                    .Where(caa => caa.CharacterFk == id)
                    .Select(caa => new ItemModel(caa.ItemFkNavigation) { Equipped = caa.Equipped != 0, Count = caa.Count, CharacterItemAssId = caa.Id })
                    .AsEnumerable()
                    .OrderBy(i => i.Name),
                Skills = _dataContext.CharacterSkillAssociations
                    .Where(caa => caa.CharacterFk == id)
                    .Select(caa => new SkillModel(caa.SkillFkNavigation) { Value = caa.Value })
                    .AsEnumerable()
                    .OrderBy(s => s.Name),
                CharacterModel = new CharacterModel(_dataContext.Characters.First(c => c.Id == id))
            };
        }
    }
}
