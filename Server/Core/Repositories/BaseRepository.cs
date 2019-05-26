using System;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories
{
    public class BaseRepository
    {
        public BaseRepository(DbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            DbContext = dbContext;
        }

        protected DbContext DbContext
        {
            get;
            private set;
        }
    }
}
