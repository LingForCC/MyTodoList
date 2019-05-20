using System;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) 
            : base(options)
        {

        }
    }
}
