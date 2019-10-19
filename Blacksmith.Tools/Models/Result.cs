using Blacksmith.Validations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blacksmith.Models
{
    public class Result
    {
        private readonly IReadOnlyList<Exception> exceptions;

        protected Result(bool success, IEnumerable<Exception> exceptions)
        {
            Asserts.Assert.isTrue((success && exceptions == null) || (success == false && exceptions != null));

            this.Success = success;

            if(success == false)
                this.exceptions = exceptions
                    .ToList()
                    .AsReadOnly();
        }

        public Result() : this(true, null) { }

        public Result(params Exception[] errors) : this(false, errors) { }

        public Result(IEnumerable<Exception> errors) : this(false, errors) { }

        public bool Success { get; }

        public IReadOnlyList<Exception> Exceptions
        {
            get
            {
                Asserts.Assert.isFalse(this.Success);
                return this.exceptions;
            }
        }

        public static implicit operator Result(Exception exception)
        {
            return new Result(exception);
        }
    }

    public class Result<T> : Result
    {
        private readonly T value;

        private Result(bool success, IEnumerable<Exception> exceptions, T value) : base(success, exceptions)
        {
            this.value = value;
        }

        public Result(T value) : this(true, null, value) { }

        public Result(params Exception[] errors) : this(false, errors, default(T)) { }

        public Result(IEnumerable<Exception> errors) : this(false, errors, default(T)) { }

        public T Value
        {
            get
            {
                Asserts.Assert.isTrue(this.Success);
                return this.value;
            }
        }

        public static implicit operator Result<T>(T value)
        {
            return new Result<T>(value);
        }
    }
}