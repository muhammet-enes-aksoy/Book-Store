using FluentValidation;

namespace BookStore.Api.BookOperations.DeleteBook;
public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
{
    public DeleteBookCommandValidator()
    {
        // DeleteBookCommand validation rules

        // check if book id is greater than 0
        RuleFor(command => command.BookId).GreaterThan(0)
            .WithMessage("Book Id must be greater than 0");
    }
}
