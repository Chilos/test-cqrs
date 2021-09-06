using System.Collections.Generic;

namespace Notes.Application.Notes.Queries.GetNoteList
{
    public class NoteListVm
    {
        public IReadOnlyList<NoteLookupDto> Notes { get; set; }
    }
}