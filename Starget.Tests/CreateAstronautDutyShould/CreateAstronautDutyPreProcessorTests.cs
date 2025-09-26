using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using StargateAPI.Business.Commands;
using StargateAPI.Business.Data;

namespace Starget.Tests.CreateAstronautDutyShould
{
    public class CreateAstronautDutyPreProcessorTests
    {
        [Fact]
        public async Task Should_Throw_When_Person_Not_Found()
        {
            var options = new DbContextOptionsBuilder<StargateContext>()
                .UseInMemoryDatabase("NoPersonDb")
                .Options;
            var context = new StargateContext(options);

            var preProcessor = new CreateAstronautDutyPreProcessor(context);
            var request = new CreateAstronautDuty { Name = "Unknown", Rank = "Captain", DutyTitle = "Pilot", DutyStartDate = DateTime.Now };

            await Assert.ThrowsAsync<BadHttpRequestException>(() => preProcessor.Process(request, default));
        }

        [Fact]
        public async Task Should_Throw_When_Duplicate_Duty_Exists()
        {
            var options = new DbContextOptionsBuilder<StargateContext>()
                .UseInMemoryDatabase("DuplicateDutyDb")
                .Options;
            var context = new StargateContext(options);

            context.People.Add(new Person { Id = 1, Name = "John" });
            context.AstronautDuties.Add(new AstronautDuty { PersonId = 1, DutyTitle = "Pilot", DutyStartDate = DateTime.Today });
            context.SaveChanges();

            var preProcessor = new CreateAstronautDutyPreProcessor(context);
            var request = new CreateAstronautDuty { Name = "John", Rank = "Captain", DutyTitle = "Pilot", DutyStartDate = DateTime.Today };

            await Assert.ThrowsAsync<BadHttpRequestException>(() => preProcessor.Process(request, default));
        }
    }
}

