using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using samsung.api.Extensions;
using samsung.api.Models;
using samsung.api.Models.Response;
using samsung_api.Models.Interfaces;
using samsung_api.Models.Requests;
using samsung_api.Services.Logger;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace samsung.api.Controllers
{
    public enum BuddyRequestState
    {
        None,
        Pending,
        Matched
    }

    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class BuddyController : ControllerBase
    {
        private const string StateParameter = "state";
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IBuddyService _contactsService;

        public BuddyController(ILogger logger, IMapper mapper, IBuddyService contactsService)
        {
            _logger = logger;
            _mapper = mapper;
            _contactsService = contactsService;
        }

        [HttpPost]
        public async Task<JsonResponse> SendBuddyRequestAsync(BuddyRequest buddyRequest)
        {
            try
            {
                await _contactsService.SendBuddyRequestAsync(base.User, buddyRequest.UserId);
                return new JsonResponse(null, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex.Message, ex).ConfigureAwait(false);
                // TODO: When creating a release, don't send ex.Message in response
                return new JsonResponse(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPut("{requestingBuddy}/{hasAccepted}")]
        public async Task<JsonResponse> RegisterBuddyResponseAsync(int requestingBuddy, bool hasAccepted)
        {
            try
            {
                await _contactsService.RegisterBuddyResponseAsync(base.User, requestingBuddy, hasAccepted);
                return new JsonResponse(null, HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex.Message, ex).ConfigureAwait(false);
                // TODO: When creating a release, don't send ex.Message in response
                return new JsonResponse(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        /// <summary>
        /// Get MY Buddies
        /// Get MY Pending requests
        /// Get THEIR Pending requests to me.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResponse> GetContacts()
        {
            try
            {
                base.HttpContext.TryGetEnumQueryValue(StateParameter, out BuddyRequestState state);
                List<IBuddy> contacts = await _contactsService.GetContactsAsync(base.User, state);

                var response = new GetBuddiesResponse
                {
                };

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