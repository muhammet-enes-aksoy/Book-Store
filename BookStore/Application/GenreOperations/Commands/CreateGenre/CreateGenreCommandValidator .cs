using FluentValidation;

namespace BookStore.Application.GenreOperations.Commands.CreateGenre;

// Validator class for CreateGenreCommand using FluentValidation library.
public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
{
    // Constructor: Configures validation rules for CreateGenreCommand.
    public CreateGenreCommandValidator()
    {
        // Rule: The 'Name' property in the CreateGenreModel of CreateGenreCommand should not be empty.
        // This ensures that a valid and non-empty name is provided for the new genre.
        RuleFor(command => command.Model.Name).NotEmpty()
            .WithMessage("Genre name cannot be empty.");

        // Rule: The 'Name' property in the CreateGenreModel of CreateGenreCommand should have a minimum length of 4 characters.
        // This ensures that the genre name meets a minimum length requirement.
        RuleFor(command => command.Model.Name).MinimumLength(4)
            .WithMessage("Genre name must be at least 4 characters long.");
    }
}

