using System;
using System.Linq;
using IdxSistemas.AppRepository.Context;
using IdxSistemas.Models;

namespace IdxSistemas.AppRepository.Services
{
    public class UserService
    {
        private readonly DataContext db;

        public UserService(DataContext db) => this.db = db;

        public Usuario Authenticate(string username, string password)
        {
            var user = db.Usuarios.Where( x => x.Nome == username && x.Senha == password).FirstOrDefault();

            if (user == null)
                return null;

           return user;
        }
    }
}