using samsung_api.Models.Interfaces;
using System.Collections.Generic;

namespace samsung.api.Models.Response
{
    public class GetBuddiesResponse
    {
        public IEnumerable<IBuddy> incomingRequests { get; set; }
        public IEnumerable<IBuddy> outGoingRequests { get; set; }
        public IEnumerable<IBuddy> matched { get; set; }
    }
}