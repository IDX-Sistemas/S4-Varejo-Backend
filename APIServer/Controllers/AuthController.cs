using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdxSistemas.AppRepository.Context;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

using IdxSistemas.AppRepository.Services;
using Microsoft.AspNetCore.Authorization;

namespace Projetos.Controllers
{
    
    public class Identity{
        public string Username { get; set; }
        public string Password { get; set; }
    }
    

    [Route("public/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataContext db;

        public AuthController(DataContext db) => this.db = db;

        [HttpGet("GetUserInfo")]
        public ActionResult GetUserInfo(int id)
        {
             if (@User.FindFirst(ClaimTypes.Sid).Value == null){
                 return NoContent();
            }
           
            return Ok(new {
                NomeCompleto = @User.FindFirst(ClaimTypes.Name).Value,
                UserId = @User.FindFirst(ClaimTypes.Sid).Value,
                Email = @User.FindFirst(ClaimTypes.Email).Value,
                Filial = "01",
                Role = "ALL"
            });
        }

       
        [HttpPost]
        [Route("Logon")]
        [AllowAnonymous]
        public IActionResult Logon([FromBody] Identity identity)
        {   
            try
            {
                var service = new UserService(this.db);
                var user = service.Authenticate(identity.Username, identity.Password);
        
                if (user == null)
                    return BadRequest(new { message = "Usuário ou senha inválidos" });

                var claimsIdentity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name    , user.Nome),
                    new Claim(ClaimTypes.Sid     , user.Nome),
                    new Claim(ClaimTypes.Email   , ""),
                    new Claim(ClaimTypes.GroupSid, "01"),
                    new Claim(ClaimTypes.Role    , "ALL")
                }, "Cookies");

                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                Request.HttpContext.SignInAsync("Cookies", claimsPrincipal).Wait();

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost]
        [Route("Logout")]
        [AllowAnonymous]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync("Cookies").Wait();
            return NoContent();
        }


    }
}