using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamsungApiAws.DataSource.Models
{

    public class Cities
    {
        public int Id { get; set; }
        public string CountryCode { get; set; }
        public string CityName { get; set; }
        public string CityAccentName { get; set; }
    }
}
