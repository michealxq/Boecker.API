using Boecker.Application.Contracts.Commands.ConfirmContract;
using Boecker.Application.Contracts.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Boecker.Application.Contracts.Queries.GetAllContracts;
using Boecker.Application.Contracts.Queries.GetContractById;
using Boecker.Application.Contracts.Commands.DeclineContract;
using Boecker.Application.FollowUp.Commands.DeleteFollowUp;
using Boecker.Application.Contracts.Commands.DeleteContacts;

namespace Boecker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateContract(CreateContractCommand command, CancellationToken cancellationToken)
        {
            var contractId = await mediator.Send(command, cancellationToken);
            return Ok(contractId);
        }

        [HttpPost("{id}/confirm")]
        public async Task<IActionResult> Confirm(int id, CancellationToken cancellationToken)
        {
            var success = await mediator.Send(new ConfirmContractCommand(id), cancellationToken);
            return success ? Ok("Contract confirmed.") : NotFound("Contract not found.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var contract = await mediator.Send(new GetContractByIdQuery(id), cancellationToken);
            return contract is not null ? Ok(contract) : NotFound("Contract not found.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var contracts = await mediator.Send(new GetAllContractsQuery(), cancellationToken);
            return Ok(contracts);
        }

        [HttpPost("{id}/decline")]
        public async Task<IActionResult> Decline(int id, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new DeclineContractCommand(id));
            return result ? Ok("Contract declined.") : NotFound("Contract not found.");
        }


        [HttpDelete("{id}")]
        //specifies the expected HTTP response status code to be not content or not found
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await mediator.Send(new DeleteContactsCommand(id));

            return NoContent();
        }
    }
}
