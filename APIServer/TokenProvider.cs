using IdxSistemas.AppRepository.Context;
using IdxSistemas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace IdxSistemas.AppServer
{
    public class TokenProvider
    {
        private readonly DataContext db;

        public TokenProvider(DataContext db) => this.db = db;

        public string LoginUser(string UserID, string Password)
        {
            var user = db.Usuarios.SingleOrDefault(x => x.Nome == UserID);

            if (user == null)
                return null;

            if (Password == user.Senha)
            {
                //Provide the security key which was given in the JWToken configuration in Startup.cs
                var SecretKey = Encoding.ASCII.GetBytes
                    ("IdxTecKey-2374-OFFKDI940NG7:56753253-tyuw-5769-0921-kfirox29zoxv");
                
                //Generate Token for user 
                var JWToken = new JwtSecurityToken(
                    issuer: "http://localhost:5000/",
                    audience: "http://localhost:5000/",
                    claims: GetUserClaims(user),
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                    expires: new DateTimeOffset(DateTime.Now.AddDays(1)).DateTime,
                    //Using HS256 Algorithm to encrypt Token
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(SecretKey),
                                        SecurityAlgorithms.HmacSha256Signature)
                );
                var token = new JwtSecurityTokenHandler().WriteToken(JWToken);
                return token;
            }
            else
            {
                return null;
            }
        }

        private IEnumerable<Claim> GetUserClaims(Usuario user)
        {
            IEnumerable<Claim> claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Nome),
                new Claim(ClaimTypes.Sid, user.Nome),
                new Claim(ClaimTypes.Role, user.UsuarioFuncaoId ?? "ALL")
            };

            return claims;
        }
    }
}
