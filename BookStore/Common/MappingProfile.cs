using AutoMapper;
using BookStore.Application.AuthorOperations.Commands.CreateAuthor;
using BookStore.Application.AuthorOperations.Commands.GetAuthorDetail;
using BookStore.Application.AuthorOperations.Commands.GetAuthors;
using BookStore.Application.AuthorOperations.Commands.UpdateAuthor;
using BookStore.Application.BookOperations.Commands.CreateBook;
using BookStore.Application.BookOperations.Queries.GetBookDetail;
using BookStore.Application.BookOperations.Queries.GetBooks;
using BookStore.Application.GenreOperations.Queries.GetGenreDetail;
using BookStore.Application.GenreOperations.Queries.GetGenres;
using BookStore.Entities;

namespace BookStore.Common;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateBookCommand.CreateBookModel, Book>(); //CreateBookModel objesi, book objesine maplenebilir olsun
        CreateMap<Book, GetBookDetailQuery.BookDetailViewModel>()
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()))
            .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishDate.Date.ToString("dd/MM/yyyy")))
            .ForMember(dest => dest.AuthorName,opt => opt.MapFrom(src => src.Author.Name + " " + src.Author.Surname))
            .ForMember(dest => dest.Genre,opt => opt.MapFrom(src => src.Genre.Name));
        CreateMap<Book, GetBooksQuery.BooksViewModel>()
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()))
            .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishDate.Date.ToString("dd/MM/yyyy")))
            .ForMember(dest => dest.AuthorName,opt => opt.MapFrom(src => src.Author.Name + " " + src.Author.Surname))
            .ForMember(dest => dest.Genre,opt => opt.MapFrom(src => src.Genre.Name));

        CreateMap<Genre, GenresViewModel>();
        CreateMap<Genre, GenreDetailViewModel>();
        CreateMap<Author, GetAuthorsQuery.AuthorsViewModel>()
            .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday.Date.ToString("dd/MM/yyyy")));
        CreateMap<Author, GetAuthorDetailQuery.AuthorDetailViewModel>()
            .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday.Date.ToString("dd/MM/yyyy")));

        CreateMap<UpdateAuthorCommand.UpdateAuthorViewModel, Author>();
         CreateMap<CreateAuthorCommand.CreateAuthorViewModel, Author>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname))
            .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday));
    }
}