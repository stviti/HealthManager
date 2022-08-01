using System;
using System.Threading.Tasks;
using Application.DTOs.SleepRecord;
using Application.Features.SleepRecord.Commands;
using Application.Features.SleepRecord.Queries;
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
    public class SleepRecordController : Controller
    {
        private readonly IMediator _mediator;

        public SleepRecordController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PaginationResponse<SleepRecordDto>>> Get([FromBody] PaginatedFilter paginatedFilter = default)
        {
            var command = new GetSleepRecordListRequest { PaginatedFilter = paginatedFilter };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SleepRecordDto>> Get([FromRoute] Guid id)
        {
            var command = new GetSleepRecordRequest { Id = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BaseCommandResponse>> Create([FromBody] CreateSleepRecordDto model)
        {
            var command = new CreateSleepRecordCommand { CreateDto = model };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BaseCommandResponse>> Update([FromBody] UpdateSleepRecordDto model)
        {
            var command = new UpdateSleepRecordCommand { UpdateDto = model };
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
            var command = new DeleteSleepRecordCommand { Id = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
