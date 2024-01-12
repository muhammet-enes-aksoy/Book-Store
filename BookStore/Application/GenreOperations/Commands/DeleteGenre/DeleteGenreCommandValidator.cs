using FluentValidation;

namespace BookStore.Application.GenreOperations.Commands.DeleteGenre;
// Validator class for DeleteGenreCommand using FluentValidation library.
public class DeleteGenreCommandValidator : AbstractValidator<DeleteGenreCommand>
{
    // Constructor: Configures validation rules for DeleteGenreCommand.
    public DeleteGenreCommandValidator()
    {
        // Rule: The 'GenreId' property in the DeleteGenreCommand should be greater than 0.
        // This ensures that a valid and positive genre ID is provided for the deletion.
        RuleFor(command => command.GenreId).GreaterThan(0)
            .WithMessage("Invalid genre ID. Please provide a valid genre ID greater than 0.");
    }
}

