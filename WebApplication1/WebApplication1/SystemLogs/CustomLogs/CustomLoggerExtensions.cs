using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging.Configuration;
using WebApplication1.SystemLogs.Formatters;

namespace WebApplication1.SystemLogs.CustomLogs
{
    public static class CustomLoggerExtensions
    {
        /// <summary>
        /// 添加自定义日志记录器
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static ILoggingBuilder AddCustomLogger(
            this ILoggingBuilder builder)
        {
            // 获取配置文件
            builder.AddConfiguration();
            // 尝试添加服务注入
            builder.Services.TryAddEnumerable(
                ServiceDescriptor.Singleton<ILoggerProvider, CustomLoggerProvider>());
            // Options 绑定到 Provider 
            // 可通过配置文件配置 Provider
            LoggerProviderOptions.RegisterProviderOptions
                <CustomLoggerConfiguration, CustomLoggerProvider>(builder.Services);
            return builder;
        }

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

    }
}