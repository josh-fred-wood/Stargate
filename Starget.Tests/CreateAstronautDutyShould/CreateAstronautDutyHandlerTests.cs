using Dapper;
using Moq;
using Moq.Dapper;
using StargateAPI.Business.Commands;
using StargateAPI.Business.Data;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;

namespace StargateAPI.Tests
{
    public class CreateAstronautDutyHandlerTests
    {
        private StargateContext GetDbContext(SqliteConnection connection)
        {
            var options = new DbContextOptionsBuilder<StargateContext>()
                .UseSqlite(connection)
                .Options;

            return new StargateContext(options);
        }

        [Fact]
        public async Task Handle_Should_Create_New_AstronautDuty()
        {
            // Arrange
            using var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            using var context = GetDbContext(connection);
            context.Database.EnsureCreated();

            // Seed Person
            context.People.Add(new Person { Id = 1, Name = "John Doe" });
            await context.SaveChangesAsync();

            var handler = new CreateAstronautDutyHandler(context);

            var command = new CreateAstronautDuty
            {
                Name = "John Doe",
                Rank = "Captain",
                DutyTitle = "Commander",
                DutyStartDate = new DateTime(2025, 10, 1)
            };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Id);

            var detail = await context.AstronautDetails.FirstOrDefaultAsync(ad => ad.PersonId == 1);
            Assert.NotNull(detail);
            Assert.Equal("Commander", detail.CurrentDutyTitle);
            Assert.Equal("Captain", detail.CurrentRank);

            var duty = await context.AstronautDuties.FirstOrDefaultAsync(d => d.PersonId == 1);
            Assert.NotNull(duty);
            Assert.Equal("Commander", duty.DutyTitle);
            Assert.Equal("Captain", duty.Rank);
        }

        [Fact]
        public async Task Handle_Should_Update_Existing_AstronautDuty()
        {
            // Arrange
            using var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            using var context = GetDbContext(connection);
            context.Database.EnsureCreated();

            // Seed Person, AstronautDetail, and previous duty
            context.People.Add(new Person { Id = 1, Name = "Jane Doe" });
            context.AstronautDetails.Add(new AstronautDetail
            {
                PersonId = 1,
                CurrentDutyTitle = "Pilot",
                CurrentRank = "Lieutenant",
                CareerStartDate = new DateTime(2020, 1, 1)
            });
            context.AstronautDuties.Add(new AstronautDuty
            {
                PersonId = 1,
                DutyTitle = "Test",
                Rank = "Lieutenant",
                DutyStartDate = new DateTime(2020, 1, 5),
                DutyEndDate = null
            });
            await context.SaveChangesAsync();

            var handler = new CreateAstronautDutyHandler(context);

            var command = new CreateAstronautDuty
            {
                Name = "Jane Doe",
                Rank = "Commander",
                DutyTitle = "Mission Lead",
                DutyStartDate = new DateTime(2025, 10, 1)
            };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Id);

            var updatedDetail = await context.AstronautDetails.FirstOrDefaultAsync(ad => ad.PersonId == 1);
            Assert.Equal("Mission Lead", updatedDetail.CurrentDutyTitle);
            Assert.Equal("Commander", updatedDetail.CurrentRank);

            var previousDuty = await context.AstronautDuties
                .FirstOrDefaultAsync(d => d.DutyTitle == "Pilot" && d.PersonId == 1);
            Assert.NotNull(previousDuty.DutyEndDate); // Previous duty was closed

            var newDuty = await context.AstronautDuties
                .FirstOrDefaultAsync(d => d.DutyTitle == "Mission Lead" && d.PersonId == 1);
            Assert.NotNull(newDuty);
            Assert.Null(newDuty.DutyEndDate); // New duty is active
        }
    }
}
