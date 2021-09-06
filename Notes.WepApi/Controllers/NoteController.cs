using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notes.Application.Notes.Commands.CreateNote;
using Notes.Application.Notes.Commands.DeleteNote;
using Notes.Application.Notes.Commands.UpdateNote;
using Notes.Application.Notes.Queries.GetNoteDetails;
using Notes.Application.Notes.Queries.GetNoteList;
using Notes.WepApi.Models;

namespace Notes.WepApi.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Produces("application/json")]
    [Route("api/{version:apiVersion}/[controller]/[action]")]
    public class NoteController: BaseController
    {
        private readonly IMapper _mapper;
        public NoteController(IMapper mapper) => _mapper = mapper;
        
        /// <summary>
        /// Gets the list of notes
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /note
        /// </remarks>
        /// <returns>Return <see cref="NoteListVm"/></returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public Task<NoteListVm> GetAll(CancellationToken cancellationToken)
        {
            var query = new GetNoteListQuery
            {
                UserId = UserId
            };
            
            return Mediator.Send(query, cancellationToken);
        }

        /// <summary>
        /// Gets the note by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /note/98C6CCFB-00A8-47B4-A23E-98BF9D5E6C5B
        /// </remarks>
        /// <param name="id">Note Id (guid)</param>
        /// <param name="cancellationToken">CancellationToken object</param>
        /// <returns>Return <see cref="NoteDetailsVm"/></returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public Task<NoteDetailsVm> Get(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetNoteDetailsQuery
            {
                UserId = UserId,
                Id = id
            };
            
            return Mediator.Send(query, cancellationToken);
        }

        /// <summary>
        /// Creates the note
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /note
        /// {
        ///     title: "note title",
        ///     details: "note details"
        /// }
        /// </remarks>
        /// <param name="dto">CreateNoteDto object</param>
        /// <param name="cancellationToken">CancellationToken object</param>
        /// <returns>Return id created note</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public Task<Guid> Create([FromBody] CreateNoteDto dto, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreateNoteCommand>(dto);
            command.UserId = UserId;
            
            return Mediator.Send(command, cancellationToken);
        }
        
        /// <summary>
        /// Updates the note
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /note
        /// {
        ///     id: "D4B3020F-F6F8-45ED-8B53-ACE8A75A5BFD"
        ///     title: "note title",
        ///     details: "note details"
        /// }
        /// </remarks>
        /// <param name="dto">UpdateNoteDto object</param>
        /// <param name="cancellationToken">CancellationToken object</param>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update([FromBody] UpdateNoteDto dto, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<UpdateNoteCommand>(dto);
            command.UserId = UserId;
            await Mediator.Send(command, cancellationToken);

            return NoContent();
        }

        /// <summary>
        /// Deletes the note
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /note/98C6CCFB-00A8-47B4-A23E-98BF9D5E6C5B
        /// </remarks>
        /// <param name="id">Note Id (guid)</param>
        /// <param name="cancellationToken">CancellationToken object</param>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteNoteCommand
            {
                UserId = UserId,
                Id = id
            };
            await Mediator.Send(command, cancellationToken);
            
            return NoContent();
        }
    }
}