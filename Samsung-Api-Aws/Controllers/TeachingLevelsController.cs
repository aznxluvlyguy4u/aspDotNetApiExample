using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using samsung.api.Models;
using samsung.api.Models.Response;
using samsung.api.Services.TeachingLevels;
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
    public class TeachingLevelsController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly ITeachingLevelsService _teachingLevelsService;

        public TeachingLevelsController(ILogger logger, IMapper mapper, ITeachingLevelsService teachingLevelsService)
        {
            _logger = logger;
            _mapper = mapper;
            _teachingLevelsService = teachingLevelsService;
        }

        /// <summary>
        /// Get a list of all teachingLevels
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<JsonResponse> GetTeachingLevels()
        {
            try
            {
                IEnumerable<ITeachingLevel> teachingLevels = await _teachingLevelsService.GetAllTeachingLevels();

                //if (teachingLevels.Count())
                var response = teachingLevels.Select(x => _mapper.Map<GetTeachingLevelsResponse>(x));

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