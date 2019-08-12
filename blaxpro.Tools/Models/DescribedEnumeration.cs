namespace blaxpro.Tools.Models
{
    public abstract class DescribedEnumeration : Enumeration
    {
        protected DescribedEnumeration(int id, string name, string description) : base(id, name)
        {
            this.Description = description;
        }

        public string Description { get; }
    }
}