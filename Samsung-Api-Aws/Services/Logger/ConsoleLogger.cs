using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace samsung_api.Services.Logger
{
    public class ConsoleLogger : ILogger
    {
        public async Task LogErrorAsync(string message, Exception exception = null, [CallerMemberName]string callerMember = null)
        {
            var exceptionMessage = exception?.Message;
            var exceptionStacktrace = exception?.StackTrace;

            while ((exception = exception?.InnerException) != null)
            {
                exceptionMessage += Environment.NewLine + exception.Message;
            }

            await Console.WriteErrorAsync(
                callerMember + " " + message
                + Environment.NewLine
                + exceptionMessage
                + Environment.NewLine
                + exceptionStacktrace
            ).ConfigureAwait(false);
        }

        public async Task LogInfoAsync(string message, [CallerMemberName]string callerMember = null)
            => await Console.WriteInfoAsync(callerMember + " " + message).ConfigureAwait(false);

        public async Task LogWarningAsync(string message, [CallerMemberName]string callerMember = null)
            => await Console.WriteWarningAsync(callerMember + " " + message).ConfigureAwait(false);
    }
}