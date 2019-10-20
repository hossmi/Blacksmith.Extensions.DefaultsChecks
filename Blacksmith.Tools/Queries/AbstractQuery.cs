using Blacksmith.Extensions.Queries;
using Blacksmith.Validations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Blacksmith.Queries
{
    public abstract class AbstractQuery<Tin, TOut> : IQuery<TOut>
    {
        protected static Asserts assert;

        static AbstractQuery()
        {
            assert = Asserts.Assert;
        }

        private readonly IQueryable<Tin> query;
        private int page;
        private readonly bool pageInMemory;
        private int pageSize;

        public AbstractQuery(IQueryable<Tin> query, bool pageInMemory)
        {
            this.query = query;
            this.pageSize = int.MaxValue;
            this.page = 0;
            this.pageInMemory = pageInMemory;
        }

        public int Page
        {
            get
            {
                return this.page;
            }
            set
            {
                assert.isTrue(value >= 0, $"{nameof(this.Page)} must be greater or equal than zero.");
                this.page = value;

            }
        }

        public int PageSize
        {
            get
            {
                return this.pageSize;
            }
            set
            {
                assert.isTrue(1 <= value && value <= int.MaxValue, $"{nameof(this.PageSize)} must be positive number.");
                this.pageSize = value;
            }
        }

        public int Count
        {
            get
            {
                return this.query.Count();
            }
        }

        public IEnumerator<TOut> GetEnumerator()
        {
            return prv_enumerate();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return prv_enumerate();
        }

        protected abstract TOut prv_map(Tin dbEntity);

        private IEnumerator<TOut> prv_enumerate()
        {
            if(this.pageInMemory)
            {
                return this.query
                    .AsEnumerable()                   
                    .Skip(this.page * this.pageSize)
                    .Take(this.pageSize)
                    .Select(prv_map)
                    .Where(r => r != null)
                    .GetEnumerator();
            }
            else
            {
                return this.query
                    .Skip(this.page * this.pageSize)
                    .Take(this.pageSize)
                    .AsEnumerable()
                    .Select(prv_map)
                    .Where(r => r != null)
                    .GetEnumerator();
            }
        }
    }
}
