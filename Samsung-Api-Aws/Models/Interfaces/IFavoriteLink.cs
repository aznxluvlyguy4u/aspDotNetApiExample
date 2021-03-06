﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace samsung_api.Models.Interfaces
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public interface IFavoriteLink
    {
        ILink Link { get; set; }
        IGeneralUser GeneralUser { get; set; }
    }
}