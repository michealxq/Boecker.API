using Boecker.Application.FollowUp.Commands.ConfirmFollowUp;
using Boecker.Application.FollowUp.Commands.DeclineFollowUp;
using Boecker.Application.ServiceSchedules.Commands.AssignTechnician;
using Boecker.Application.ServiceSchedules.Commands.CreateServiceSchedule;
using Boecker.Application.ServiceSchedules.Commands.DeleteServiceSchedule;
using Boecker.Application.ServiceSchedules.Commands.UpdateServiceSchedule;
using Boecker.Application.ServiceSchedules.Queries.GetAllServiceSchedules;
using Boecker.Application.ServiceSchedules.Queries.GetFilteredServiceSchedules;
using Boecker.Application.ServiceSchedules.Queries.GetServiceScheduleById;
using Boecker.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Boecker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceScheduleController(IMediator mediator) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateServiceScheduleCommand command)
        {
            var id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        // ✅ Assign a technician
        [HttpPost("{id}/assign-technician")]
        public async Task<IActionResult> AssignTechnician(int id, [FromBody] int technicianId, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new AssignTechnicianCommand(id, technicianId), cancellationToken);
            return result
                ? Ok("✅ Technician assigned.")
                : BadRequest("❌ Assignment failed. Either technician/schedule doesn't exist or is unavailable.");
        }

        // ✅ List all service schedules
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var schedules = await mediator.Send(new GetAllServiceSchedulesQuery(), cancellationToken);
            return Ok(schedules);
        }

        // ✅ Filter service schedules
        [HttpGet("filter")]
        public async Task<IActionResult> FilterSchedules(
            [FromQuery] int? technicianId,
            [FromQuery] DateOnly? dateScheduled,
            [FromQuery] ScheduleStatus? status,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetFilteredServiceSchedulesQuery(technicianId, dateScheduled, status), cancellationToken);
            return Ok(result);
        }

        // ✅ Confirm follow-up
        [HttpPost("{id}/confirm")]
        public async Task<IActionResult> Confirm(int id)
        {
            await mediator.Send(new ConfirmFollowUpCommand(id));
            return Ok(new { message = "Follow-up confirmed and service schedule created." });
        }

        // ✅ Decline follow-up
        [HttpPost("{id}/decline")]
        public async Task<IActionResult> Decline(int id)
        {
            await mediator.Send(new DeclineFollowUpCommand(id));
            return Ok(new { message = "Follow-up declined." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateServiceScheduleCommand command)
        {
            if (id != command.Id) return BadRequest("ID mismatch.");
            await mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await mediator.Send(new DeleteServiceScheduleCommand { Id = id});
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await mediator.Send(new GetServiceScheduleByIdQuery(id));
            return result is null ? NotFound() : Ok(result);
        }


    }

}
