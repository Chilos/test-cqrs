using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Notes.Application.Notes.Queries.GetNoteList;
using Notes.Persistence;
using Notes.Tests.Commons;
using Shouldly;
using Xunit;

namespace Notes.Tests.Queries
{
    [Collection("QueryCollection")]
    public class GetNoteListQueryHandlerTests
    {
        private readonly NotesDbContext _context;
        private readonly IMapper _mapper;

        public GetNoteListQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }
        
        [Fact]
        public async Task GetNoteListQueryHandler_Success()
        {
            var handler = new GetNoteListQueryHandler(_context, _mapper);

            var result = await handler.Handle(new GetNoteListQuery
            {
                UserId = NotesContextFactory.UserBId
            }, CancellationToken.None);
            
            result.ShouldBeOfType<NoteListVm>();
            result.Notes.Count.ShouldBe(2);
            
        }
    }
}