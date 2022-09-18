using System;
using System.Threading;
using System.Threading.Tasks;
using Application.DTOs.Treatment;
using Application.Features.Treatment.Commands;
using Application.Features.Treatment.Queries;
using Application.Models;
using Application.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TreatmentController : Controller
    {
        private readonly IMediator _mediator;

        public TreatmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PaginationResponse<TreatmentDto>>> Get([FromBody] PaginatedFilter paginatedFilter, CancellationToken cancellationToken)
        {
            var command = new GetTreatmentListRequest { PaginatedFilter = paginatedFilter };
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TreatmentDto>> Get([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var command = new GetTreatmentRequest { Id = id };
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BaseCommandResponse>> Create([FromBody] CreateTreatmentDto model, CancellationToken cancellationToken)
        {
            var command = new CreateTreatmentCommand { CreateDto = model };
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BaseCommandResponse>> Update([FromBody] UpdateTreatmentDto model, CancellationToken cancellationToken)
        {
            var command = new UpdateTreatmentCommand { UpdateDto = model };
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BaseCommandResponse>> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteTreatmentCommand { Id = id };
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

    }

}
