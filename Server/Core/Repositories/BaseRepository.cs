using System;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories
{
    public class BaseRepository
    {
        public BaseRepository(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        protected DbContext DbContext
        {
            get;
            private set;
        }
    }
}
