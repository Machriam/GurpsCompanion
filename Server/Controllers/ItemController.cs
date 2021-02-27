using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GurpsCompanion.Shared.DataModel;
using GurpsCompanion.Shared.DataModel.DataContext;

namespace GurpsCompanion.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly ILogger<CharacterController> _logger;
        private readonly DataContext _dataContext;

        public ItemController(ILogger<CharacterController> logger, DataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
        }

        [HttpGet("itemnames")]
        public IEnumerable<string> Get()
        {
            return _dataContext.Items.Select(c => c.Name);
        }

        [HttpGet("item")]
        public ItemModel GetItem(string name)
        {
            return new ItemModel(_dataContext.Items.First(i => i.Name == name));
        }
    }
}
