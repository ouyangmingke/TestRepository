using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging.Configuration;
using WebApplication1.SystemLogs.Formatters;

namespace WebApplication1.SystemLogs.CustomLogs
{
    public static class CustomLoggerExtensions
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

        /// <summary>
        /// ����Զ�����־��ʽ��
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static ILoggingBuilder AddCustomFormatter(
           this ILoggingBuilder builder,
           Action<CustomOptions> configure) =>
           builder.AddConsoleFormatter<CustomFormatter, CustomOptions>(configure);


        /// <summary>
        /// ����Զ�����־��ʽ��
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