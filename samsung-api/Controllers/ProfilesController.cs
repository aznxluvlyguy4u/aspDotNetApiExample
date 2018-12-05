using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using samsung.api.DataSource;
using samsung.api.Models;
using samsung.api.Models.Requests;
using samsung.api.Models.Response;
using samsung.api.Services.Profiles;
using samsung_api.Extensions;
using samsung_api.Models.Interfaces;

namespace samsung_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IMapper _mapper;
        private readonly IProfilesService _profilesService;

        public ProfilesController(DatabaseContext context, IMapper mapper, IProfilesService profilesService)
        {
            _databaseContext = context;
            _mapper = mapper;
            _profilesService = profilesService;
        }

        // GET: api/Profiles
        [HttpGet]
        public string Get()
        {
            var wtf = "fu".ToJson();

            return wtf;
        }

        // GET: api/Profiles/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Profiles
        [HttpPost]
        public JsonResponse Post([FromBody] ProfileCreateRequest profileCreateRequest)
        {
            try
            {
                var profile = _mapper.Map<ProfileCreateRequest, IProfile>(profileCreateRequest);
                var result = _profilesService.CreateProfile(profile);
                var response = new ProfileCreateResponse(result);

                return new JsonResponse(response, System.Net.HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return new JsonResponse(ex.Message, System.Net.HttpStatusCode.BadRequest);
            }
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
