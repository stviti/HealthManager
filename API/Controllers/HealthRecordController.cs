using System;
using System.Threading.Tasks;
using Application.DTOs.HealthRecord;
using Application.Features.HealthRecord.Commands;
using Application.Features.HealthRecord.Queries;
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
    public class HealthRecordController : Controller
    {
        private readonly IMediator _mediator;

        public HealthRecordController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PaginationResponse<HealthRecordDto>>> Get([FromBody] PaginatedFilter paginatedFilter = default)
        {
            var command = new GetHealthRecordListRequest { PaginatedFilter = paginatedFilter };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HealthRecordDto>> Get([FromRoute] Guid id)
        {
            var command = new GetHealthRecordRequest { Id = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BaseCommandResponse>> Create([FromBody] CreateHealthRecordDto model)
        {
            var command = new CreateHealthRecordCommand { CreateDto = model };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BaseCommandResponse>> Update([FromBody] UpdateHealthRecordDto model)
        {
            var command = new UpdateHealthRecordCommand { UpdateDto = model };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BaseCommandResponse>> Delete([FromRoute] Guid id)
        {
            var command = new DeleteHealthRecordCommand { Id = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
