using BookStore.Application.GenreOperations.Queries.GetGenreDetail;
using FluentAssertions;

namespace BookStore.UnitTests.Application.GenreOperations.Queries.GetGenreDetail;
public class GetGenreDetailQueryValidatorTest
{
    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    [InlineData(-99999)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int genreId)
    {
        // Arrange
        var genreDetailQuery = new GetGenreDetailQuery(null, null)
        {
            GenreId = genreId
        };

        // Act 
        var validator = new GetGenreDetailQueryValidator();
        var result = validator.Validate(genreDetailQuery);

        // Assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    // Happy Path
    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
    {
        // Arrange
        var genreDetailQuery = new GetGenreDetailQuery(null, null)
        {
            GenreId = 1
        };

        // Act 
        GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
        var result = validator.Validate(genreDetailQuery);

        // Assert 
        result.Errors.Count.Should().Be(0);
    }
}