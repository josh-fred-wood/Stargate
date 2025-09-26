using StargateAPI.Business.Commands;
using StargateAPI.Business.Data;
using Microsoft.EntityFrameworkCore;

namespace Starget.Tests.CreatePersonShould
{
    public class CreatePersonHandlerTests
    {
        [Fact]
        public async Task Handler_Should_Create_Person()
        {
            var options = new DbContextOptionsBuilder<StargateContext>()
                .UseInMemoryDatabase(databaseName: "HandlerTestDb")
                .Options;

            await using var context = new StargateContext(options);
            var handler = new CreatePersonHandler(context);

            var command = new CreatePerson { Name = "New Person" };

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result.Id > 0);

            var createdPerson = context.People.FirstOrDefault(p => p.Id == result.Id);
            Assert.NotNull(createdPerson);
            Assert.Equal("New Person", createdPerson.Name);
        }
    }
}
