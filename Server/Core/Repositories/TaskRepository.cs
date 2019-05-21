using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories
{
    public class TaskRepository : BaseRepository, ITaskRepository
    {

        private readonly DbSet<Task> _tasks;


        public TaskRepository(DbContext dbContext)
            :base(dbContext)
        {
            _tasks = dbContext.Set<Task>();
        }

        public async Task<int> AddTaskAsync(Task task)
        {
            try
            {
                await _tasks.AddAsync(task);
                return await DbContext.SaveChangesAsync();
            }
            catch(Exception e)
            {
                throw new RepositoryException(Name, e.Message, e);
            }

        }

        public string Name 
        { 
            get
            {
                return "TaskRepository";
            }
        }


        #region ITaskRepository Implementation

        public void Add(Task entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Task entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Task> Find(Expression<Func<Task, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Task> FindAll()
        {
            throw new NotImplementedException();
        }

        public Task FindById(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(Task entity)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
