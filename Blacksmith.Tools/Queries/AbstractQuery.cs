using Blacksmith.Extensions.Queries;
using Blacksmith.Validations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Blacksmith.Queries
{
    public abstract class AbstractQuery<Tin, TOut> : IQuery<TOut>
    {
        private readonly IQueryable<Tin> query;
        private int page;
        private string sortBy;
        private string sortDirection;
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
                Asserts.Assert.isTrue(value >= 0);
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
                Asserts.Assert.isTrue(1 <= value && value <= int.MaxValue);
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

        public string SortBy
        {
            get
            {
                return this.sortBy;
            }

            set
            {
                Asserts.Assert.isNotNull(value);
                this.sortBy = value;
            }
        }

        public string SortDirection
        {
            get
            {
                return this.sortDirection;
            }

            set
            {
                Asserts.Assert.isNotNull(value);
                this.sortDirection = value;
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
                    .setOrder(this.sortBy, this.sortDirection)
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
                    .setOrder(this.sortBy, this.sortDirection)
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
