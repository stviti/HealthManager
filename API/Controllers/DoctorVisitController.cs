using System;
using System.Threading.Tasks;
using Application.DTOs.DoctorVisit;
using Application.Features.DoctorVisit.Commands;
using Application.Features.DoctorVisit.Queries;
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
    public class DoctorVisitController : Controller
    {
        private readonly IMediator _mediator;

        public DoctorVisitController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PaginationResponse<DoctorVisitDto>>> Get([FromBody] PaginatedFilter paginatedFilter = default)
        {
            var command = new GetDoctorVisitListRequest { PaginatedFilter = paginatedFilter };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DoctorVisitDto>> Get([FromRoute] Guid id)
        {
            var command = new GetDoctorVisitRequest { Id = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BaseCommandResponse>> Create([FromBody] CreateDoctorVisitDto model)
        {
            var command = new CreateDoctorVisitCommand { CreateDto = model };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BaseCommandResponse>> Update([FromBody] UpdateDoctorVisitDto model)
        {
            var command = new UpdateDoctorVisitCommand { UpdateDto = model };
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
            var command = new DeleteDoctorVisitCommand { Id = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
