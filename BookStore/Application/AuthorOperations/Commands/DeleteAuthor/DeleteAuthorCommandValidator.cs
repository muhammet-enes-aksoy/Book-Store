using FluentValidation;

namespace BookStore.Application.AuthorOperations.Commands.DeleteAuthor;
// Validator class for DeleteAuthorCommand using FluentValidation library.
public class DeleteAuthorCommandValidator : AbstractValidator<DeleteAuthorCommand>
{
    // Constructor: Configures validation rules for DeleteAuthorCommand.
    public DeleteAuthorCommandValidator()
    {
        // Rule: The 'AuthorId' property in DeleteAuthorCommand should be greater than 0.
        // This ensures that a valid and positive AuthorId is provided for the delete operation.
        RuleFor(command => command.AuthorId).GreaterThan(0);
    }
}