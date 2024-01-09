using FluentValidation;

namespace BookStore.BookOperations.UpdateBook;
public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidator()
    {
        // UpdateBookCommand validation rules

        // check if book id is greater than 0
        RuleFor(command => command.BookId).GreaterThan(0);

        // check if genre id is greater than 0
        RuleFor(command => command.Model.GenreId).GreaterThan(0);

        // check if page count is greater than 4
        RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4);
    }
}
