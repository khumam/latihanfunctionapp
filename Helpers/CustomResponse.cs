using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace TodoFunctionApp.Helpers
{
  public class CustomResponse : ObjectResult
  {
    public CustomResponse(object value, HttpStatusCode code) : base(value)
    {
      this.StatusCode = (int)code;
    }
  }
}