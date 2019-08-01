using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using samsung.api.Models;
using samsung.api.Models.Response;
using samsung.api.Services.TeachingAgeGroups;
using samsung_api.Models.Interfaces;
using samsung_api.Services.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace samsung.api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TeachingAgeGroupsController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly ITeachingAgeGroupsService _teachingAgeGroupsService;

        public TeachingAgeGroupsController(ILogger logger, IMapper mapper, ITeachingAgeGroupsService teachingAgeGroupsService)
        {
            _logger = logger;
            _mapper = mapper;
            _teachingAgeGroupsService = teachingAgeGroupsService;
        }

        /// <summary>
        /// Get a list of all teachingAgeGroups
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<JsonResponse> GetTeachingAgeGroups()
        {
            try
            {
                IEnumerable<ITeachingAgeGroup> teachingAgeGroups = await _teachingAgeGroupsService.GetAllTeachingAgeGroups();

                //if (teachingAgeGroups.Count())
                var response = teachingAgeGroups.Select(x => _mapper.Map<GetTeachingAgeGroupsResponse>(x));

                return new JsonResponse(response, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex.Message, ex).ConfigureAwait(false);
                // TODO: When creating a release, don't send ex.Message in response
                return new JsonResponse(ex.Message, HttpStatusCode.BadRequest);
            }
        }
    }
}