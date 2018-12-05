using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using samsung.api.Extensions;
using samsung.api.Models;
using samsung_api.Models.Interfaces;
using samsung_api.Models.Requests;
using samsung_api.Services.Logger;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace samsung.api.Controllers
{
    public enum ContactRequestState
    {
        None,
        Pending,
        Matched
    }

    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactsController : ControllerBase
    {
        private const string StateParameter = "state";
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IContactsService _contactsService;

        public ContactsController(ILogger logger, IMapper mapper, IContactsService contactsService)
        {
            _logger = logger;
            _mapper = mapper;
            _contactsService = contactsService;
        }

        /// <summary>
        /// Send request to user
        /// </summary>
        /// <param name="userContactRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResponse> RequestContactAsync(UserContactRequest userContactRequest)
        {
            try
            {

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
        /// Send request to user
        /// </summary>
        /// <param name="userContactRequest"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<JsonResponse> RequestContactAsync(Guid RequestId, UserContactRequest userContactRequest)
        {
            try
            {

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
                base.HttpContext.TryGetEnumQueryValue(StateParameter, out ContactRequestState state);
                List<IContact> contacts = await _contactsService.GetContactsAsync(base.User, state);

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