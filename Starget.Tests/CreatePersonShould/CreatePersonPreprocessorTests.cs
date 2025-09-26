using StargateAPI.Business.Commands;
using StargateAPI.Business.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Starget.Tests.CreatePersonShould
{
    public class CreatePersonPreprocessorTests
    {
        [Fact]
        public async Task PreProcessor_Should_Throw_When_Person_Exists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<StargateContext>()
                .UseInMemoryDatabase(databaseName: "PreProcessorTestDb1")
                .Options;

            await using var context = new StargateContext(options);
            context.People.Add(new Person { Name = "Existing" });
            await context.SaveChangesAsync();

            var preProcessor = new CreatePersonPreProcessor(context);

            var command = new CreatePerson { Name = "Existing" };

            // Act & Assert
            await Assert.ThrowsAsync<BadHttpRequestException>(() => preProcessor.Process(command, CancellationToken.None));
        }

        [Fact]
        public async Task PreProcessor_Should_Not_Throw_When_Person_Does_Not_Exist()
        {
            var options = new DbContextOptionsBuilder<StargateContext>()
                .UseInMemoryDatabase(databaseName: "PreProcessorTestDb2")
                .Options;

            await using var context = new StargateContext(options);

            var preProcessor = new CreatePersonPreProcessor(context);

            var command = new CreatePerson { Name = "New Person" };

            var exception = await Record.ExceptionAsync(() => preProcessor.Process(command, CancellationToken.None));

            Assert.Null(exception);
        }
    }
}
