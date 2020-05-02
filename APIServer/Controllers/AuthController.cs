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
using IdxSistemas.AppServer;
using Microsoft.AspNetCore.Http;

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
        public ActionResult GetUserInfo(string id)
        {
            if (@User.FindFirst(ClaimTypes.Sid).Value == null){
                return NoContent();
            }
           
            return Ok(new {
                NomeCompleto = @User.FindFirst(ClaimTypes.Name).Value,
                UserId = @User.FindFirst(ClaimTypes.Sid).Value,
                //Email = @User.FindFirst(ClaimTypes.Email).Value,
                //Filial = "01",
                Role = @User.FindFirst(ClaimTypes.Role).Value
            });
        }

       
        [HttpPost]
        [Route("Logon")]
        [AllowAnonymous]
        public IActionResult Logon([FromBody] Identity identity)
        {   
            try
            {
                var userToken = new TokenProvider(this.db).LoginUser(identity.Username, identity.Password);
                

                if ( string.IsNullOrEmpty(  userToken ))
                    return BadRequest(new { message = "Usuário ou senha inválidos" });


                HttpContext.Session.SetString("JWToken", userToken);
              
                return Ok(userToken);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return BadRequest(ModelState);
            }
        }


        [HttpPost]
        [Route("Logout")]
        [AllowAnonymous]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return NoContent();
        }


    }
}