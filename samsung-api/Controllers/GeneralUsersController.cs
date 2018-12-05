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
using samsung.api.Services.GeneralUsers;
using samsung_api.Models.Interfaces;
using samsung_api.Services.Logger;

namespace samsung_api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GeneralUsersController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IMapper _mapper;
        private readonly IGeneralUsersService _usersService;
        private readonly ILogger _logger;

        public GeneralUsersController(DatabaseContext context, IMapper mapper, IGeneralUsersService usersService, ILogger logger)
        {
            _databaseContext = context;
            _mapper = mapper;
            _usersService = usersService;
            _logger = logger;
        }

        // GET: api/Users
        [HttpGet]
        public string Get()
        {
            var wtf = "fu";

            return wtf;
        }

        // GET: api/Users/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Users
        [HttpPost]
        public JsonResponse Post([FromBody] GeneralUserCreateRequest userCreateRequest)
        {
            try
            {
                var user = _mapper.Map<GeneralUserCreateRequest, IGeneralUser>(userCreateRequest);
                var result = _usersService.CreateGeneralUser(user);
                var response = new GeneralUserCreateResponse(result);

                return new JsonResponse(response, System.Net.HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex.Message, ex).GetAwaiter().GetResult();
           
                return new JsonResponse(ex.Message, System.Net.HttpStatusCode.BadRequest);
            }
        }

        // PUT: api/Users/5
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
