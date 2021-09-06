using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Common.Exceptions;
using Notes.Application.Interfaces;
using Notes.Domain;

namespace Notes.Application.Notes.Commands.DeleteNote
{
    public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand>
    {
        private readonly INotesDbContext _notesDbContext;

        public DeleteNoteCommandHandler(INotesDbContext notesDbContext)
        {
            _notesDbContext = notesDbContext;
        }
        public async Task<Unit> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
        {
            var entity = await _notesDbContext
                .Notes
                .FirstOrDefaultAsync(note => note.Id == request.Id, cancellationToken);
            
            if (entity == null || entity.UserId != request.UserId) throw new NotFoundException(nameof(Note), request.Id);

            _notesDbContext.Notes.Remove(entity);
            await _notesDbContext.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}