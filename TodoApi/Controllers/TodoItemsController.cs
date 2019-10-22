using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.DAL;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemsRepository _todoRepository;

        public TodoItemsController(ITodoItemsRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<IEnumerable<TodoItem>> GetTodoItems()
        {
            return await _todoRepository.GetTodoItems();
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            var todoItem = await _todoRepository.GetTodoItem(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            return todoItem;
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            _todoRepository.PutTodoItem(id, todoItem);            

            try
            {                
                await _todoRepository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public ActionResult<TodoItem> PostTodoItem(TodoItem todoItem)
        {
            _todoRepository.PostTodoItem(todoItem);
            _todoRepository.Save();
            
            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoItem>> DeleteTodoItem(long id)
        {
            var todoItem = await _todoRepository.GetTodoItem(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            await _todoRepository.DeleteTodoItem(id);
            await _todoRepository.Save();

            return todoItem;
        }

        private bool TodoItemExists(long id)
        {
            return _todoRepository.TodoItemExists(id);
        }
    }
}
