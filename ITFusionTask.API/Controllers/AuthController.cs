using ITFusionTask.API.ApiServicesClasses;
using ITFusionTask.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ITFusionTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly SignInManager<User> _signInManager;

        public AuthController(IJwtTokenService jwtTokenService, SignInManager<User> signInManager)
        {
            _jwtTokenService = jwtTokenService;
            _signInManager = signInManager;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromForm] string UserName, [FromForm] string password)
        {
            var result = await _signInManager.PasswordSignInAsync(UserName, password, true, false);
            if (result.Succeeded)
            {
                var token = _jwtTokenService.GenerateToken(UserName);
                return new JsonResult(new { Succeded = true, Message = "Authorized", Token = token });
            }
            return new JsonResult(new { Succeded = false, Message = "Not Authorized", Token = "" });
        }

    }
}
