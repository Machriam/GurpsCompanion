using System.Collections.Generic;
using System.Linq;
using GurpsCompanion.Shared.DataModel;
using GurpsCompanion.Shared.DataModel.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
    }
}
