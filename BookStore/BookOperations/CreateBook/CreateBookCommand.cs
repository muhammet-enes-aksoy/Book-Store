using AutoMapper;
using BookStore.Api;
using BookStore.Api.DbOperations;
using Microsoft.EntityFrameworkCore;

namespace BookStore.BookOperations.CreateBook;

public class CreateBookCommand(BookStoreDbContext dbContext, IMapper mapper)
{
    private readonly BookStoreDbContext dbContext = dbContext;
    private readonly IMapper mapper = mapper;
    public CreateBookModel? Model { get; set; }

    public async Task HandleAsync()
    {
        // if model is null, throw exception
        if (Model is null)
            throw new ArgumentNullException($"Model : {nameof(Model)} can not be null.");

        // if book title already exists, throw exception
        if (await dbContext.Books.AnyAsync(x => x.Title == Model.Title))
            throw new InvalidOperationException($"The book already exists with title: {Model.Title}");

        var book = mapper.Map<Book>(Model);

        dbContext.Books.Add(book);
        dbContext.SaveChanges();
    }
}