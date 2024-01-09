using AutoMapper;
using BookStore.Api.BookOperations.DeleteBook;
using BookStore.Api.BookOperations.GetBookDetail;
using BookStore.Api.BookOperations.GetBooks;
using BookStore.Api.BookOperations.UpdateBook;
using BookStore.Api.DbOperations;
using BookStore.BookOperations.CreateBook;
using BookStore.BookOperations.GetBookDetail;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController(BookStoreDbContext context, IMapper mapper) : ControllerBase
    {
        private readonly BookStoreDbContext context = context;
        private readonly IMapper mapper = mapper;

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var query = new GetBooksQuery(context, mapper);

            var result = await query.HandleAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            BookDetailViewModel result;

            try
            {
                var query = new GetBookDetailQuery(context, mapper)
                {
                    BookId = id
                };

                result = await query.HandleAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] CreateBookModel createBookModel)
        {
            var command = new CreateBookCommand(context, mapper);

            try
            {
                command.Model = createBookModel;
                await command.HandleAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] UpdateBookModel updateBookModel)
        {
            try
            {
                var command = new UpdateBookCommand(context);

                command.BookId = id;
                command.Model = updateBookModel;

                await command.HandleAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            DeleteBookCommand command = new DeleteBookCommand(context);
            try
            {

                command.BookId = id;
                await command.HandleAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}
