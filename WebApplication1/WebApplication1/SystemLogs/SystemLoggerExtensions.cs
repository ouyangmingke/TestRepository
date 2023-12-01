using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging.Configuration;
using WebApplication1.SystemLogs.ColorConsoleLogs;
using WebApplication1.SystemLogs.CustomLogs;

namespace WebApplication1.SystemLogs
{
    public static class SystemLoggerExtensions
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

        #region  ColorConsoleLogger
        public static ILoggingBuilder AddColorConsoleLogger(
              this ILoggingBuilder builder)
        {
            builder.AddConfiguration();

            builder.Services.TryAddEnumerable(
                ServiceDescriptor.Singleton<ILoggerProvider, ColorConsoleLoggerProvider>());
            LoggerProviderOptions.RegisterProviderOptions
                <ColorConsoleLoggerConfiguration, ColorConsoleLoggerProvider>(builder.Services);
            return builder;
        }

        public static ILoggingBuilder AddColorConsoleLogger(
            this ILoggingBuilder builder,
            Action<ColorConsoleLoggerConfiguration> configure)
        {
            builder.AddColorConsoleLogger();
            builder.Services.Configure(configure);
            return builder;
        }
        #endregion
    }
}