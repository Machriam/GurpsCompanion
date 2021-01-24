using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GurpsCompanion.Shared.DataModel;
using GurpsCompanion.Shared.DataModel.DataContext;
using GurpsCompanion.Shared.FeatureModels;

namespace GurpsCompanion.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ILogger<CharacterController> _logger;
        private readonly DataContext _dataContext;

        public CharacterController(ILogger<CharacterController> logger, DataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
        }

        [HttpGet]
        public IEnumerable<CharacterModel> Get()
        {
            return _dataContext.Characters.Select(c => new CharacterModel(c));
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
    }
}
