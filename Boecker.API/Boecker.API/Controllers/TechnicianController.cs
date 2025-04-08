using Boecker.Application.Reporting.Interfaces;
using Boecker.Application.Technicians.Commands.CreateTechnician;
using Boecker.Application.Technicians.Commands.DeleteTechnician;
using Boecker.Application.Technicians.Commands.ToggleAvailability;
using Boecker.Application.Technicians.Queries.GetAllTechnicians;
using Boecker.Application.Technicians.Queries.GetTechnicianSchedules;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Boecker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechnicianController(IMediator mediator) : ControllerBase
    {
        [HttpPost("{id}/toggle-availability")]
        public async Task<IActionResult> ToggleAvailability(int id, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new ToggleAvailabilityCommand(id), cancellationToken);
            return result ? Ok("✅ Availability toggled.") : NotFound("Technician not found.");
        }

        [HttpGet("{id}/schedules")]
        public async Task<IActionResult> GetSchedules(int id, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetTechnicianSchedulesQuery(id), cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}/performance")]
        public async Task<IActionResult> GetPerformanceForTechnician(int id, [FromServices] IReportingRepository reportingRepo, CancellationToken cancellationToken)
        {
            var all = await reportingRepo.GetTechnicianPerformanceAsync(null, null, cancellationToken);
            var tech = all.FirstOrDefault(t => t.TechnicianId == id);
            return tech is not null ? Ok(tech) : NotFound("Technician not found or has no completed services.");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTechnicianCommand command)
        {
            var id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetAll), new { id }, id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await mediator.Send(new DeleteTechnicianCommand(id));
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var technicians = await mediator.Send(new GetAllTechniciansQuery());
            return Ok(technicians);
        }

    }
}
