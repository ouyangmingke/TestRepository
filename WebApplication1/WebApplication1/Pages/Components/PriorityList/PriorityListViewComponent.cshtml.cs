using Microsoft.AspNetCore.Mvc;
using ViewComponentSample.Models;

namespace ViewComponentSample.ViewComponents;

public class PriorityListViewComponent : ViewComponent
{

    public async Task<IViewComponentResult> InvokeAsync(
                                            int maxPriority, bool isDone)
    {
        var items = await GetItemsAsync(maxPriority, isDone);
        return View(items);
    }

    private Task<List<TodoItem>> GetItemsAsync(int maxPriority, bool isDone)
    {
        var list = new List<TodoItem>();
        for (int i = 0; i < 10; i++)
        {
            list.Add(
                new TodoItem { Id = i, Name = "Task" + i, Priority = 10 - i, IsDone = false }


                );
        };
        return Task.FromResult(list);
    }
}