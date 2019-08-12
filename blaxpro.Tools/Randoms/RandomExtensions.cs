using System;
using System.Collections.Generic;

namespace Everis.ToolBox.Extensions.Randoms
{
    public static class RandomExtensions
    {
        private static Random random;

        static RandomExtensions()
        {
            random = new Random(DateTime.UtcNow.Millisecond);
        }

        public static Random Instance
        {
            get
            {
                return random;
            }
            set
            {
                random = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        public static DateTime getDateBetween(this Random random, DateTime from, DateTime to)
        {
            TimeSpan diference = to - from;
            int days = (int)diference.TotalDays;
            int randomDays = random.Next(days);
            DateTime result = from.AddDays(randomDays);
            return result;
        }

        public static bool nextBool(this Random random)
        {
            return prv_nextBool(random, 0.5);
        }

        public static bool nextBool(this Random random, double truePercentage)
        {
            return prv_nextBool(random, truePercentage);
        }

        public static double nextDouble(this Random random, double max)
        {
            return random.NextDouble() * max;
        }

        public static decimal nextDecimal(this Random random, decimal max)
        {
            return (decimal)random.NextDouble() * max;
        }

        public static T getRandomItem<T>(this Random random, IReadOnlyList<T> items)
        {
            int index;

            index = random.Next(0, items.Count);

            return items[index];
        }

        public static T getRandomItem<T>(this Random random, IList<T> items)
        {
            int index;

            index = random.Next(0, items.Count);

            return items[index];
        }

        public static T getRandomItem<T>(this Random random, T[] items)
        {
            int index;

            index = random.Next(0, items.Length);

            return items[index];
        }

        public static T getRandom<T>(this IList<T> items)
        {
            int index;

            index = Instance.Next(0, items.Count);

            return items[index];
        }

        public static T getRandomItem<T>(this IReadOnlyList<T> items)
        {
            int index;

            index = Instance.Next(0, items.Count);

            return items[index];
        }

        public static T getRandom<T>(this T[] items)
        {
            int index;

            index = Instance.Next(0, items.Length);

            return items[index];
        }

        public static T extractRandomItem<T>(this Random random, IList<T> items)
        {
            int index;
            T item;

            if (items.Count <= 0)
                throw new ArgumentOutOfRangeException(nameof(items));

            index = random.Next(0, items.Count);
            item = items[index];
            items.RemoveAt(index);

            return item;
        }

        public static T extractRandom<T>(this IList<T> items)
        {
            int index;
            T item;

            if (items.Count <= 0)
                throw new ArgumentOutOfRangeException(nameof(items));

            index = Instance.Next(0, items.Count);
            item = items[index];
            items.RemoveAt(index);

            return item;
        }

        private static bool prv_nextBool(Random random, double truePercentage)
        {
            if (random == null)
                throw new ArgumentNullException(nameof(random));

            if (truePercentage < 0.0 || 1.0 < truePercentage)
                throw new ArgumentNullException(nameof(truePercentage));

            return random.NextDouble() <= truePercentage;
        }
    }
}
