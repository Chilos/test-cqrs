using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Notes.Application.Interfaces;
using Notes.Domain;

namespace Notes.Application.Notes.Commands.CreateNote
{
    public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, Guid>
    {
        private readonly INotesDbContext _notesDbContext;

        public CreateNoteCommandHandler(INotesDbContext notesDbContext)
        {
            _notesDbContext = notesDbContext;
        }
        public async Task<Guid> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
        {
            var note = new Note
            {
                UserId = request.UserId,
                Title = request.Title,
                Detail = request.Detail,
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                EditDate = null
            };

            await _notesDbContext.Notes.AddAsync(note, cancellationToken);
            await _notesDbContext.SaveChangesAsync(cancellationToken);
            
            return note.Id;
        }
    }
}