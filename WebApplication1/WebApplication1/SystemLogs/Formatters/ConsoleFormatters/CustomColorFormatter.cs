using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;

namespace WebApplication1.SystemLogs.Formatters.ConsoleFormatters
{
    /// <summary>
    /// 自定义颜色配置
    /// </summary>
    public class CustomColorOptions : SimpleConsoleFormatterOptions
    {
        public string? CustomPrefix { get; set; }
    }

    /// <summary>
    /// 修改日志输出颜色
    /// 日志需要存在 <see cref="Exception"/>  信息
    /// 自定义控制台日志格式工具
    /// </summary>
    public sealed class CustomColorFormatter : ConsoleFormatter, IDisposable
    {
        private readonly IDisposable? _optionsReloadToken;
        private CustomColorOptions _formatterOptions;

        private bool ConsoleColorFormattingEnabled =>
            _formatterOptions.ColorBehavior == LoggerColorBehavior.Enabled ||
            _formatterOptions.ColorBehavior == LoggerColorBehavior.Default &&
            Console.IsOutputRedirected == false;

        public CustomColorFormatter(IOptionsMonitor<CustomColorOptions> options)
            // Case insensitive
            : base(nameof(CustomColorFormatter)) =>
            (_optionsReloadToken, _formatterOptions) =
                (options.OnChange(ReloadLoggerOptions), options.CurrentValue);

        private void ReloadLoggerOptions(CustomColorOptions options) =>
            _formatterOptions = options;

        public override void Write<TState>(
            in LogEntry<TState> logEntry,
            IExternalScopeProvider? scopeProvider,
            TextWriter textWriter)
        {
            if (logEntry.Exception is null)
            {
                return;
            }

            string? message =
                logEntry.Formatter?.Invoke(
                    logEntry.State, logEntry.Exception);

            if (message is null)
            {
                return;
            }

            CustomLogicGoesHere(textWriter);
            textWriter.WriteLine(message);
        }

        private void CustomLogicGoesHere(TextWriter textWriter)
        {
            if (ConsoleColorFormattingEnabled)
            {
                textWriter.WriteWithColor(
                    _formatterOptions.CustomPrefix ?? string.Empty,
                    ConsoleColor.Black,
                    ConsoleColor.Green);
            }
            else
            {
                textWriter.Write(_formatterOptions.CustomPrefix);
            }
        }

        public void Dispose() => _optionsReloadToken?.Dispose();
    }

    public static class TextWriterExtensions
    {
        const string DefaultForegroundColor = "\x1B[39m\x1B[22m";
        const string DefaultBackgroundColor = "\x1B[49m";

        /// <summary>
        /// 使用指定颜色输出
        /// </summary>
        /// <param name="textWriter"></param>
        /// <param name="message"></param>
        /// <param name="background"></param>
        /// <param name="foreground"></param>
        public static void WriteWithColor(
            this TextWriter textWriter,
            string message,
            ConsoleColor? background,
            ConsoleColor? foreground)
        {
            // Order:
            //   1. background color
            //   2. foreground color
            //   3. message
            //   4. reset foreground color
            //   5. reset background color

            var backgroundColor = background.HasValue ? GetBackgroundColorEscapeCode(background.Value) : null;
            var foregroundColor = foreground.HasValue ? GetForegroundColorEscapeCode(foreground.Value) : null;

            if (backgroundColor != null)
            {
                textWriter.Write(backgroundColor);
            }
            if (foregroundColor != null)
            {
                textWriter.Write(foregroundColor);
            }

            textWriter.WriteLine(message);

            if (foregroundColor != null)
            {
                textWriter.Write(DefaultForegroundColor);
            }
            if (backgroundColor != null)
            {
                textWriter.Write(DefaultBackgroundColor);
            }
        }

        static string GetForegroundColorEscapeCode(ConsoleColor color) =>
            color switch
            {
                ConsoleColor.Black => "\x1B[30m",
                ConsoleColor.DarkRed => "\x1B[31m",
                ConsoleColor.DarkGreen => "\x1B[32m",
                ConsoleColor.DarkYellow => "\x1B[33m",
                ConsoleColor.DarkBlue => "\x1B[34m",
                ConsoleColor.DarkMagenta => "\x1B[35m",
                ConsoleColor.DarkCyan => "\x1B[36m",
                ConsoleColor.Gray => "\x1B[37m",
                ConsoleColor.Red => "\x1B[1m\x1B[31m",
                ConsoleColor.Green => "\x1B[1m\x1B[32m",
                ConsoleColor.Yellow => "\x1B[1m\x1B[33m",
                ConsoleColor.Blue => "\x1B[1m\x1B[34m",
                ConsoleColor.Magenta => "\x1B[1m\x1B[35m",
                ConsoleColor.Cyan => "\x1B[1m\x1B[36m",
                ConsoleColor.White => "\x1B[1m\x1B[37m",

                _ => DefaultForegroundColor
            };

        static string GetBackgroundColorEscapeCode(ConsoleColor color) =>
            color switch
            {
                ConsoleColor.Black => "\x1B[40m",
                ConsoleColor.DarkRed => "\x1B[41m",
                ConsoleColor.DarkGreen => "\x1B[42m",
                ConsoleColor.DarkYellow => "\x1B[43m",
                ConsoleColor.DarkBlue => "\x1B[44m",
                ConsoleColor.DarkMagenta => "\x1B[45m",
                ConsoleColor.DarkCyan => "\x1B[46m",
                ConsoleColor.Gray => "\x1B[47m",

                _ => DefaultBackgroundColor
            };
    }


}
