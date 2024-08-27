using Core.DTOs;
using FluentValidation;
using MediatR;
using Persistence.Models;

namespace Core.Features.Queries.AddTodoDetail
{
    public class AddTodoDetailQuery : IRequest<AddTodoDetailResponse>
    {
        public List<TodoDetailDto> TodoDetails { get; set; } // Added property for todo details
    }

    public class AddTodoDetailQueryValidator : AbstractValidator<AddTodoDetailQuery>
    {
        public AddTodoDetailQueryValidator()
        {
            RuleForEach(x => x.TodoDetails)
                .SetValidator(new TodoDetailValidator());
        }
    }

    public class TodoDetailValidator : AbstractValidator<TodoDetailDto>
    {
        public TodoDetailValidator()
        {
            RuleFor(x => x.Category)
                .Must(category => category.ToLower() == "task" || category.ToLower() == "daily activity")
                .WithMessage("Category must be either 'task' or 'daily activity'");
        }
    }
}
