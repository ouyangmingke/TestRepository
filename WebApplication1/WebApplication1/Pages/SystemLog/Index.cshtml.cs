using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages.SystemLog
{
    public class IndexModel : PageModel
    {
        private ILogger<IndexModel> Logger { get; }

        public IndexModel(ILogger<IndexModel> Logger)
        {
            this.Logger = Logger;
        }

        public void OnGet()
        {
            //Logger.LogDebug(1, "This is a debug message.");
            Logger.LogInformation(3, "This is an information message.\n\n");
            Logger.LogWarning(5, "This is a warning message.");
            //Logger.LogError(7, "This is an error message.");
            //Logger.LogTrace(5!, "This is a trace message.");
        }
    }
}
