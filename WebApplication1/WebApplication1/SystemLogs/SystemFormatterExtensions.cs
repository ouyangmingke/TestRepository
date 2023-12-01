using WebApplication1.SystemLogs.Formatters.ConsoleFormatters;

namespace WebApplication1.SystemLogs
{
    public static class SystemFormatterExtensions
    {
        /* 在 ConsoleLoggerProvider 中
         * 会注入全部 ConsoleFormatter
         * 通过配置的 FormatterName 找到目标格式化器
         * 使用目标 ConsoleFormatter 注册 ConsoleLogger 
         */

        #region ConsoleFormatter 供Console日志提供程序使用的日志格式化工具

        /// <summary>
        /// 添加自定义日志格式化
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static ILoggingBuilder AddCustomFormatter(
           this ILoggingBuilder builder,
           Action<CustomOptions> configure) =>
           builder.AddConsoleFormatter<CustomFormatter, CustomOptions>(configure);


        /// <summary>
        /// 添加自定义日志格式化
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static ILoggingBuilder AddCustomWrappingConsoleFormatter(
           this ILoggingBuilder builder,
           Action<CustomWrappingConsoleFormatterOptions> configure) =>
           builder.AddConsoleFormatter<CustomTimePrefixingFormatter, CustomWrappingConsoleFormatterOptions>(configure);
        #endregion
    }
}
