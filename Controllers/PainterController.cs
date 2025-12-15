
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyGraphqlApp.Exception.UserException;
using MyGraphqlApp.Model;

namespace MyGraphqlApp.Controllers.PainterController
{



    [ApiController]
    [Route("api/painter")]
    public class PainterController : ControllerBase
    {

        private readonly IpaintService _ipaintService;
        private readonly JwtUtils _jwtUtils;


        public PainterController(IpaintService ipaintService, JwtUtils jwtUtils)
        {
            _ipaintService = ipaintService;
            _jwtUtils = jwtUtils;
        }

        [Authorize]
        [HttpPost("register")]
        public Task<Painter> RegisterPainter([FromBody] Painter painter)
        {
            var userDetails = _jwtUtils.ExtractLoggedInUserDetails();
            if (userDetails.Role != 1)
            {
                throw new UserException("Unauthorize access , you are not Authroize to this resourse",System.Net.HttpStatusCode.Unauthorized);
            }
            var result = _ipaintService.RegisterPainter(painter);
            return result;
        }
    }

}