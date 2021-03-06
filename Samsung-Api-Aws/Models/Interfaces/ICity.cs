﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace samsung_api.Models.Interfaces
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public interface ICity
    {
        int Id { get; set; }
        string CountryCode { get; set; }
        string CityName { get; set; }
        string CityAccentName { get; set; }
    }
}