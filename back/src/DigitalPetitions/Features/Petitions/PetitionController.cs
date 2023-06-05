using AutoMapper;
using DigitalPetitions.Common.Models;
using DigitalPetitions.Features.Petitions.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DigitalPetitions.Features.Petitions
{
    [Route("petitions")]
    [ApiController]
    public class PetitionController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PetitionController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        // GET: petitions
        [HttpGet]
        public async Task<ActionResult<PaginationResponse<PetitionDto>>> GetAll([FromQuery] GetPetitionsQuery query)
        {
            query.Role = User.FindFirst(ClaimTypes.Role)?.Value;

            var validator = new GetPetitionsValidator();
            var validationResults = await validator.ValidateAsync(query);

            if (validationResults.IsValid)
            {
                return await _mediator.Send(query);
            }

            return BadRequest();
        }

        // GET petitions/13
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PetitionDto>> GetById(int id)
        {
            var query = new GetPetitionByIdQuery() { Id = id, Role = User.FindFirst(ClaimTypes.Role)?.Value };

            var validator = new GetPetitionByIdValidator();
            var validationResults = await validator.ValidateAsync(query);

            if (validationResults.IsValid)
            {
                var responce = await _mediator.Send(query);

                if (responce == null)
                    return NotFound();

                return responce;
            }

            return BadRequest();
        }

        // POST petitions
        [HttpPost]
        public async Task<ActionResult<PetitionDto>> Create([FromBody] CreatePetitionCommand command)
        {
            var validator = new CreatePetitionValidator();
            var validationResults = await validator.ValidateAsync(command);

            if (validationResults.IsValid)
            {
                return await _mediator.Send(command);
            }

            return BadRequest();
        }

        // POST: petitions/13/sign
        [HttpPost("{id:int}/sign")]
        public async Task<ActionResult> Sign(int id, [FromBody] SignPetitionCommand command)
        {
            command.Id = id;

            var validator = new SignPetitionValidator();
            var validationResults = await validator.ValidateAsync(command);

            if (validationResults.IsValid)
            {
                var response = await _mediator.Send(command);

                if (response)
                    return Ok();
            }

            return BadRequest();
        }
    }
}
