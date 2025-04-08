using Boecker.Application.FollowUp.Commands.ConfirmFollowUp;
using Boecker.Application.FollowUp.Commands.DeclineFollowUp;
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
    }
}
