using logifly.application.DTOs;
using logifly.application.Services;
using logifly.domain.Enums;
using logifly.tests.TestHelpers;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.InMemory.Storage.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace logifly.tests.Services
{
    public class TicketLogServiceTests
    {
        [Fact]
        public async Task AddAsync_ValidRequest_AddsLog()
        {
            //Arrange
            var context = InMemoryDbContextFactory.Create();
            context.Tickets.Add(new domain.Entities.Ticket { Id = Guid.NewGuid(), Title = "Test Ticket", Status = domain.Enums.TicketStatus.Pending, CreatedBy = "test@gmail.com", CreatedAt = DateTime.UtcNow, Message = "Test Verisi" });
            await context.SaveChangesAsync();

            var service = new TicketLogService(context);
            var dto = new TicketLogCreateDto
            {
                TicketId = context.Tickets.First().Id,
                LogType = "INFO",
                Content = "Test Log",
                CreatedBy = "Test User"
            };

            //Act
            await service.AddAsync(dto);

            //Assert
            var log = await context.TicketLogs.FirstOrDefaultAsync();
            log.Should().NotBeNull();
            log!.LogType.Should().Be(TicketLogType.INFO);
            log.Content.Should().Be("Test Log");
        }

        [Fact]
        public async Task AddAsync_InvalidLogType_ThrowsArgumentException()
        {
            var context = InMemoryDbContextFactory.Create();
            context.Tickets.Add(new domain.Entities.Ticket { Id = Guid.NewGuid(), Title = "Test Ticket", Status = domain.Enums.TicketStatus.Pending, CreatedBy = "Test User" });
            await context.SaveChangesAsync();
            var service = new TicketLogService(context);
            var dto = new TicketLogCreateDto
            {
                TicketId = context.Tickets.First().Id,
                LogType = "INVALID", // Geçersiz log tipi
                Content = "Test Log",
                CreatedBy = "Test User"
            };
            //Act & Assert
            await Assert.ThrowsAsync<FluentValidation.ValidationException>(async () => await service.AddAsync(dto));
        }
        [Fact]
        public async Task AddAsync_TicketNotExists_ThrowsArgumentException()
        {
            var context = InMemoryDbContextFactory.Create();
            var service = new TicketLogService(context);
            var dto = new TicketLogCreateDto
            {
                TicketId = Guid.NewGuid(), // Mevcut olmayan bir TicketId
                LogType = "INFO",
                Content = "Test Log",
                CreatedBy = "Test User"
            };
            //Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await service.AddAsync(dto));
        }

    }
}
