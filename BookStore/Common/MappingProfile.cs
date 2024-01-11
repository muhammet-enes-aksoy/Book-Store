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
        // Map CreateBookModel to Book
        CreateMap<CreateBookCommand.CreateBookModel, Book>();

        // Map Book to BookDetailViewModel for detailed book queries
        CreateMap<Book, GetBookDetailQuery.BookDetailViewModel>()
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()))
            .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishDate.Date.ToString("dd/MM/yyyy")))
            .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name + " " + src.Author.Surname))
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));

        // Map Book to BooksViewModel for listing books
        CreateMap<Book, GetBooksQuery.BooksViewModel>()
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()))
            .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishDate.Date.ToString("dd/MM/yyyy")))
            .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name + " " + src.Author.Surname))
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));

        // Map Genre to GenresViewModel for listing genres
        CreateMap<Genre, GenresViewModel>();

        // Map Genre to GenreDetailViewModel for detailed genre queries
        CreateMap<Genre, GenreDetailViewModel>();

        // Map Author to AuthorsViewModel for listing authors
        CreateMap<Author, GetAuthorsQuery.AuthorsViewModel>()
            .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday.Date.ToString("dd/MM/yyyy")));

        // Map Author to AuthorDetailViewModel for detailed author queries
        CreateMap<Author, GetAuthorDetailQuery.AuthorDetailViewModel>()
            .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday.Date.ToString("dd/MM/yyyy")));

        // Map UpdateAuthorViewModel to Author for updating author details
        CreateMap<UpdateAuthorCommand.UpdateAuthorViewModel, Author>();

        // Map CreateAuthorViewModel to Author for creating a new author
        CreateMap<CreateAuthorCommand.CreateAuthorViewModel, Author>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname))
            .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday));
    }
}

