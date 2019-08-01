using samsung.api.DataSource.Models;
using System.Collections.Generic;

namespace SamsungApiAws.DataSource.Models
{
    public class City : BaseEntity
    {
        public int Id { get; set; }
        public string CountryCode { get; set; }
        public string CityName { get; set; }
        public string CityAccentName { get; set; }

        public ICollection<GeneralUser> GeneralUsers { get; set; } = new HashSet<GeneralUser>();
    }
}