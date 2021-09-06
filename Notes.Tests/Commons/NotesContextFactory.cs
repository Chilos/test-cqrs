using System;
using Microsoft.EntityFrameworkCore;
using Notes.Persistence;
using Notes.Domain;

namespace Notes.Tests.Commons
{
    public class NotesContextFactory
    {
        public static Guid UserAId = Guid.NewGuid();
        public static Guid UserBId = Guid.NewGuid();
        
        public static Guid NoteIdForDelete = Guid.NewGuid();
        public static Guid NoteIdForUpdate = Guid.NewGuid();

        public static NotesDbContext Create()
        {
            var options = new DbContextOptionsBuilder<NotesDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new NotesDbContext(options);
            context.Database.EnsureCreated();
            context.Notes.AddRange(
                    new Note
                    {
                        CreationDate = DateTime.Today,
                        Detail = "Details1",
                        EditDate = null,
                        Id = Guid.Parse("6AA65E46-53F8-400E-90C3-590CC874877D"),
                        Title = "Title1",
                        UserId = UserAId
                    },
                    new Note
                    {
                        CreationDate = DateTime.Today,
                        Detail = "Details2",
                        EditDate = null,
                        Id = Guid.Parse("AD91F412-B380-495E-9AD4-3F15853A165B"),
                        Title = "Title2",
                        UserId = UserBId
                    },
                    new Note
                    {
                        CreationDate = DateTime.Today,
                        Detail = "Details3",
                        EditDate = null,
                        Id = NoteIdForDelete,
                        Title = "Title3",
                        UserId = UserAId
                    },
                    new Note
                    {
                        CreationDate = DateTime.Today,
                        Detail = "Details4",
                        EditDate = null,
                        Id = NoteIdForUpdate,
                        Title = "Title4",
                        UserId = UserBId
                    }
                );
            context.SaveChanges();
            return context;
        }

        public static void Destroy(NotesDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}