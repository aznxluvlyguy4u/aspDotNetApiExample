//using AutoMapper;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using samsung.api.Enumerations;
//using samsung.api.Extensions;
//using samsung.api.Models;
//using samsung.api.Models.Response;
//using samsung.api.Services.Buddies;
//using samsung_api.Models.Requests;
//using samsung_api.Services.Logger;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Threading.Tasks;

//namespace samsung.api.Controllers
//{
//    [Route("api/v1/[controller]")]
//    [ApiController]
//    public class SubjectsController : ControllerBase
//    {
//        private const string StateParameter = "state";
//        private readonly ILogger _logger;
//        private readonly IMapper _mapper;
//        private readonly IBuddiesService _buddiesService;

//        public SubjectsController(ILogger logger, IMapper mapper, IBuddiesService buddiesService)
//        {
//            _logger = logger;
//            _mapper = mapper;
//            _buddiesService = buddiesService;
//        }

//        [HttpPost]
//        public async Task<JsonResponse> SendBuddyRequestAsync(BuddyRequest buddyRequest)
//        {
//            try
//            {
//                await _buddiesService.SendBuddyRequestAsync(base.User, buddyRequest.UserId);
//                return new JsonResponse(null, HttpStatusCode.OK);
//            }
//            catch (Exception ex)
//            {
//                await _logger.LogErrorAsync(ex.Message, ex).ConfigureAwait(false);
//                // TODO: When creating a release, don't send ex.Message in response
//                return new JsonResponse(ex.Message, HttpStatusCode.BadRequest);
//            }
//        }

//        [HttpPut("{requestingBuddy}/{hasAccepted}")]
//        public async Task<JsonResponse> RegisterBuddyResponseAsync(int requestingBuddy, bool hasAccepted)
//        {
//            try
//            {
//                await _buddiesService.RegisterBuddyResponseAsync(base.User, requestingBuddy, hasAccepted);
//                return new JsonResponse(null, HttpStatusCode.Created);
//            }
//            catch (Exception ex)
//            {
//                await _logger.LogErrorAsync(ex.Message, ex).ConfigureAwait(false);
//                // TODO: When creating a release, don't send ex.Message in response
//                return new JsonResponse(ex.Message, HttpStatusCode.BadRequest);
//            }
//        }

//        /// <summary>
//        /// Get MY Buddies
//        /// Get MY Pending requests
//        /// Get THEIR Pending requests to me.
//        /// </summary>
//        /// <returns></returns>
//        [HttpGet]
//        public async Task<JsonResponse> GetBuddies()
//        {
//            try
//            {
//                base.HttpContext.TryGetEnumQueryValue(StateParameter, out BuddyRequestState state);
//                IEnumerable<IBuddy> buddies = await _buddiesService.GetBuddiesAsync(base.User, state);

//                var response = buddies.Select(x => _mapper.Map<GetBuddiesResponse>(buddies));

//                return new JsonResponse(response, HttpStatusCode.OK);
//            }
//            catch (Exception ex)
//            {
//                await _logger.LogErrorAsync(ex.Message, ex).ConfigureAwait(false);
//                // TODO: When creating a release, don't send ex.Message in response
//                return new JsonResponse(ex.Message, HttpStatusCode.BadRequest);
//            }
//        }
//    }
//}