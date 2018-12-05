using samsung.api.Controllers;
using System.Collections.Generic;

namespace samsung.api.Models.Response
{
    public class GetBuddiesResponse
    {
        public IEnumerable<IBuddy> IncomingRequests { get; set; }
        public IEnumerable<IBuddy> OutGoingRequests { get; set; }
        public IEnumerable<IBuddy> Matched { get; set; }
    }
}