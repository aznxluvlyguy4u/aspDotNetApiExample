using Microsoft.AspNetCore.Mvc.ModelBinding;
using samsung.api.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SamsungApiAws.Models.QueryParameters
{
    public class GetBuddiesQueryParams
    {
        public BuddyRequestState State { get; set; }
    }
}
