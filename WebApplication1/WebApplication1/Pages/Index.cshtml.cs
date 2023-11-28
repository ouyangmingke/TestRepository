using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ViewComponentSample.Models;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            TodoItems = new List<TodoItem>();
        }
        public List<TodoItem> TodoItems { get; set; }

        public string Name { get; set; } = "IndexPageModel";
        public void OnGet()
        {

        }

        public void OnPost()
        {

        }
        public IActionResult OnPostViewComponent()
        {
            return ViewComponent("PriorityList",
                   new
                   {
                       maxPriority = 10,
                       isDone = true
                   });
        }
        public async Task OnPostTask()
        {
            await Task.Delay(1000);
        }
        /// <summary>
        /// 处理程序方法 Create
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPostCreate()
        {
            for (int i = 0; i < 10; i++)
            {
                TodoItems.Add(
                    new TodoItem { Id = i, Name = "Task" + i, Priority = 10 - i, IsDone = false }
                    );
            };
            return Page();
        }
        /// <summary>
        /// NonHandler  拒绝服务端的方法匹配处理请求
        /// </summary>
        /// <returns></returns>
        [NonHandler]
        public IActionResult OnPostEdit()
        {
            for (int i = 10; i < 20; i++)
            {
                TodoItems.Add(
                    new TodoItem { Id = i, Name = "Task" + i, Priority = 10 - i, IsDone = false }
                    );
            };
            return Page();
        }
    }
}