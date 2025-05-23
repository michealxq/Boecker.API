﻿using Boecker.Application.Clients.Commands.DeleteClient;
using Boecker.Application.FollowUp.Commands.ConfirmFollowUp;
using Boecker.Application.FollowUp.Commands.DeclineFollowUp;
using Boecker.Application.FollowUp.Commands.DeleteFollowUp;
using Boecker.Application.FollowUp.Dtos;
using Boecker.Application.FollowUp.Queries.GetAllFollowUps;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Boecker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowUpController(IMediator mediator) : ControllerBase
    {
        [HttpPost("{id}/confirm")]
        public async Task<IActionResult> ConfirmFollowUp(int id, CancellationToken cancellationToken)
        {
            await mediator.Send(new ConfirmFollowUpCommand(id), cancellationToken);
            return Ok("✅ Follow-up confirmed.");
        }

        [HttpPost("{id}/decline")]
        public async Task<IActionResult> DeclineFollowUp(int id, CancellationToken cancellationToken)
        {
            await mediator.Send(new DeclineFollowUpCommand(id), cancellationToken);
            return Ok("✅ Follow-up declined.");
        }

        [HttpGet]
        public async Task<ActionResult<List<FollowUpDto>>> GetAll(CancellationToken cancellationToken)
        {
            var followUps = await mediator.Send(new GetAllFollowUpsQuery(), cancellationToken);
            return Ok(followUps);
        }

        [HttpDelete("{id}")]
        //specifies the expected HTTP response status code to be not content or not found
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await mediator.Send(new DeleteFollowUpCommand(id));

            return NoContent();
        }
    }
}
