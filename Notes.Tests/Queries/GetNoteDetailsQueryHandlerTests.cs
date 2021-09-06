using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Notes.Application.Notes.Queries.GetNoteDetails;
using Notes.Persistence;
using Notes.Tests.Commons;
using Shouldly;
using Xunit;

namespace Notes.Tests.Queries
{
    [Collection("QueryCollection")]
    public class GetNoteDetailsQueryHandlerTests
    {
        private readonly NotesDbContext _context;
        private readonly IMapper _mapper;

        public GetNoteDetailsQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }
        
        [Fact]
        public async Task GetNoteDetailsQueryHandler_Success()
        {
            var handler = new GetNoteDetailsQueryHandler(_context, _mapper);

            var result = await handler.Handle(new GetNoteDetailsQuery
            {
                Id = Guid.Parse("AD91F412-B380-495E-9AD4-3F15853A165B"),
                UserId = NotesContextFactory.UserBId
            }, CancellationToken.None);
            
            result.ShouldBeOfType<NoteDetailsVm>();
            result.Title.ShouldBe("Title2");
            result.CreationDate.ShouldBe(DateTime.Today);
        }
    }
}