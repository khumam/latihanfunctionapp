using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using BookManagementFunctionApp6.Managers;
using BookManagementFunctionApp6.Models;
using System.Collections.Generic;
using TodoFunctionApp.Helpers;
using System.IO;

[assembly: FunctionsStartup(typeof(Startup))]
namespace BookManagementFunctionApp6
{
  public class BookManagementFunctionApp
  {
    private readonly BookManager _bookManager;
    private readonly TodoManager _todoManager;

    public BookManagementFunctionApp(
      BookContext bookContext,
      TodoContext todoContext
    )
    {
      this._bookManager = new BookManager(bookContext);
      this._todoManager = new TodoManager(todoContext);
    }

    [FunctionName("GetAllBooks")]
    public async Task<IActionResult> RunGetAllBooks(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
    {
      try
      {
        List<Book> books = await _bookManager.FetchBooks();
        return new OkObjectResult(JsonConvert.SerializeObject(books));
      }
      catch (Exception e)
      {
        return ErrorHelper.HandleError(e, null, log);
      }
    }

    [FunctionName("CreateBook")]
    public async Task<IActionResult> RunCreateBook(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
    {
      try
      {
        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        dynamic data = JsonConvert.DeserializeObject(requestBody);
        Book bookData = new()
        {
          Id = data.Id,
          Title = data.Title,
          Author = data.Author,
        };

        Book book = await _bookManager.CreateBook(bookData);
        return new OkObjectResult(JsonConvert.SerializeObject(book));
      }
      catch (Exception e)
      {
        return ErrorHelper.HandleError(e, null, log);
      }
    }

    [FunctionName("GetAllTodos")]
    public async Task<IActionResult> RunGetAllTodos(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
    {
      try
      {
        List<Todo> todos = await _todoManager.FetchTodos();
        return new OkObjectResult(JsonConvert.SerializeObject(todos));
      }
      catch (Exception e)
      {
        return ErrorHelper.HandleError(e, null, log);
      }
    }

    [FunctionName("CheckEnvironment")]
    public async Task<IActionResult> RunCheckEnvironment(
      [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
    {
      try
      {
        string data = Environment.GetEnvironmentVariable("DataValue", EnvironmentVariableTarget.Process);
        Console.WriteLine(data);
        return new OkObjectResult(JsonConvert.SerializeObject(new {response = data}));
      }
      catch (Exception e)
      {
        return ErrorHelper.HandleError(e, null, log);
      }
    }
  }
}
