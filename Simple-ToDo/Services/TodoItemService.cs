using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Simple_ToDo.Data;
using Simple_ToDo.Models;

namespace Simple_ToDo.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ApplicationDbContext _context;

        public TodoItemService(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<bool> AddItemAsync(TodoItem newItem, ApplicationUser user)
        {
            newItem.Id = Guid.NewGuid();
            newItem.IsDone = false;
            newItem.DueAt = newItem.DueAt;
            newItem.UserId = user.Id;

            _context.Items.Add(newItem);

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<TodoItem[]> GetIncompleteItemsAsync(ApplicationUser user)
        {
            return await _context.Items
                .Where(x => x.IsDone == false && x.UserId == user.Id).OrderBy(x=> x.DueAt)
                .ToArrayAsync();
        }

        public async Task<bool> MarkDoneAsync(Guid id, ApplicationUser user)
        {
            var item = await _context.Items
                .Where(x => x.Id == id && x.UserId == user.Id)
                .SingleOrDefaultAsync();

            if (item == null)
            {
                return false;
            }

            item.IsDone = true;

            var saveResult = await _context.SaveChangesAsync();

            return saveResult == 1;
        }
    }
}
