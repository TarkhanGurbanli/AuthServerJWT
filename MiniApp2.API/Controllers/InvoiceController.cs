using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

//Numune , onemli deyilin indi yazilan kodlar

namespace MiniApp2.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var userName = HttpContext.User.Identity.Name;

            //Id getirmek ucun
            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            ///Database den userId ve ya UserName uzerinden lazim olan datalari getir
            ///stockId, stockQuantuty, Category , UserId/UserName
            return Ok($"Invoice : => UserName : {userName}, UserId : {userIdClaim.Value}");

        }
    }
}
