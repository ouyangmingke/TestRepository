using WebApplication1.Extensions;

var builder = WebApplication.CreateBuilder(args);
// 添加的一组默认的日志记录提供程序：
builder.Host.ConfigureLogging(logging =>
{
    // 清除现有的提供程序
    logging.ClearProviders();
    // 使用自定义日志提供程序
    logging.AddCustomLogger();
});
// 筛选器函数
builder.Logging.AddFilter((provider, category, logLevel) =>
{
    return true;
    if (provider.Contains("ConsoleLoggerProvider")
        && category.Contains("Controller")
        && logLevel >= LogLevel.Information)
    {
        return true;
    }
    else if (provider.Contains("ConsoleLoggerProvider")
        && category.Contains("Microsoft")
        && logLevel >= LogLevel.Information)
    {
        return true;
    }
    else
    {
        return false;
    }
});



// Add services to the container.
builder.Services.AddRazorPages();
// 添加目录浏览器中间件服务。
builder.Services.AddDirectoryBrowser();

var app = builder.Build();
app.Logger.LogInformation("Starting the app");
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();
// 添加其他静态文件
app.UseMyStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
