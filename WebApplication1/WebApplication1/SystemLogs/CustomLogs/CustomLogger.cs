
namespace WebApplication1.SystemLogs.CustomLogs
{

    /// <summary>
    /// 自定义日志记录器
    /// </summary>
    public sealed class CustomLogger : ILogger
    {
        private readonly string name;
        private readonly Func<CustomLoggerConfiguration> getCurrentConfig;

        /// <summary>
        /// 通过 CustomLoggerProvider 进行创建
        /// </summary>
        /// <param name="name"></param>
        /// <param name="getCurrentConfig"></param>
        public CustomLogger(
            string name,
            Func<CustomLoggerConfiguration> getCurrentConfig) =>
            (this.name, this.getCurrentConfig) = (name, getCurrentConfig);
        // 避免编译器检查该值为 null
        public IDisposable BeginScope<TState>(TState state) => default!;

        // 检查配置中是否启用该级别
        public bool IsEnabled(LogLevel logLevel) =>
            getCurrentConfig().LogLevels.ContainsKey(logLevel);

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception? exception,
            Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            CustomLoggerConfiguration config = getCurrentConfig();
            // 无事件id或相等
            if (config.EventId == 0 || config.EventId == eventId.Id)
            {
                switch (config.LogLevels[logLevel])
                {
                    case CustomLoggerConfiguration.LogFormat.Short:
                        Console.WriteLine($"{nameof(CustomLogger)}==》 {name}: {formatter(state, exception)}");
                        break;
                    case CustomLoggerConfiguration.LogFormat.Long:
                        Console.WriteLine($"{nameof(CustomLogger)}==》 [{eventId.Id,2}: {logLevel,-12}] {name} - {formatter(state, exception)}");
                        break;
                    default:
                        // No-op
                        break;
                }
            }
        }
    }
}