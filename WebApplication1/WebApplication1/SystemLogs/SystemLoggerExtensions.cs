using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging.Configuration;
using WebApplication1.SystemLogs.ColorConsoleLogs;
using WebApplication1.SystemLogs.CustomLogs;

namespace WebApplication1.SystemLogs
{
    public static class SystemLoggerExtensions
    {
        /// <summary>
        /// ����Զ�����־��¼��
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static ILoggingBuilder AddCustomLogger(
            this ILoggingBuilder builder)
        {
            // ��ȡ�����ļ�
            builder.AddConfiguration();
            // ������ӷ���ע��
            builder.Services.TryAddEnumerable(
                ServiceDescriptor.Singleton<ILoggerProvider, CustomLoggerProvider>());
            // Options �󶨵� Provider 
            // ��ͨ�������ļ����� Provider
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