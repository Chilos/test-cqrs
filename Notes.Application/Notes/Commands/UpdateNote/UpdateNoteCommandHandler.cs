using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Common.Exceptions;
using Notes.Application.Interfaces;
using Notes.Domain;

namespace Notes.Application.Notes.Commands.UpdateNote
{
    public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand>
    {
        private readonly INotesDbContext _notesDbContext;

        public UpdateNoteCommandHandler(INotesDbContext notesDbContext)
        {
            _notesDbContext = notesDbContext;
        }
        public async Task<Unit> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
        {
            var entity = await _notesDbContext
                .Notes
                .FirstOrDefaultAsync(note => note.Id == request.Id, cancellationToken);
            
            if (entity == null || entity.UserId != request.UserId) throw new NotFoundException(nameof(Note), request.Id);

            entity.Detail = request.Detail;
            entity.Title = request.Title;
            entity.EditDate = DateTime.Now;

            await _notesDbContext.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}