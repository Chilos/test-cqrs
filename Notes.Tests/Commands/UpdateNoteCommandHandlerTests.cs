using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Common.Exceptions;
using Notes.Application.Notes.Commands.UpdateNote;
using Notes.Tests.Commons;
using Xunit;

namespace Notes.Tests.Commands
{
    public class UpdateNoteCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task UpdateNoteCommandHandler_Success()
        {
            var handler = new UpdateNoteCommandHandler(Context);
            var updatedTitle = "New title";
            var updatedDetail = "New Detail";

            await handler.Handle(
                new UpdateNoteCommand
                {
                    Id = NotesContextFactory.NoteIdForUpdate,
                    UserId = NotesContextFactory.UserBId,
                    Title = updatedTitle,
                    Detail = updatedDetail
                }, CancellationToken.None);
            
            Assert.NotNull(await Context.Notes.SingleOrDefaultAsync(note => 
                note.Id == NotesContextFactory.NoteIdForUpdate 
                && note.Title == updatedTitle 
                && note.Detail == updatedDetail));
        }
        
        [Fact]
        public async Task UpdateNoteCommandHandler_FailOnWrongId()
        {
            var handler = new UpdateNoteCommandHandler(Context);

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(
                new UpdateNoteCommand
                {
                    Id = Guid.NewGuid(),
                    UserId = NotesContextFactory.UserAId
                }, CancellationToken.None));
        }
        
        [Fact]
        public async Task UpdateNoteCommandHandler_FailOnWrongUserId()
        {
            var handler = new UpdateNoteCommandHandler(Context);

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(
                new UpdateNoteCommand
                {
                    Id = NotesContextFactory.NoteIdForUpdate,
                    UserId = NotesContextFactory.UserAId
                }, CancellationToken.None));
        }
    }
}