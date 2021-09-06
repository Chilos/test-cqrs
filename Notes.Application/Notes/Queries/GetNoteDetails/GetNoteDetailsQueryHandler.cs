using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Common.Exceptions;
using Notes.Application.Interfaces;
using Notes.Domain;

namespace Notes.Application.Notes.Queries.GetNoteDetails
{
    public class GetNoteDetailsQueryHandler : IRequestHandler<GetNoteDetailsQuery, NoteDetailsVm>
    {
        private readonly INotesDbContext _notesDbContext;
        private readonly IMapper _mapper;

        public GetNoteDetailsQueryHandler(INotesDbContext notesDbContext, IMapper mapper) =>
            (_notesDbContext, _mapper) = (notesDbContext, mapper);

        public async Task<NoteDetailsVm> Handle(GetNoteDetailsQuery request, CancellationToken cancellationToken)
        {
            var entity = await _notesDbContext
                    .Notes
                    .FirstOrDefaultAsync(note => note.Id == request.Id, cancellationToken);

            if (entity == null || entity.UserId != request.UserId)
                throw new NotFoundException(nameof(Note), request.Id);

            return _mapper.Map<NoteDetailsVm>(entity);
        }
    }
}