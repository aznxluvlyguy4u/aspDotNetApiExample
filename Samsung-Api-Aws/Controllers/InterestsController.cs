using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using samsung.api.Models;
using samsung.api.Models.Response;
using samsung.api.Services.Interests;
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
    public class InterestsController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IInterestsService _interestsService;

        public InterestsController(ILogger logger, IMapper mapper, IInterestsService interestsService)
        {
            _logger = logger;
            _mapper = mapper;
            _interestsService = interestsService;
        }

        /// <summary>
        /// Get a list of all interests
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResponse> GetInterests()
        {
            try
            {
                IEnumerable<IInterest> interests = await _interestsService.GetAllInterests();

                //if (teachingSubjects.Count())
                var response = interests.Select(x => _mapper.Map<GetInterestsResponse>(x));

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