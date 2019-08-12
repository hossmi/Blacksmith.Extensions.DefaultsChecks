using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace blaxpro.Tools.Models
{
    public abstract class Enumeration : IComparable 
    {
        protected Enumeration(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public string Name { get; }
        public int Id { get; }

        public override string ToString()
        {
            return this.Name;
        }

        public static IEnumerable<T> getAll<T>() where T : Enumeration
        {
            var fields = typeof(T).GetFields(
                BindingFlags.Public 
                | BindingFlags.Static 
                | BindingFlags.DeclaredOnly);

            return fields.
                Select(f => f.GetValue(null))
                .Cast<T>();
        }

        public static bool isDefined<T>(T value) where T: Enumeration
        {
            return getAll<T>()
                .Contains<T>(value);
        }

        public override bool Equals(object obj)
        {
            bool typeMatches, valueMatches;
            Enumeration otherValue;

            otherValue = obj as Enumeration;

            if (otherValue == null)
                return false;

            typeMatches = GetType().Equals(obj.GetType());
            valueMatches = this.Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public int CompareTo(object other)
        {
            Enumeration otherValue;

            otherValue = other as Enumeration;

            if (otherValue == null)
                throw new ArgumentException("Invalid compareTo argument", nameof(other));

            return this.Id.CompareTo(otherValue.Id);
        }
    }
}