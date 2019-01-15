using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using samsung.api.Models;
using samsung.api.Models.Response;
using samsung.api.Services.TeachingSubjects;
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
    public class TeachingSubjectsController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly ITeachingSubjectsService _teachingSubjectsService;

        public TeachingSubjectsController(ILogger logger, IMapper mapper, ITeachingSubjectsService teachingSubjectsService)
        {
            _logger = logger;
            _mapper = mapper;
            _teachingSubjectsService = teachingSubjectsService;
        }

        /// <summary>
        /// Get a list of all teach subjects
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResponse> GetTeachingSubjects()
        {
            try
            {
                IEnumerable<ITeachingSubject> teachingSubjects = await _teachingSubjectsService.GetAllTeachingSubjects();

                //if (teachingSubjects.Count())
                var response = teachingSubjects.Select(x => _mapper.Map<GetTeachingSubjectsResponse>(x));

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