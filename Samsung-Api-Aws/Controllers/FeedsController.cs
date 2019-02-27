using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using samsung.api.Extensions;
using samsung.api.Models;
using samsung.api.Models.Response;
using samsung_api.Models.Interfaces;
using samsung_api.Services.Logger;
using SamsungApiAws.Services.Feeds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SamsungApiAws.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FeedsController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IFeedsService _feedsService;

        public FeedsController(ILogger logger, IMapper mapper, IFeedsService feedsService)
        {
            _logger = logger;
            _mapper = mapper;
            _feedsService = feedsService;
        }

        [HttpGet("")]
        public async Task<JsonResponse> GetFeedsAsync()
        {
            try
            {
                IEnumerable<ILink> links = await _feedsService.GetFeedsAsync();

                if (links.IsNullOrEmpty()) return new JsonResponse(null, HttpStatusCode.NotFound);

                var response = links.Select(x => _mapper.Map<GetLinkResponse>(x));
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