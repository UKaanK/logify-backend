using FluentValidation;
using logifly.application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logifly.application.Validators
{
    public class TicketCreateDtoValidator:AbstractValidator<TicketCreateDto>
    {
        public TicketCreateDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title Is Required").MaximumLength(200).WithMessage("Title cannot exceed 200 characters");
            RuleFor(x => x.Message).NotEmpty().WithMessage("Message is required");
            RuleFor(x => x.CreatedBy).NotEmpty().WithMessage("CreatedBy is required");
        }
    }
}
