using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.DAL
{
    public interface ITodoItemsRepository
    {
        Task<IEnumerable<TodoItem>> GetTodoItems();
        Task<TodoItem> GetTodoItem(long id);
        Task<TodoItem> DeleteTodoItem(long id);
        void PutTodoItem(long id, TodoItem todoItem);
        void PostTodoItem(TodoItem todoItem);
        bool TodoItemExists(long id);
        Task Save();        
    }
}
