﻿using System;

namespace Core.Exceptions
{
    [Serializable]
    public class DomainException : Exception
    {
        public DomainException() { }

        public DomainException(string message) : base(message) { }

        public DomainException(string message, Exception inner) : base(message, inner) { }
    }

}