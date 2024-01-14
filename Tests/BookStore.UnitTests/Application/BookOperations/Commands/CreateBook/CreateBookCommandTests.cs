using AutoMapper;
using BookStore.Application.BookOperations.Commands.CreateBook;
using BookStore.DbOperations;
using BookStore.Entities;
using BookStore.UnitTests.TestsSetup;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace BookStore.UnitTests.Application.BookOperations.Commands.CreateBook;
public class CreateBookCommandTests(CommonTestFixture testFixture) : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context = testFixture.Context;
    private readonly IMapper mapper = testFixture.Mapper;

    [Fact]
    public async Task WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        //Arrange 
        var book = new Book()
        {
            Title = "Test_WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn",
            PageCount = 100,
            PublishDate = new DateTime(1990, 01, 10),
            GenreId = 1,
            AuthorId = 1
        };

        await context.Books.AddAsync(book);
        await context.SaveChangesAsync();

        var command = new CreateBookCommand(context, mapper)
        {
            Model = new CreateBookModel()
            {
                Title = book.Title
            }
        };

        //Act & Assert 
        await FluentActions
            .Invoking(async () => await command.HandleAsync())
            .Should().ThrowAsync<InvalidOperationException>().WithMessage($"The book already exists with title: {book.Title}");
    }

    // Happy Path
    [Fact]
    public async Task WhenValidInputsAreGiven_Book_ShouldBeCreated()
    {
        // Arrange
        var command = new CreateBookCommand(context, mapper);

        var model = new CreateBookModel()
        {
            Title = "Solaris",
            PageCount = 224,
            PublishDate = new DateTime(2002, 1, 1),
            GenreId = 1
        };
        command.Model = model;

        // Act
        await FluentActions.Invoking(async () => await command.HandleAsync()).Invoke();

        // Assert
        var book = await context.Books.SingleOrDefaultAsync(book => book.Title == model.Title);
        book.Should().NotBeNull();
        book.PageCount.Should().Be(model.PageCount);
        book.PublishDate.Should().Be(model.PublishDate.Date);
        book.GenreId.Should().Be(model.GenreId);
    }
}