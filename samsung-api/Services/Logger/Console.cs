using System;
using System.Threading.Tasks;

namespace samsung_api.Services.Logger
{
    /// <summary>
    /// Hides system default console implementation,
    /// and overrides it with color indexed defaults for ease of spotting in the console.
    /// </summary>
    public static class Console
    {
        private const ConsoleColor DefaultColor = ConsoleColor.White;
        private const ConsoleColor ErrorColor = ConsoleColor.Red;
        private const ConsoleColor InfoColor = ConsoleColor.Cyan;
        private const ConsoleColor WarningColor = ConsoleColor.Yellow;

        public static void WriteError(string message)
        {
            System.Console.ForegroundColor = ErrorColor;
            System.Console.WriteLine($"{GetNow()}: {message}");
            System.Console.ForegroundColor = DefaultColor;
        }

        public static async Task WriteErrorAsync(string message)
        {
            System.Console.ForegroundColor = ErrorColor;
            await System.Console.Out.WriteLineAsync($"{GetNow()}: {message}").ConfigureAwait(false);
            System.Console.ForegroundColor = DefaultColor;
        }

        public static void WriteInfo(string message)
        {
            System.Console.ForegroundColor = InfoColor;
            System.Console.WriteLine($"{GetNow()}: {message}");
            System.Console.ForegroundColor = DefaultColor;
        }

        public static async Task WriteInfoAsync(string message)
        {
            System.Console.ForegroundColor = InfoColor;
            await System.Console.Out.WriteLineAsync($"{GetNow()}: {message}").ConfigureAwait(false);
            System.Console.ForegroundColor = DefaultColor;
        }

        public static void WriteLine(string message)
        {
            System.Console.ForegroundColor = DefaultColor;
            System.Console.WriteLine($"{GetNow()}: {message}");
        }

        public static async Task WriteLineAsync(string message)
        {
            System.Console.ForegroundColor = DefaultColor;
            await System.Console.Out.WriteLineAsync($"{GetNow()}: {message}").ConfigureAwait(false);
        }

        public static void WriteWarning(string message)
        {
            System.Console.ForegroundColor = WarningColor;
            System.Console.WriteLine($"{GetNow()}: {message}");
            System.Console.ForegroundColor = DefaultColor;
        }

        public static async Task WriteWarningAsync(string message)
        {
            System.Console.ForegroundColor = WarningColor;
            await System.Console.Out.WriteLineAsync($"{GetNow()}: {message}").ConfigureAwait(false);
            System.Console.ForegroundColor = DefaultColor;
        }

        private static string GetNow()
            => $"{DateTime.UtcNow.ToShortDateString()} {DateTime.UtcNow.ToShortTimeString()}";
    }
}