using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;

namespace WebApplication1.SystemLogs.Formatters
{
    /// <summary>
    /// 自定义包装控制台格式化程序选项
    /// </summary>
    public sealed class CustomWrappingConsoleFormatterOptions : ConsoleFormatterOptions
    {
        /// <summary>
        /// 自定义前缀
        /// </summary>
        public string? CustomPrefix { get; set; }
        /// <summary>
        /// 自定义后缀
        /// </summary>
        public string? CustomSuffix { get; set; }
    }
    /// <summary>
    /// 为日志时间前缀 自定义后缀 
    /// 自定义控制台日志格式工具
    /// </summary>
    public sealed class CustomTimePrefixingFormatter : ConsoleFormatter, IDisposable
    {
        private readonly IDisposable? _optionsReloadToken;
        private CustomWrappingConsoleFormatterOptions _formatterOptions;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options">可更新配置项</param>
        public CustomTimePrefixingFormatter(IOptionsMonitor<CustomWrappingConsoleFormatterOptions> options)
            // Case insensitive
            : base(nameof(CustomTimePrefixingFormatter)) =>
            (_optionsReloadToken, _formatterOptions) =
                (options.OnChange(ReloadLoggerOptions), options.CurrentValue);
        /// <summary>
        /// 更新配置
        /// </summary>
        /// <param name="options"></param>
        private void ReloadLoggerOptions(CustomWrappingConsoleFormatterOptions options) =>
            _formatterOptions = options;

        public override void Write<TState>(
            in LogEntry<TState> logEntry,
            IExternalScopeProvider? scopeProvider,
            TextWriter textWriter)
        {
            string message =
                logEntry.Formatter(
                    logEntry.State, logEntry.Exception);

            if (message == null)
            {
                return;
            }
            // 写入前缀和后缀
            WritePrefix(textWriter);
            textWriter.Write(message);
            WriteSuffix(textWriter);
        }

        private void WritePrefix(TextWriter textWriter)
        {
            DateTime now = _formatterOptions.UseUtcTimestamp
                ? DateTime.UtcNow
                : DateTime.Now;

            textWriter.Write($"{_formatterOptions.CustomPrefix} {now.ToString(_formatterOptions.TimestampFormat)}");
        }

        private void WriteSuffix(TextWriter textWriter) =>
            textWriter.WriteLine($" {_formatterOptions.CustomSuffix}");

        public void Dispose() => _optionsReloadToken?.Dispose();
    }
}
