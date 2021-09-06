using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Notes.Commands.CreateNote;
using Notes.Tests.Commons;
using Xunit;

namespace Notes.Tests.Commands
{
    public class CreateNoteCommandHandlerTests: TestCommandBase
    {
        [Fact]
        public async Task CreateNoteCommandHandler_Success()
        {
            var handler = new CreateNoteCommandHandler(Context);
            var noteName = "Note name";
            var noteDetails = "Note details";

            var noteId = await handler.Handle(
                new CreateNoteCommand
                {
                    Title = noteName,
                    Detail = noteDetails,
                    UserId = NotesContextFactory.UserAId
                }, CancellationToken.None);
            
            Assert.NotNull(await Context.Notes.SingleOrDefaultAsync(note => 
                note.Id == noteId && note.Title == noteName && note.Detail == noteDetails));
        }
    }
}