using System;
namespace Core.Repositories
{
    public class RepositoryException : Exception
    {
        public RepositoryException(string repositoryName, 
            string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public RepositoryException(string repositoryName, string message)
            : base(message, null)
        {

        }

    }
}
