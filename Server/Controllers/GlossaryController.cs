using System.Collections.Generic;
using System.Linq;
using GurpsCompanion.Shared.DataModel;
using GurpsCompanion.Shared.DataModel.DataContext;
using Microsoft.AspNetCore.Mvc;

namespace GurpsCompanion.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GlossaryController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public GlossaryController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet()]
        public IEnumerable<GlossaryModel> Get()
        {
            return _dataContext.Glossaries
                .OrderBy(g => g.Name)
                .Select(g => new GlossaryModel(g));
        }

        [HttpPost]
        public GlossaryModel Post([FromBody] GlossaryModel model)
        {
            var dbEntry = new Glossary(model);
            _dataContext.Glossaries.Add(dbEntry);
            _dataContext.SaveChanges();
            return new GlossaryModel(dbEntry);
        }

        [HttpPut]
        public GlossaryModel Put([FromBody] GlossaryModel model)
        {
            var item = _dataContext.Glossaries.First(g => g.Id == model.Id);
            _dataContext.Entry(item).CurrentValues.SetValues(model);
            _dataContext.SaveChanges();
            return new GlossaryModel(item);
        }

        [HttpDelete]
        public void Delete(long glossaryId)
        {
            var entry = _dataContext.Glossaries.First(g => g.Id == glossaryId);
            _dataContext.Glossaries.Remove(entry);
            _dataContext.SaveChanges();
        }
    }
}
