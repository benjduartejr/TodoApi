using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.DAL
{
    public class TodoItemsRepository : ITodoItemsRepository, IDisposable
    {
        private bool disposed = false;
        private readonly TodoContext _context;             

        public TodoItemsRepository(TodoContext todoContext)
        {
            _context = todoContext;
        }

        public async Task<TodoItem> DeleteTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            _context.TodoItems.Remove(todoItem);            

            return  todoItem;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<TodoItem> GetTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            return todoItem;
        }

        public void PostTodoItem(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);            
        }

        public void PutTodoItem(long id, TodoItem todoItem)
        {
            _context.Entry(todoItem).State = EntityState.Modified;
        }

        public bool TodoItemExists(long id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        
        public async Task<IEnumerable<TodoItem>> GetTodoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }

        public async Task Save()
        {            
            await Task.Run(() => _context.SaveChangesAsync());
        }
    }
}
