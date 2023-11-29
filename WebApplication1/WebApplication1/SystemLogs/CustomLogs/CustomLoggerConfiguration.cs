namespace WebApplication1.SystemLogs.CustomLogs
{

    /// <summary>
    /// 自定义日志配置
    /// 更据不同日志等级设置不同日志格式
    /// </summary>
    public class CustomLoggerConfiguration
    {
        public int EventId { get; set; }

        public Dictionary<LogLevel, LogFormat> LogLevels { get; set; } =
            new()
            {
                [LogLevel.Information] = LogFormat.Short,
                [LogLevel.Warning] = LogFormat.Short,
                [LogLevel.Error] = LogFormat.Long
            };

        public enum LogFormat
        {
            Short,
            Long
        }
    }
}