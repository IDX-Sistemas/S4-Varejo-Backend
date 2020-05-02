using IdxSistemas.AppRepository.Context;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Services
{
    public class AplicativoService
    {
        private readonly DataContext db;

        public AplicativoService(DataContext db)
        {
            this.db = db;
        }

        public string getTitulo(string codigo)
        {
            var aplicativo = db.Aplicativos.Where(x => x.Codigo == codigo).SingleOrDefault();

            if (aplicativo == null)
                return "";

            return aplicativo.Titulo;
        }

        public string getDescricao(string codigo)
        {
            var aplicativo = db.Aplicativos.Where(x => x.Codigo == codigo).SingleOrDefault();

            if (aplicativo == null)
                return "";

            return aplicativo.Descricao;
        }
    }
}
