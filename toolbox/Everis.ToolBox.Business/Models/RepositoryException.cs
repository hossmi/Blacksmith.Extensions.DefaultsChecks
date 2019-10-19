﻿using Everis.ToolBox.Extensions.Strings;
using System;
using System.Runtime.Serialization;

namespace Everis.ToolBox.Models
{
    public class RepositoryException : Exception
    {
        public RepositoryException(string message) : base(message)
        {
        }
        public RepositoryException(string message, params object[] args) : base(message.format(args))
        {
        }

        public RepositoryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RepositoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public static implicit operator RepositoryException(string message)
        {
            return new RepositoryException(message);
        }
    }
}