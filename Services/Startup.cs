using System;
using BookManagementFunctionApp6.Models;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

public class Startup : FunctionsStartup
{
  public override void Configure(IFunctionsHostBuilder builder)
  {
    // Enable logging
    builder.Services.AddLogging();
    string sqlConnectionString = Environment.GetEnvironmentVariable("ConnectionString", EnvironmentVariableTarget.Process);
    Console.WriteLine(sqlConnectionString);
    builder.Services.AddDbContext<BookContext>(opt => opt.UseNpgsql(sqlConnectionString));
    builder.Services.AddDbContext<TodoContext>(opt => opt.UseNpgsql(sqlConnectionString));
  }
}

