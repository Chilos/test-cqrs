using System;
using FluentValidation;

namespace Notes.Application.Notes.Commands.DeleteNote
{
    public class DeleteNoteCommandValidator : AbstractValidator<DeleteNoteCommand>
    {
        public DeleteNoteCommandValidator()
        {
            RuleFor(command => command.UserId).NotEqual(Guid.Empty);
            RuleFor(command => command.Id).NotEqual(Guid.Empty);
        }
    }
}