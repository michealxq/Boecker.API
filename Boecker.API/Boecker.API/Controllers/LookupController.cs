using Boecker.Application.Lookup.Dtos;
using Boecker.Application.Lookup.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Boecker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LookupController : ControllerBase
    {
        private readonly ISender _mediator;

        public LookupController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<LookupDto>> Get(string name)
        {
            var result = await _mediator.Send(new GetLookupByNameQuery(name));
            return result == null ? NotFound() : Ok(result);
        }
    }

}
