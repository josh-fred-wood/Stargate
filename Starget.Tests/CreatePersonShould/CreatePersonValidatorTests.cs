using StargateAPI.Business.Commands;

namespace Starget.Tests.CreatePersonShould
{
    public class CreatePersonValidatorTests
    {
        [Fact]
        public void Validator_Should_Fail_When_Name_Is_Empty()
        {
            // Arrange
            var validator = new CreatePersonValidator();
            var command = new CreatePerson { Name = string.Empty };

            // Act
            var result = validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Name");
        }

        [Fact]
        public void Validator_Should_Pass_When_Name_Is_Not_Empty()
        {
            var validator = new CreatePersonValidator();
            var command = new CreatePerson { Name = "John Doe" };

            var result = validator.Validate(command);

            Assert.True(result.IsValid);
        }
    }
}
