using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;
 
namespace DotJwt.Controllers {

    [Route("~/api/[controller]")]
    [ApiController]
    public class GetNotes : ControllerBase {
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public ActionResult Index() {
            return Ok(new { message = "Hello user, welcome !" });
        }
    }
}
