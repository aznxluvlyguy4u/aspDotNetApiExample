using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamsungApiAws.Models.QueryParameters
{
    public class GetBuddiesQueryParams
    {
        [BindRequired]
        public int state { get; set; }
    }
}
