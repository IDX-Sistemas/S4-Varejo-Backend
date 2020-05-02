using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Context
{
    public interface IDbContext : IDisposable
    {
        DbSet<T> Set<T>() where T : class;
    }
}
