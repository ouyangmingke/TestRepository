using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;

namespace WebApplication1.Extensions
{
    public static class MyStaticFileExtensions
    {
        public static IApplicationBuilder UseMyStaticFiles(this IApplicationBuilder app)
        {
            // 添加新的文件路径
            var fileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "MyStaticFiles"));
            var requestPath = "/MyStaticFiles";
            var provider = new FileExtensionContentTypeProvider();
            // 添加新的映射
            provider.Mappings[".apk"] = "application/x-msdownload";
            provider.Mappings[".htm3"] = "text/html";
            provider.Mappings[".image"] = "image/png";
            // 替换现有映射
            provider.Mappings[".rtf"] = "application/x-msdownload";
            // 移除映射
            provider.Mappings.Remove(".mp4");

            app.UseStaticFiles(new StaticFileOptions
            {
                ContentTypeProvider = provider,
                FileProvider = fileProvider,
                RequestPath = requestPath
            });
            // 启用目录浏览
            //app.UseDirectoryBrowser(new DirectoryBrowserOptions
            //{
            //    FileProvider = fileProvider,
            //    RequestPath = requestPath
            //});
            return app;
        }
    }
}
