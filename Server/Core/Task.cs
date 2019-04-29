using System;
using System.Collections.Generic;

namespace Core
{
    public class Task
    {

        #region Private Fields

        private List<IDependency> _dependencies;

        #endregion

        #region Constructor

        public Task()
        {
            _dependencies = new List<IDependency>();
        }

        #endregion

        #region Properties

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
            private set;
        }

        #endregion

        public void Complete()
        {
            IsCompleted = true;
        }

        public void UnComplete()
        {
            IsCompleted = false;
        }

        public void AddDependency(IDependency dependency)
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
