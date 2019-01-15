using Newtonsoft.Json;

namespace samsung_api.Services.Logger
{
    public class Field
    {
        public Field()
        {
        }

        // @ tells the compiler to ignore the fact that the short variable is a reserved keyword here.
        public Field(string title, string value, bool @short)
        {
            Title = title;
            Value = value;
            Short = @short;
        }

        [JsonProperty("title")] public string Title { set; get; }

        [JsonProperty("value")] public string Value { set; get; }

        [JsonProperty("short")] public bool Short { set; get; }
    }
}