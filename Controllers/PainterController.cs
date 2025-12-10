
using Microsoft.AspNetCore.Mvc;
using MyGraphqlApp.Interface;
using MyGraphqlApp.Interface.IpaintService;
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


        [HttpPost("register")]
        public Task<Painter> RegisterPainter([FromBody] Painter painter)
        {
            var result = _ipaintService.RegisterPainter(painter);
            return result;
        }
    }

}