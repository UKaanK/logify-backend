using FluentAssertions;
using logifly.application.DTOs;
using logifly.application.Validators;
using logifly.persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logifly.tests.Validators
{
    public class TicketLogCreateDtoValidatorTests
    {
        [Fact]
        public async Task Should_Have_Error_When_LogType_Is_Invalid()
        {
            var options = new DbContextOptionsBuilder<LogiflyDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            using var dbContext = new LogiflyDbContext(options); // Gerekli parametre ile context oluşturuldu
            var validator = new TicketLogCreateDtoValidator(dbContext);
            var dto = new TicketLogCreateDto
            {
                TicketId = Guid.NewGuid(),
                LogType = "INVALID", // Geçersiz log tipi
                Content = "Test Log",
                CreatedBy = "Test User"
            };
            var result = await validator.ValidateAsync(dto);
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle();
        }
        [Fact]
        public async Task Should_Not_Have_Error_When_LogType_Is_Valid()
        {
            var options = new DbContextOptionsBuilder<LogiflyDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var dbContext = new LogiflyDbContext(options); // Gerekli parametre ile context oluşturuldu
            var validator = new TicketLogCreateDtoValidator(dbContext);
            var dto = new TicketLogCreateDto
            {
                TicketId = Guid.NewGuid(),
                LogType = "INFO", // Geçerli log tipi
                Content = "Test Log",
                CreatedBy = "Test User"
            };
            var result = await validator.ValidateAsync(dto);
            result.IsValid.Should().BeTrue();
        }
    }
}
