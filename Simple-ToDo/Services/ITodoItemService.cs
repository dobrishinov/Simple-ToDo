using Simple_ToDo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simple_ToDo.Services
{
    public interface ITodoItemService
    {
        Task<TodoItem[]> GetIncompleteItemsAsync(ApplicationUser user);

        Task<bool> AddItemAsync(TodoItem newItem, ApplicationUser user);

        Task<bool> MarkDoneAsync(Guid id, ApplicationUser user);
    }
}
