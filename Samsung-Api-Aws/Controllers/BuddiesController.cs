﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using samsung.api.Enumerations;
using samsung.api.Extensions;
using samsung.api.Models;
using samsung.api.Models.Response;
using samsung.api.Services.Buddies;
using samsung_api.Models.Interfaces;
using samsung_api.Models.Requests;
using samsung_api.Services.Logger;
using SamsungApiAws.Models.QueryParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace samsung.api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BuddiesController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IBuddiesService _buddiesService;

        public BuddiesController(ILogger logger, IMapper mapper, IBuddiesService buddiesService)
        {
            _logger = logger;
            _mapper = mapper;
            _buddiesService = buddiesService;
        }

        /// <summary>
        /// Create BuddyRequest from currently logged in user to another user
        /// </summary>
        /// <returns></returns>
        [HttpPost("/api/v1/BuddyRequests")]
        public async Task<JsonResponse> SendBuddyRequestAsync(BuddyRequest buddyRequest)
        {
            try
            {
                await _buddiesService.SendBuddyRequestAsync(base.User, buddyRequest.GeneralUserId);
                return new JsonResponse(null, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex.Message, ex).ConfigureAwait(false);
                // TODO: When creating a release, don't send ex.Message in response
                return new JsonResponse(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        /// <summary>
        /// Get pending BuddyRequests to me
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/v1/BuddyRequests")]
        public async Task<JsonResponse> GetMyBuddyRequests()
        {
            try
            {
                IEnumerable<IGeneralUser> buddies = await _buddiesService.GetMyBuddyRequestsAsync(base.User);

                var response = buddies.Select(x => _mapper.Map<GetGeneralUserResponse>(x));
                return new JsonResponse(response, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex.Message, ex).ConfigureAwait(false);
                // TODO: When creating a release, don't send ex.Message in response
                return new JsonResponse(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        //[HttpPut("{requestingBuddy}/{hasAccepted}")]
        //public async Task<JsonResponse> RegisterBuddyResponseAsync(int requestingBuddy, bool hasAccepted)
        //{
        //    try
        //    {
        //        await _buddiesService.RegisterBuddyResponseAsync(base.User, requestingBuddy, hasAccepted);
        //        return new JsonResponse(null, HttpStatusCode.Created);
        //    }
        //    catch (Exception ex)
        //    {
        //        await _logger.LogErrorAsync(ex.Message, ex).ConfigureAwait(false);
        //        // TODO: When creating a release, don't send ex.Message in response
        //        return new JsonResponse(ex.Message, HttpStatusCode.BadRequest);
        //    }
        //}

        /// <summary>
        /// Get MY matched Buddies
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResponse> GetMatchedBuddies()
        {
            try
            {
                IEnumerable<IGeneralUser> buddies = await _buddiesService.GetMyBuddiesAsync(base.User);

                var response = buddies.Select(x => _mapper.Map<GetGeneralUserResponse>(x));
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