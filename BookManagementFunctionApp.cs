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

[assembly: FunctionsStartup(typeof(Startup))]
namespace BookManagementFunctionApp6
{
  public class BookManagementFunctionApp
  {
    private readonly BookManager _bookManager;

    public BookManagementFunctionApp(
      BookContext bookContext
    )
    {
      this._bookManager = new BookManager(bookContext);
    }

    [FunctionName("GetAllBooks")]
    public async Task<IActionResult> RunGetAllBooks(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
    {
      try
      {
        List<Book> todoItems = await _bookManager.FetchBooks();
        return new OkObjectResult(JsonConvert.SerializeObject(todoItems));
      }
      catch (Exception e)
      {
        return ErrorHelper.HandleError(e, null, log);
      }
    }
  }
}
