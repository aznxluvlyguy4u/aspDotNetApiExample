﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using samsung.api.Enumerations;

namespace samsung_api.Models.Interfaces
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public interface IBuddu
    {
        BuddyRequestState RequestState { get; set; }
        IGeneralUser GeneralUser { get; set; }
    }
}