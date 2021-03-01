using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using GurpsCompanion.Shared.DataModel;
using GurpsCompanion.Shared.DataModel.DataContext;

namespace GurpsCompanion.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public ItemController(DataContext dataContext)
        {
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
            var dbItem = GetOrCreateItem(model);
            var existingAssociation = _dataContext.CharacterItemAssociations
                .FirstOrDefault(cia => cia.CharacterFk == characterId && cia.ItemFk == dbItem.Id);
            if (existingAssociation != null)
            {
                existingAssociation.Count++;
            }
            else
            {
                var characterItemAssociation = new CharacterItemAssociation() { CharacterFk = characterId, ItemFk = dbItem.Id, Count = 1 };
                _dataContext.CharacterItemAssociations.Add(characterItemAssociation);
            }
            _dataContext.SaveChanges();
            transaction.Commit();
            return new ItemModel(dbItem);
        }

        private Item GetOrCreateItem(ItemModel model)
        {
            var dbItem = _dataContext.Items.FirstOrDefault(i => i.Name == model.Name);
            if (dbItem == null)
            {
                dbItem = new Item(model);
                _dataContext.Items.Add(dbItem);
                _dataContext.SaveChanges();
            }

            return dbItem;
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
            association.Count--;
            if (association.Count <= 0) _dataContext.CharacterItemAssociations.Remove(association);
            _dataContext.SaveChanges();
        }
    }
}
