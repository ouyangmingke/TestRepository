using System.Collections.Concurrent;
using Microsoft.Extensions.Options;

namespace WebApplication1.SystemLogs.CustomLogs
{

    /// <summary>
    /// 自定义日志记录提供程序
    /// 使用 CustomLog 作为别名
    /// </summary>
    [ProviderAlias("CustomLog")]
    public sealed class CustomLoggerProvider : ILoggerProvider
    {
        private readonly IDisposable onChangeToken;
        private CustomLoggerConfiguration config;
        /// <summary>
        /// 并发字典
        /// Key不区分大小写
        /// </summary>
        private readonly ConcurrentDictionary<string, CustomLogger> loggers =
            new(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 通过选项模式获取配置
        /// </summary>
        /// <param name="config">变化时通知的选项</param>
        public CustomLoggerProvider(
            IOptionsMonitor<CustomLoggerConfiguration> config)
        {
            this.config = config.CurrentValue;
            // 注册变更回调
            onChangeToken = config.OnChange(updatedConfig => this.config = updatedConfig);
        }
        /// <summary>
        /// 创建日志执行器
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public ILogger CreateLogger(string categoryName) =>
            loggers.GetOrAdd(categoryName, name => new CustomLogger(name, GetCurrentConfig));

        private CustomLoggerConfiguration GetCurrentConfig() => config;

        public void Dispose()
        {
            loggers.Clear();
            onChangeToken.Dispose();
        }
    }
}