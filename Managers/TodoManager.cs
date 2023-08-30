using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using BookManagementFunctionApp6.Models;
using System.Threading.Tasks;

namespace BookManagementFunctionApp6.Managers
{
  public class TodoManager
  {
    private readonly TodoContext _todoContext;

    public TodoManager(TodoContext todoContext)
    {
      this._todoContext = todoContext;
    }

    public async Task<List<Todo>> FetchTodos()
    {
      List<Todo> todos = await _todoContext.Todos.ToListAsync();
      return todos;
    }
  }
}