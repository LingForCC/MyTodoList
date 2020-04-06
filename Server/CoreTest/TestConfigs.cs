using Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CoreTest
{
  public class TestConfigs
  {
      public static TaskDbContext GetTaskDbContext(string dbName)
      {
          return new TaskDbContext(GetDbOptions(dbName));
      }

      public static DbContextOptions<TaskDbContext> GetDbOptions(string dbName) {
            return new DbContextOptionsBuilder<TaskDbContext>()
              .UseInMemoryDatabase(dbName)
              .Options;
      }
  }
}
