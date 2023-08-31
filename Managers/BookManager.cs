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
      try {
        List<Book> books = await _bookContext.Books.ToListAsync();
        return books;
      } catch (Exception e) {
        throw e;
      }
    }

    public async Task<Book> CreateBook(Book book)
    {
      try {
        Book bookData = new()
        {
          Id = book.Id,
          Title = book.Title,
          Author = book.Author
        };

        _bookContext.Add(bookData);
        _bookContext.SaveChanges();
        return bookData;
      } catch (Exception e) {
        throw e;
      }
    }
  }
}