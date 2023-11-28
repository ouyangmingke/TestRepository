using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging.Configuration;

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

        builder.Services.TryAddEnumerable(
            ServiceDescriptor.Singleton<ILoggerProvider, CustomLoggerProvider>());

        LoggerProviderOptions.RegisterProviderOptions
            <CustomLoggerConfiguration, CustomLoggerProvider>(builder.Services);

        return builder;
    }
}