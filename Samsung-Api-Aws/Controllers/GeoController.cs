using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using samsung.api.Models;
using samsung_api.Services.Logger;
using SamsungApiAws.Services.Geo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SamsungApiAws.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GeoController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IGeoService _geoService;

        public GeoController(ILogger logger, IMapper mapper, IGeoService geoService)
        {
            _logger = logger;
            _mapper = mapper;
            _geoService = geoService;
        }

        [HttpGet("/{countryCode}")]
        public async Task<JsonResponse> GetCitiesAsync(string countryCode)
        {
            try
            {
                if (countryCode.ToUpperInvariant() != "NL" && countryCode.ToUpperInvariant() != "BE")
                    return new JsonResponse($"Country {countryCode} not supported", HttpStatusCode.BadRequest);

                var cities = await _geoService.GetCountryCitiesAsync(countryCode.ToLowerInvariant());
                return new JsonResponse(cities, HttpStatusCode.OK);
            }
            catch(Exception ex)
            {
                await _logger.LogErrorAsync(ex.Message, ex).ConfigureAwait(false);
                // TODO: When creating a release, don't send ex.Message in response
                return new JsonResponse(ex.Message, HttpStatusCode.BadRequest);
            }
        }
    }
}
