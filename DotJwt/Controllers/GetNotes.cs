using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;
 
namespace DotJwt.Controllers {

    [Route("~/api/[controller]")]
    [ApiController]
    public class GetNotes : ControllerBase {
        [Authorize(Roles = "ADMIN,USER")]
        [HttpGet("/getNote")]
        public ActionResult Index() {
            return Ok(new { message = "Hello user or admin, welcome !" });
        }

		[Authorize(Roles = "ADMIN")]
		[HttpGet("/admin")]
		public ActionResult Admin()
		{
			return Ok(new { message = "Hello Admin, welcome !" });
		}
	}
}
