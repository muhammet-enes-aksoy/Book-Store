using AutoMapper;
using BookStore.Api;
using BookStore.Api.DbOperations;
using Microsoft.EntityFrameworkCore;

namespace BookStore.BookOperations.CreateBook;

public class CreateBookCommand
{
    public CreateBookModel Model { get; set; }
    private readonly BookStoreDbContext _dbContext;

    public CreateBookCommand(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Handle()
    {
        var book = _dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);
        if (book != null)
        {
            throw new InvalidOperationException("The book already exists");

        }
        book = new Book
        {
            Title = Model.Title,
            GenreId = Model.GenreId,
            PageCount = Model.PageCount,
            PublishDate = Model.PublishDate
        };
        _dbContext.Books.Add(book);
        _dbContext.SaveChanges();
    }

    public class CreateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}