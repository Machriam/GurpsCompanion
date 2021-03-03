using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using GurpsCompanion.Shared.DataModel;
using GurpsCompanion.Shared.DataModel.DataContext;

namespace GurpsCompanion.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdvantageController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public AdvantageController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet("names")]
        public IEnumerable<string> GetAdvantageNames()
        {
            return _dataContext.Advantages.Select(c => c.Name);
        }

        [HttpGet("advantage")]
        public AdvantageModel GetAdvantage(string name)
        {
            return new AdvantageModel(_dataContext.Advantages.First(i => i.Name == name));
        }

        [HttpPost]
        public AdvantageModel Post([FromBody] AdvantageModel model, long characterId)
        {
            using var transaction = _dataContext.Database.BeginTransaction();
            var dbItem = GetOrCreateAdvantage(model);
            if (_dataContext.CharacterAdvantageAssociations.Any(csa =>
                             csa.CharacterFk == characterId && csa.AdvantageFk == dbItem.Id))
            {
                throw new Exception("Only one Skill of a given type is allowed.");
            }
            var characterAdvantageAssociation = new CharacterAdvantageAssociation()
            { CharacterFk = characterId, AdvantageFk = dbItem.Id, Level = model.Level };

            _dataContext.CharacterAdvantageAssociations.Add(characterAdvantageAssociation);
            _dataContext.SaveChanges();
            transaction.Commit();
            return new AdvantageModel(dbItem) { Level = characterAdvantageAssociation.Level };
        }

        private Advantage GetOrCreateAdvantage(AdvantageModel model)
        {
            var dbItem = _dataContext.Advantages.FirstOrDefault(i => i.Name == model.Name);
            if (dbItem == null)
            {
                dbItem = new Advantage(model);
                _dataContext.Advantages.Add(dbItem);
                _dataContext.SaveChanges();
            }

            return dbItem;
        }

        [HttpPut]
        public AdvantageModel Put([FromBody] AdvantageModel model, long characterId)
        {
            var item = _dataContext.Advantages.First(item => item.Id == model.Id);
            var dbItem = new Advantage(model) { Id = model.Id };
            _dataContext.Entry(item).CurrentValues.SetValues(dbItem);
            _dataContext.CharacterAdvantageAssociations
                .First(caa => caa.AdvantageFk == model.Id && caa.CharacterFk == characterId)
                .Level = model.Level;
            _dataContext.SaveChanges();
            return new AdvantageModel(item);
        }

        [HttpDelete]
        public void Delete(long advantageId, long characterId)
        {
            var association = _dataContext.CharacterAdvantageAssociations
                .First(caa => caa.CharacterFk == characterId && caa.AdvantageFk == advantageId);
            _dataContext.CharacterAdvantageAssociations.Remove(association);
            _dataContext.SaveChanges();
        }
    }
}
