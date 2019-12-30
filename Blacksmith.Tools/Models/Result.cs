using Blacksmith.Tools;
using Blacksmith.Validations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blacksmith.Models
{
    public class Result : Blacksmith.Validations.AbstractDomain
    {
        private readonly IReadOnlyList<Exception> exceptions;

        protected Result(bool success, IEnumerable<Exception> exceptions) : base()
        {
            if (success)
                this.assert.isNull(exceptions);
            else
                this.assert.isNotNull(exceptions);

            this.Success = success;

            this.Exceptions = (exceptions ?? Enumerable.Empty<Exception>())
                .ToList()
                .AsReadOnly();
        }

        public Result() : this(true, null) { }

        public Result(params Exception[] errors) : this(false, errors) { }

        public Result(IEnumerable<Exception> errors) : this(false, errors) { }

        public bool Success { get; }

        public IReadOnlyList<Exception> Exceptions { get; }

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
                base.isTrue<ValueRequestOnUnsuccessResultException>(this.Success);
                return this.value;
            }
        }

        public static implicit operator Result<T>(T value)
        {
            return new Result<T>(value);
        }
    }
}