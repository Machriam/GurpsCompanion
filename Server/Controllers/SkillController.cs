using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using GurpsCompanion.Shared.DataModel;
using GurpsCompanion.Shared.DataModel.DataContext;

namespace GurpsCompanion.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public SkillController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet("names")]
        public IEnumerable<string> Get()
        {
            return _dataContext.Skills.Select(c => c.Name);
        }

        [HttpGet("skill")]
        public SkillModel GetItem(string name)
        {
            return new SkillModel(_dataContext.Skills.First(i => i.Name == name));
        }

        [HttpPost]
        public SkillModel PostItem([FromBody] SkillModel model, long characterId)
        {
            using var transaction = _dataContext.Database.BeginTransaction();
            var dbItem = GetOrCreateSkill(model);
            var characterSkillAssociation = new CharacterSkillAssociation() { CharacterFk = characterId, SkillFk = dbItem.Id };
            _dataContext.CharacterSkillAssociations.Add(characterSkillAssociation);
            _dataContext.SaveChanges();
            transaction.Commit();
            return new SkillModel(dbItem);
        }

        private Skill GetOrCreateSkill(SkillModel model)
        {
            var dbItem = _dataContext.Skills.FirstOrDefault(i => i.Name == model.Name);
            if (dbItem == null)
            {
                dbItem = new Skill(model);
                _dataContext.Skills.Add(dbItem);
                _dataContext.SaveChanges();
            }

            return dbItem;
        }

        [HttpPut]
        public SkillModel PutItem([FromBody] SkillModel model)
        {
            var item = _dataContext.Skills.First(item => item.Id == model.Id);
            _dataContext.Entry(item).CurrentValues.SetValues(model);
            _dataContext.SaveChanges();
            return new SkillModel(item);
        }

        [HttpDelete]
        public void DeleteItem(long skillId, long characterId)
        {
            var association = _dataContext.CharacterSkillAssociations
                .First(cia => cia.CharacterFk == characterId && cia.SkillFk == skillId);
            _dataContext.CharacterSkillAssociations.Remove(association);
            _dataContext.SaveChanges();
        }
    }
}
