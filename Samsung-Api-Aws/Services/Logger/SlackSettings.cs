using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace samsung_api.Services.Logger
{
    public class SlackSettings : IProvideSlackSettings
    {
        private readonly IConfiguration _config;

        public SlackSettings(IConfiguration configuration)
        {
            _config = configuration;
        }

        public string ConnectionString => _config.GetConnectionString("slackwebhook");

        public LogLevel LogLevel => LogLevel.Debug;

        public string Environement => "development";
    }
}