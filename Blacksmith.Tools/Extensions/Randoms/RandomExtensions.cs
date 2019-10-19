using System;
using System.Collections.Generic;
using Blacksmith.Tools.Exceptions;
using Blacksmith.Validations;

namespace Blacksmith.Tools.Extensions.Randoms
{
    public static class RandomExtensions
    {
        private static Random random;
        private static readonly IValidatorStrings strings;
        private static readonly Validator<RandomExtensionsException> validate;

        static RandomExtensions()
        {
            random = new Random(DateTime.UtcNow.Millisecond);
            strings = new EnValidatorStrings();
            validate = new Validator<RandomExtensionsException>(strings, prv_buildException);
        }

        public static Random Instance
        {
            get
            {
                return random;
            }
            set
            {
                validate.isNotNull(value);
                random = value;
            }
        }

        public static DateTime getDateBetween(this Random random, DateTime from, DateTime to)
        {
            DateTime result;
            TimeSpan diference;
            int days, randomDays;

            diference = to - from;
            days = (int)diference.TotalDays;
            randomDays = random.Next(days);
            result = from.AddDays(randomDays);

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

            validate.isTrue(0 < items.Count, string.Format(strings.Out_of_Range_value_for_0, nameof(items)));

            index = random.Next(0, items.Count);
            item = items[index];
            items.RemoveAt(index);

            return item;
        }

        public static T extractRandom<T>(this IList<T> items)
        {
            int index;
            T item;

            validate.isTrue(0 < items.Count, string.Format(strings.Out_of_Range_value_for_0, nameof(items)));

            index = Instance.Next(0, items.Count);
            item = items[index];
            items.RemoveAt(index);

            return item;
        }

        private static bool prv_nextBool(Random random, double truePercentage)
        {
            validate.isNotNull(random);
            validate.isTrue(0.0 <= truePercentage && truePercentage <= 1.0
                , string.Format(strings.Out_of_Range_value_for_0, nameof(truePercentage)));

            return random.NextDouble() <= truePercentage;
        }

        private static RandomExtensionsException prv_buildException(string message)
        {
            return new RandomExtensionsException(message);
        }
    }
}
