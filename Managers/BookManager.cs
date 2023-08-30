using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using BookManagementFunctionApp6.Models;
using System.Threading.Tasks;

namespace BookManagementFunctionApp6.Managers
{
  public class BookManager
  {
    private readonly BookContext _bookContext;

    public BookManager(BookContext bookContext)
    {
      this._bookContext = bookContext;
    }

    public async Task<List<Book>> FetchBooks()
    {
      List<Book> books = await _bookContext.Books.ToListAsync();
      return books;
    }
  }
}