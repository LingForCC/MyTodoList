using Core.Exceptions;
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

        public Task(string name)
        {
            ValidateName(name);
            Name = name;
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

        private void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new TaskException("invalid task name.");
            }

            if (!Regex.IsMatch(name, Ultils.Regessions.TASK_NAME_REG))
            {
                throw new TaskException("invalid task name.");
            }

            if (name.Trim().Length > MAX_LENGTH_OF_NAME)
            {
                throw new TaskException("invalid task name.");
            }
        }
    }
}
