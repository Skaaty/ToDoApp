﻿using FluentValidation;
using TodoApi.DTOs;

namespace TodoApi.Validation
{
    public class CreateTaskListValidator : AbstractValidator<CreateTaskListDto>
    {
        public CreateTaskListValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .MaximumLength(300);
        }
    }
}
