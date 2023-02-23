﻿using System.Collections.Generic;
using System.Linq;
using GurpsCompanion.Shared.DataModel;
using GurpsCompanion.Shared.DataModel.DataContext;
using Microsoft.AspNetCore.Mvc;

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
        public IEnumerable<ItemModel> Get()
        {
            return _dataContext.Items.Select(c => new ItemModel()
            {
                Name = c.Name,
                Id = c.Id,
            });
        }

        [HttpGet("item")]
        public ItemModel GetItem(long id)
        {
            var dbItem = _dataContext.Items.First(i => i.Id == id);
            return new ItemModel(dbItem)
            {
                Image = dbItem.Image
            };
        }

        [HttpPut("equip")]
        public void EquipItem(ItemModel model)
        {
            var existingAssociation = _dataContext.CharacterItemAssociations
                .FirstOrDefault(cia => cia.Id == model.CharacterItemAssId);
            existingAssociation.Equipped = model.Equipped ? 1 : 0;
            _dataContext.SaveChanges();
        }

        [HttpPut("changeCount")]
        public void ChangeCount(ItemModel model)
        {
            var existingAssociation = _dataContext.CharacterItemAssociations
                .FirstOrDefault(cia => cia.Id == model.CharacterItemAssId);
            existingAssociation.Count = model.Count;
            _dataContext.SaveChanges();
        }

        [HttpPost]
        public ItemModel PostItem([FromBody] ItemModel model, long characterId)
        {
            using var transaction = _dataContext.Database.BeginTransaction();
            var dbItem = GetOrCreateItem(model);
            var characterItemAssociation = new CharacterItemAssociation() { CharacterFk = characterId, ItemFk = dbItem.Id, Count = 1, Equipped = 1 };
            _dataContext.CharacterItemAssociations.Add(characterItemAssociation);
            _dataContext.SaveChanges();
            transaction.Commit();
            return new ItemModel(dbItem)
            {
                CharacterItemAssId = characterItemAssociation.Id,
                Count = characterItemAssociation.Count,
            };
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
        public void DeleteItem(long characterAssId)
        {
            var association = _dataContext.CharacterItemAssociations
                .First(cia => cia.Id == characterAssId);
            _dataContext.CharacterItemAssociations.Remove(association);
            _dataContext.SaveChanges();
        }
    }
}
