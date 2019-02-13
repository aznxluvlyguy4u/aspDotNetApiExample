﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using samsung.api.Models;
using samsung_api.Services.Logger;
using System;
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
        private readonly ILinkService _linkService;

        public LinksController(ILogger logger, IMapper mapper, ILinkService linkService)
        {
            _logger = logger;
            _mapper = mapper;
            _linkService = linkService;
        }

        [HttpGet("")]
        public async Task<JsonResponse> GetLinkAsync(string email)
        {
            throw new  NotImplementedException();
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

        [HttpPost]
        public async Task<JsonResponse> CreateLinkAsync(CreateLinkRequest createLinkRequest)
        {
            try
            {
                await _linkService.CreateLinkAsync(createLinkRequest);
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

    }
}