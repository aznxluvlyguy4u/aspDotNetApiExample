using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace samsung_api.Services.Logger
{
    public interface ILogger
    {
        Task LogErrorAsync(string message, Exception exception = null, [CallerMemberName]string callerMember = null);

        Task LogInfoAsync(string message, [CallerMemberName]string callerMember = null);

        Task LogWarningAsync(string message, [CallerMemberName]string callerMember = null);
    }
}