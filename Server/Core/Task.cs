using System;
using System.Collections.Generic;

namespace Core
{
    public class Task
    {
        private List<Dependency> _dependencies;

        public Task()
        {
            _dependencies = new List<Dependency>();
        }


        public string Name
        {
            get;
            set;
        }

        public string Description
        { 
            get;
            set;
        }

        public bool IsCompleted
        {
            get;
            set;
        }

        public void AddDependency(Dependency dependency)
        {
            if(null != dependency)
            {
                _dependencies.Add(dependency);
            }
            else
            {
                throw new TaskException("The input parameter can not be null");
            }
        }

    }

    public class TaskException : Exception
    {

        public TaskException(string message)
            : this(message, null)
        {

        }
    

        public TaskException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }
}
