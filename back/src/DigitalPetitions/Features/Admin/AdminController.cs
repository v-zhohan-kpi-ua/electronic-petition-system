using DigitalPetitions.Features.Admin.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DigitalPetitions.Features.Admin
{
    [Route("admin")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: admin/petitions/13/mod
        [HttpPost("petitions/{id:int}/mod")]
        public async Task<ActionResult<ModerationResultDto>> ModeratePetition(int id, [FromBody] ModeratePetitionCommand command)
        {
            command.Id = id;
            command.ModeratorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var validator = new ModeratePetitionValidator();
            var validationResults = await validator.ValidateAsync(command);

            if (validationResults.IsValid)
            {
                var response = await _mediator.Send(command);

                if (response != null)
                {
                    return response;
                }
            }

            return BadRequest();
        }

        // POST: admon/petitions/13/answer
        [HttpPost("petitions/{id:int}/answer")]
        public async Task<ActionResult<AnswerDto>> AnswerPetition(int id, [FromBody] AnswerPetitionCommand command)
        {
            command.Id = id;
            command.PublisherAdminId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var validator = new AnswerPetitionValidator();
            var validationResults = await validator.ValidateAsync(command);

            if (validationResults.IsValid)
            {
                var response = await _mediator.Send(command);

                if (response != null)
                {
                    return response;
                }
            }

            return BadRequest();
        }

        [HttpGet("ping")]
        public async Task<ActionResult> Ping()
        {
            return Ok();
        }
    }
}
