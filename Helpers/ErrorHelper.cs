using System;
using System.Net;
using Microsoft.Extensions.Logging;

namespace TodoFunctionApp.Helpers
{
  public class ErrorHelper
  {
    public ErrorHelper() { }
    public static CustomResponse HandleError(Exception e, object quargs, ILogger logger)
    {
      logger.LogError(e, "Bad data");
      return new CustomResponse("Error", HttpStatusCode.Conflict);
    }
  }
}