using AutoMapper;
using BookStore.DbOperations;
using BookStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.BookOperations.Queries.GetBooks;
public class GetBooksQuery
{
    private readonly BookStoreDbContext _dbContext;
private readonly IMapper _mapper;
    public GetBooksQuery(BookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public List<BooksViewModel> Handle()
    {
        var bookList = _dbContext.Books.Include(x=>x.Genre).Include(a => a.Author).OrderBy(x => x.Id).ToList<Book>();
        var vm = _mapper.Map<List<Book>,  List<BooksViewModel>>(bookList);
        return vm;
    }

    //View Model

    public class BooksViewModel
    {
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }
}
