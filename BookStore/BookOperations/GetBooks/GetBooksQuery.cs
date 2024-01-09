using AutoMapper;
using BookStore.Api.DbOperations;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Api.BookOperations.GetBooks;
public class GetBooksQuery
{
    private readonly BookStoreDbContext _dbContext;

    public GetBooksQuery(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<BooksViewModel> Handle()
    {
        var bookList = _dbContext.Books.OrderBy(x => x.Id).ToList<Book>();
        List<BooksViewModel> vm = new List<BooksViewModel>();
        foreach (var book in bookList)
        {
            vm.Add(new BooksViewModel()
            {
                Title = book.Title,
                Genre = ((BookStore.Common.GenreEnum)book.GenreId).ToString(),
                PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy"),
                PageCount = book.PageCount
            });
        }
        return vm;
    }

    //View Model

    public class BooksViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }
}
