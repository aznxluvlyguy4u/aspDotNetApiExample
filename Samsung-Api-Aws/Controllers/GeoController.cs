using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using samsung.api.Models;
using samsung_api.Services.Logger;
using SamsungApiAws.Services.Geo;
using System;
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

        [HttpGet("City/{cityId}")]
        public async Task<JsonResponse> GetCityAsync(int cityId)
        {
            try
            {
                var name = await _geoService.GetCityNameById(cityId);
                return new JsonResponse(name, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex.Message, ex).ConfigureAwait(false);
                // TODO: When creating a release, don't send ex.Message in response
                return new JsonResponse(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpGet("Country/{countryCode}")]
        [AllowAnonymous]
        public async Task<JsonResponse> GetCitiesAsync(string countryCode)
        {
            try
            {
                if (
                    !string.Equals(countryCode, "NL", StringComparison.InvariantCultureIgnoreCase)
                    && !string.Equals(countryCode, "BE", StringComparison.InvariantCultureIgnoreCase)
                )
                {
                    return new JsonResponse($"Country {countryCode} not supported", HttpStatusCode.BadRequest);
                }

                //if (searchText.Length < 3)
                //    return new JsonResponse($"At least 3 characters required for city search", HttpStatusCode.BadRequest);

                var searchText = "";

                var cities = await _geoService.GetCountryCitiesAsync(countryCode.ToLowerInvariant(), searchText);
                var response = cities.Select(x => new { name = x.Value, id = x.Key });
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