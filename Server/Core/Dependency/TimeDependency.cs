using System;
namespace Core.Dependency
{
    public class TimeDependency : IDependency
    {
        public TimeDependency()
        {
        }

        public string Name
        {
            get
            {
                return "TimeDependency";
            }
        }

    }
}
