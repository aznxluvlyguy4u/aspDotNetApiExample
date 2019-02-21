﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace samsung.api.Models.Requests
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class FindImageRequest
    {
        public string Url { get; set; }
    }
}