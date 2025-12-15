
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyGraphqlApp.Model;

namespace MyGraphqlApp.Controllers.PainterController
{



    [ApiController]
    [Route("api/painter")]
    public class PainterController : ControllerBase
    {

        private readonly IpaintService _ipaintService;


        public PainterController(IpaintService ipaintService)
        {
            _ipaintService = ipaintService;
        }

        [Authorize]
        [HttpPost("register")]
        public Task<Painter> RegisterPainter([FromBody] Painter painter)
        {
            var result = _ipaintService.RegisterPainter(painter);
            return result;
        }
    }

}