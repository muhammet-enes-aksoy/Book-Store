using BookStore.Application.AuthorOperations.Commands.CreateAuthor;
using FluentAssertions;

namespace BookStore.UnitTests.Application.AuthorOperations.Commands.CreateAuthor;

// Test class for validating CreateAuthorCommand inputs
public class CreateAuthorCommandValidatorTest
{
    // Test cases for invalid inputs
    [Theory]
    [InlineData("", "")]
    [InlineData("S", "LEM")]
    [InlineData("STAN", "LE")]
    public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string name, string surname)
    {
        // Arrange 
        // Create a CreateAuthorCommand with invalid input
        var command = new CreateAuthorCommand(null, null)
        {
            Model = new CreateAuthorCommand.CreateAuthorViewModel()
            {                
                Name = name,
                Surname = surname,
                Birthday = DateTime.Now.Date.AddYears(-40)
            }
        };

        // Act
        // Execute the validator
        var validator = new CreateAuthorCommandValidator();
        var result = validator.Validate(command);

        // Assert 
        // Verify that errors are returned
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    // Test case for when DateOfBirth is equal to DateTime.Now.Date
    [Fact]
    public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
    {
        // Arrange 
        // Create a CreateAuthorCommand with DateOfBirth equal to DateTime.Now.Date
        var command = new CreateAuthorCommand(null, null)
        {
            Model = new CreateAuthorCommand.CreateAuthorViewModel()
            {
                Name = "Test Firstname",
                Surname = "Test Lastname",
                Birthday = DateTime.Now.Date
            }
        };

        // Act 
        // Execute the validator
        CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
        var result = validator.Validate(command);

        // Assert 
        // Verify that errors are returned
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    // Test case for valid inputs
    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
    {
        // Arrange
        // Create a CreateAuthorCommand with valid input
        CreateAuthorCommand command = new CreateAuthorCommand(null, null);
        command.Model = new CreateAuthorCommand.CreateAuthorViewModel
        {
            Name = "Firstname",
            Surname = "Lastname",
            Birthday = DateTime.Now.Date.AddYears(-40)
        };

        // Act 
        // Execute the validator
        var validator = new CreateAuthorCommandValidator();
        var result = validator.Validate(command);

        // Assert 
        // Verify that no errors are returned
        result.Errors.Count.Should().Be(0);
    }
}

