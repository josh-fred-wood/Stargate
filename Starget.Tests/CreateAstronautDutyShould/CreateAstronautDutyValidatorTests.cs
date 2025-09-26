using FluentValidation.TestHelper;
using StargateAPI.Business.Commands;
using Xunit;

namespace Stargate.Tests.CreateAstronautDutyShould
{
    public class CreateAstronautDutyValidatorTests
    {
        private readonly CreateAstronautDutyValidator _validator = new();

        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            var model = new CreateAstronautDuty { Name = "", Rank = "Captain", DutyTitle = "Pilot", DutyStartDate = DateTime.Now };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Pass_When_All_Fields_Are_Valid()
        {
            var model = new CreateAstronautDuty { Name = "John Doe", Rank = "Captain", DutyTitle = "Pilot", DutyStartDate = DateTime.Now };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

