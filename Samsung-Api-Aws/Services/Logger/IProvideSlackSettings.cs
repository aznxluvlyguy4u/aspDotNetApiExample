using Microsoft.Extensions.Logging;

namespace samsung_api.Services.Logger
{
    public interface IProvideSlackSettings
    {
        string ConnectionString { get; }
        LogLevel LogLevel { get; }
        string Environement { get; }
    }
}