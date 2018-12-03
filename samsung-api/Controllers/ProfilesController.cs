using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using samsung.api.DataSource;
using samsung.api.Models.Requests;

namespace samsung_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;
        public ProfilesController(DatabaseContext context)
        {
            _databaseContext = context;
        }

        // GET: api/Profiles
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Profiles/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Profiles
        [HttpPost]
        public void Post([FromBody] IProfile profile)
        {
            _databaseContext.Profiles.Add
        }

        // PUT: api/Profiles/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
