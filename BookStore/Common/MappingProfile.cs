using AutoMapper;
using BookStore.BookOperations.GetBooks;
using BookStore.BookOperations.CreateBook;
using BookStore.BookOperations.GetBookDetail;

namespace BookStore.Common;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateBookCommand.CreateBookModel, Book>(); //CreateBookModel objesi, book objesine maplenebilir olsun
        CreateMap<Book, GetBookDetailQuery.BookDetailViewModel>()
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()))
            .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishDate.Date.ToString("dd/MM/yyyy")));
        CreateMap<Book, GetBooksQuery.BooksViewModel>()
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()))
            .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishDate.Date.ToString("dd/MM/yyyy")));
    }
}