using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;

namespace WebApplication1.SystemLogs.Formatters.ConsoleFormatters
{
    public class CustomOptions : ConsoleFormatterOptions
    {
        public string? CustomPrefix { get; set; }
    }

    /// <summary>
    /// 为日志添加自定义前缀
    /// 自定义控制台日志格式工具
    /// </summary>
    public sealed class CustomFormatter : ConsoleFormatter, IDisposable
    {
        private readonly IDisposable? _optionsReloadToken;
        private CustomOptions _formatterOptions;

        public CustomFormatter(IOptionsMonitor<CustomOptions> options)
            // Case insensitive
            : base(nameof(CustomFormatter)) =>
            (_optionsReloadToken, _formatterOptions) =
                (options.OnChange(ReloadLoggerOptions), options.CurrentValue);

        private void ReloadLoggerOptions(CustomOptions options) =>
            _formatterOptions = options;

        /// <summary>
        /// 每个日志消息包装的文本类型
        /// 一个标准的 ConsoleFormatter 应至少能包装日志的范围、时间戳和严重性级别。
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="logEntry"></param>
        /// <param name="scopeProvider"></param>
        /// <param name="textWriter"></param>
        public override void Write<TState>(
            in LogEntry<TState> logEntry,
            IExternalScopeProvider? scopeProvider,
            TextWriter textWriter)
        {
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
            textWriter.Write(_formatterOptions.CustomPrefix);
        }
        public void Dispose() => _optionsReloadToken?.Dispose();
    }
}
