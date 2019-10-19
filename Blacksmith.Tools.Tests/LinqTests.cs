using System.Linq;
using Xunit;
using Blacksmith.Tools.Extensions.Queryables;

namespace Blacksmith.Tools.Tests
{
    public class LinqTests
    {
        [Fact(DisplayName = "Single extension methods throws custom exception.")]
        public void single_extension_methods_throws_custom_exception()
        {
            int[] numbers;
            int x;

            numbers = new int[] { };

            Assert.Throws<MyException>(() =>
            {
                x = numbers.single(n => n % 2 == 0, ex => new MyException("Boooom", ex));
            });
        }

        [Fact(DisplayName = "Single extension methods throws custom exception using implicit cast.")]
        public void single_extension_methods_throws_custom_exception_using_implicit_cast()
        {
            int[] numbers;
            int x;

            numbers = new int[] { };

            Assert.Throws<MyException>(() =>
            {
                x = numbers.single(n => n % 2 == 0, ex => (MyException)ex);
            });
        }
    }
}
