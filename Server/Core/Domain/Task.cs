using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Core
{
    public class Task
    {
        public const int MAX_LENGTH_OF_NAME = 300;

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

        public string Id { get; set; }

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
            if (null != dependency)
            {
                _dependencies.Add(dependency);
            }
            else
            {
                throw new TaskException("The input parameter can not be null");
            }
        }

        public void ValidateTaskName()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new TaskException("invalid task name.");
            }

            if (!Regex.IsMatch(Name, Ultils.Regessions.TASK_NAME_REG))
            {
                throw new TaskException("invalid task name.");
            }

            if (Name.Trim().Length > MAX_LENGTH_OF_NAME)
            {
                throw new TaskException("invalid task name.");
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
