using FluentValidation;
using logifly.application.DTOs;
using logifly.persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logifly.application.Validators
{
    public class TicketLogCreateDtoValidator:AbstractValidator<TicketLogCreateDto>
    {
        private readonly LogiflyDbContext _context;

        public TicketLogCreateDtoValidator(LogiflyDbContext context)
        {
            _context = context;
            RuleFor(x => x.TicketId).NotEmpty().WithMessage("TicketId is required");
            RuleFor(x => x.Content).NotEmpty().WithMessage("Content is required");
            RuleFor(x => x.LogType).NotEmpty().WithMessage("LogType is required").Must(logtype => new[]
            {
                "INFO","WARNING","ERROR"
            }.Contains(logtype.ToUpper())).WithMessage("LogType must be INFO,WARNING,ERROR");
        }
       
    }
}
