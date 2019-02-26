using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using samsung.api.Extensions;
using samsung.api.Models;
using samsung.api.Models.Requests;
using samsung.api.Models.Response;
using samsung.api.Services.Links;
using samsung_api.Models.Interfaces;
using samsung_api.Models.Requests;
using samsung_api.Services.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SamsungApiAws.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LinksController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly ILinksService _linksService;

        public LinksController(ILogger logger, IMapper mapper, ILinksService linksService)
        {
            _logger = logger;
            _mapper = mapper;
            _linksService = linksService;
        }

        [HttpGet("")]
        public async Task<JsonResponse> GetMyLinksAsync()
        {
            try
            {
                IEnumerable<ILink> links = await _linksService.GetMyLinksAsync(base.User);

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

        [HttpGet("/api/v1/FavoriteLinks")]
        public async Task<JsonResponse> GetMyFavoriteLinksAsync()
        {
            try
            {
                IEnumerable<ILink> links = await _linksService.GetMyFavoriteLinksAsync(base.User);

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

        [HttpPost()]
        public async Task<JsonResponse> CreateLinkAsync([FromBody]CreateLinkRequest createLinkRequest)
        {
            try
            {
                var toBeCreatedLink = _mapper.Map<CreateLinkRequest, ILink>(createLinkRequest);
                await _linksService.CreateLinkAsync(toBeCreatedLink, base.User);
                return new JsonResponse(null, HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex.Message, ex).ConfigureAwait(false);
                // TODO: When creating a release, don't send ex.Message in response
                return new JsonResponse(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPost("/api/v1/FavoriteLinks")]
        public async Task<JsonResponse> CreateFavoriteLinkAsync([FromBody]CreateFavoriteLinkRequest createFavoriteLinkRequest)
        {
            try
            {
                var toBeCreatedFavoriteLink = _mapper.Map<CreateFavoriteLinkRequest, ILink>(createFavoriteLinkRequest);
                await _linksService.CreateFavoriteLinkAsync(toBeCreatedFavoriteLink, base.User);
                return new JsonResponse(null, HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex.Message, ex).ConfigureAwait(false);
                // TODO: When creating a release, don't send ex.Message in response
                return new JsonResponse(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpDelete("{linkId}")]
        public async Task<JsonResponse> DeleteLinkAsync(int linkId)
        {
            throw new NotImplementedException();
            try
            {
                //var name = await _linkService.GetCityNameById(cityId);
                return new JsonResponse(null, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex.Message, ex).ConfigureAwait(false);
                // TODO: When creating a release, don't send ex.Message in response
                return new JsonResponse(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPost("ImageSearches")]
        [AllowAnonymous]
        public async Task<JsonResponse> FindImagesOnUrl([FromBody]FindImageRequest findImageRequest)
        {
            try
            {
                IEnumerable<GetLinkImageSearchResponse> images = await _linksService.FindImagesByUrl(findImageRequest);

                return new JsonResponse(images, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex.Message, ex);

                return new JsonResponse(ex.Message, HttpStatusCode.BadRequest);
            }
        }
    }
}