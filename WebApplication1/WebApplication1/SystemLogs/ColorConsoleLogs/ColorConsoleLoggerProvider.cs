﻿using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using System.Runtime.Versioning;

namespace WebApplication1.SystemLogs.ColorConsoleLogs
{

    /// <summary>
    /// 
    /// </summary>
    [UnsupportedOSPlatform("browser")]
    [ProviderAlias("ColorConsole")]
    public class ColorConsoleLoggerProvider : ILoggerProvider
    {
        private readonly IDisposable? _onChangeToken;
        private ColorConsoleLoggerConfiguration _currentConfig;
        private readonly ConcurrentDictionary<string, ColorConsoleLogger> _loggers =
            new(StringComparer.OrdinalIgnoreCase);

        public ColorConsoleLoggerProvider(
            IOptionsMonitor<ColorConsoleLoggerConfiguration> config)
        {
            _currentConfig = config.CurrentValue;
            _onChangeToken = config.OnChange(updatedConfig => _currentConfig = updatedConfig);
        }

        public ILogger CreateLogger(string categoryName) =>
            _loggers.GetOrAdd(categoryName, name => new ColorConsoleLogger(name, GetCurrentConfig));

        private ColorConsoleLoggerConfiguration GetCurrentConfig() => _currentConfig;

        public void Dispose()
        {
            _loggers.Clear();
            _onChangeToken?.Dispose();
        }
    }
}