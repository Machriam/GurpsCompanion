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

        [HttpPost]
        public ItemModel PostItem([FromBody] ItemModel model, long characterId)
        {
            using var transaction = _dataContext.Database.BeginTransaction();
            var item = new Item(model);
            _dataContext.Items.Add(item);
            _dataContext.SaveChanges();
            var characterItemAssociation = new CharacterItemAssociation() { CharacterFk = characterId, ItemFk = item.Id };
            _dataContext.CharacterItemAssociations.Add(characterItemAssociation);
            _dataContext.SaveChanges();
            transaction.Commit();
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

        [HttpDelete]
        public void DeleteItem(long itemId, long characterId)
        {
            var association = _dataContext.CharacterItemAssociations
                .First(cia => cia.CharacterFk == characterId && cia.ItemFk == itemId);
            _dataContext.CharacterItemAssociations.Remove(association);
            _dataContext.SaveChanges();
        }
    }
}
