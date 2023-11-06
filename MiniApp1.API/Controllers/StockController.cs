using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

//Numune Microservice

namespace MiniApp1.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetStock()
        {
            var userName = HttpContext.User.Identity.Name;

            //Id getirmek ucun
            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            ///Database den userId ve ya UserName uzerinden lazim olan datalari getir
            ///stockId, stockQuantuty, Category , UserId/UserName
            return Ok($"Stock : => UserName : {userName}, UserId : {userIdClaim.Value}");

        }
    }
}
