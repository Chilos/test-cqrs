using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Common.Exceptions;
using Notes.Application.Notes.Commands.DeleteNote;
using Notes.Tests.Commons;
using Xunit;

namespace Notes.Tests.Commands
{
    public class DeleteNoteCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task DeleteNoteCommandHandler_Success()
        {
            var handler = new DeleteNoteCommandHandler(Context);

            await handler.Handle(
                new DeleteNoteCommand
                {
                    Id = NotesContextFactory.NoteIdForDelete,
                    UserId = NotesContextFactory.UserAId
                }, CancellationToken.None);
            
            Assert.Null(await Context.Notes.SingleOrDefaultAsync(note => 
                note.Id == NotesContextFactory.NoteIdForDelete));
        }
        
        [Fact]
        public async Task DeleteNoteCommandHandler_FailOnWrongId()
        {
            var handler = new DeleteNoteCommandHandler(Context);

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(
                new DeleteNoteCommand
                {
                    Id = Guid.NewGuid(),
                    UserId = NotesContextFactory.UserAId
                }, CancellationToken.None));
        }
        
        [Fact]
        public async Task DeleteNoteCommandHandler_FailOnWrongUserId()
        {
            var handler = new DeleteNoteCommandHandler(Context);

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(
                new DeleteNoteCommand
                {
                    Id = NotesContextFactory.NoteIdForDelete,
                    UserId = NotesContextFactory.UserBId
                }, CancellationToken.None));
        }
    }
}