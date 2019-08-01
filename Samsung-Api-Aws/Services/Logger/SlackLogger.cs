using Microsoft.Extensions.Logging;
using samsung_api.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using static samsung_api.Enumerations.Enums;

namespace samsung_api.Services.Logger
{
    public class SlackLogger : ILogger
    {
        private readonly IProvideSlackSettings _slackSettings;

        public SlackLogger(IProvideSlackSettings slackSettings)
        {
            _slackSettings = slackSettings;
        }

        public async Task LogErrorAsync(string message, Exception exception = null,
            [CallerMemberName] string callerMember = null)
        {
            var exceptionMessage = exception?.Message;
            var exceptionStacktrace = exception?.StackTrace;

            while ((exception = exception?.InnerException) != null)
                exceptionMessage += Environment.NewLine + exception.Message;

            var payload = new SlackPayload
            {
                Username = _slackSettings.Environement + "-bot",
                Attachments = new List<Attachment>
                {
                    new Attachment
                    {
                        Color = nameof(SlackColorCode.Danger).ToLower(CultureInfo.InvariantCulture),
                        Title = $"{callerMember}-{LogLevel.Error}",
                        Text = message ?? "An Exception Occured",
                        Fields = new List<Field>
                        {
                            new Field("Exception Message", exceptionMessage, false),
                            new Field("Stacktrace", exceptionStacktrace, false)
                        }
                    }
                }
            };
            await PostAsync(payload).ConfigureAwait(false);
        }

        public async Task LogInfoAsync(string message, [CallerMemberName] string callerMember = null)
        {
            if (string.IsNullOrWhiteSpace(message))
                return;

            var payload = new SlackPayload
            {
                Username = _slackSettings.Environement + "-bot",
                Attachments = new List<Attachment>
                {
                    new Attachment
                    {
                        Color = nameof(SlackColorCode.Info).ToLower(),
                        Title = $"{callerMember}-{LogLevel.Information}",
                        Text = message
                    }
                }
            };
            await PostAsync(payload).ConfigureAwait(false);
        }

        public async Task LogWarningAsync(string message, [CallerMemberName] string callerMember = null)
        {
            if (string.IsNullOrWhiteSpace(message))
                return;

            var payload = new SlackPayload
            {
                Username = _slackSettings.Environement + "-bot",
                Attachments = new List<Attachment>
                {
                    new Attachment
                    {
                        Color = nameof(SlackColorCode.Warning).ToLower(),
                        Title = $"{callerMember}-{LogLevel.Warning}",
                        Text = message
                    }
                }
            };
            await PostAsync(payload).ConfigureAwait(false);
        }

        private async Task PostAsync(SlackPayload payload)
        {
            var client = new WebClient();

            try
            {
                var data = new NameValueCollection
                {
                    ["payload"] = payload.ToJson()
                };

                client.UploadValues(
                    new Uri(_slackSettings.ConnectionString),
                    "POST",
                    data
                );
                await Task.CompletedTask;
            }
            finally
            {
                client?.Dispose();
            }
        }
    }
}