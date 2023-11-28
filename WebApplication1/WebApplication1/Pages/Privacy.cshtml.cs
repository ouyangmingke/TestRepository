using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection.Emit;
using ViewComponentSample.Models;

namespace WebApplication1.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        // 启用模型绑定
        [BindProperty]
        public IList<TodoItem> TodoItems { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            TodoItems = new List<TodoItem>();
            for (int i = 0; i < 10; i++)
            {
                TodoItems.Add(
                     new TodoItem
                     {
                         Id = i,
                         Name = SearchString + i
                     }
                    );
            }
        }

        public void OnPost(string code)
        {
            TodoItems = new List<TodoItem>();
            for (int i = 0; i < 10; i++)
            {
                TodoItems.Add(new TodoItem
                {
                    Id = i,
                    Name = code + i
                });
            }
        }

    }
}