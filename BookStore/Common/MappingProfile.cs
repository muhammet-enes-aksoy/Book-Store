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

// AutoMapper profile for configuring mappings between different models and entities.
public class MappingProfile : Profile
{
    // Constructor: Configures mappings within the profile.
    public MappingProfile()
    {
        // Map CreateBookModel to Book entity.
        CreateMap<CreateBookCommand.CreateBookModel, Book>();

        // Map Book entity to BookDetailViewModel for detailed book queries.
        CreateMap<Book, GetBookDetailQuery.BookDetailViewModel>()
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()))
            .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishDate.Date.ToString("dd/MM/yyyy")))
            .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name + " " + src.Author.Surname))
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));

        // Map Book entity to BooksViewModel for listing books.
        CreateMap<Book, GetBooksQuery.BooksViewModel>()
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()))
            .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishDate.Date.ToString("dd/MM/yyyy")))
            .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name + " " + src.Author.Surname))
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));

        // Map Genre entity to GenresViewModel for listing genres.
        CreateMap<Genre, GenresViewModel>();

        // Map Genre entity to GenreDetailViewModel for detailed genre queries.
        CreateMap<Genre, GenreDetailViewModel>();

        // Map Author entity to AuthorsViewModel for listing authors.
        CreateMap<Author, GetAuthorsQuery.AuthorsViewModel>()
            .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday.Date.ToString("dd/MM/yyyy")));

        // Map Author entity to AuthorDetailViewModel for detailed author queries.
        CreateMap<Author, GetAuthorDetailQuery.AuthorDetailViewModel>()
            .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday.Date.ToString("dd/MM/yyyy")));

        // Map UpdateAuthorViewModel to Author entity for updating author details.
        CreateMap<UpdateAuthorCommand.UpdateAuthorViewModel, Author>();

        // Map CreateAuthorViewModel to Author entity for creating a new author.
        CreateMap<CreateAuthorCommand.CreateAuthorViewModel, Author>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname))
            .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday));
    }
}

