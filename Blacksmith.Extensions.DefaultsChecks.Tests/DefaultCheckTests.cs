using AutoFixture.Xunit2;
using Blacksmith.Exceptions;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Blacksmith.Extensions.DefaultsChecks.Tests
{
    public class DefaultCheckTests
    {
        [Theory]
        [AutoData]
        public void isFilled_should_be_true(string someText)
        {
            someText.isFilled()
                .Should()
                .BeTrue();
        }

        [Theory]
        [AutoData]
        public void isEmpty_should_be_false(string someText)
        {
            someText.isEmpty()
                .Should()
                .BeFalse();
        }

        [Theory]
        [MemberData(nameof(getNullStrings))]
        public void isEmpty_should_be_false_on_empty_string(string someText)
        {
            someText.isEmpty()
                .Should()
                .BeTrue();
        }

        [Theory]
        [MemberData(nameof(getNullStrings))]
        public void isFilled_should_be_false_on_empty_string(string someText)
        {
            someText.isFilled()
                .Should()
                .BeFalse();
        }

        [Theory]
        [MemberData(nameof(getNullStrings))]
        public void defaultIfNullOrWhiteSpace_should_return_empty_string(string someText)
        {
            someText.defaultIfNullOrWhiteSpace()
                .Should()
                .BeSameAs(string.Empty);
        }

        public static IEnumerable<object[]> getNullStrings()
        {
            yield return new object[] { null };
            yield return new object[] { "" };
            yield return new object[] { "   " };
        }

        [Fact]
        public void defaultIfNull_returns_default_instance()
        {
            TestClass item, result;

            item = null;

            result = item.defaultIfNull();
            result.Should().NotBeNull();
            result.SomeInt.Should().Be(55);
            result.SomeText.Should().Be("default");
        }

        [Fact]
        public void defaultIfNull_returns_same_instance()
        {
            TestClass item, result;

            item = new TestClass
            {
                SomeInt = 34,
                SomeText = "pepe",
            };

            result = item.defaultIfNull();
            result.Should().NotBeNull();
            result.Should().Be(item);
        }

        [Fact]
        public void defaultIfNull_returns_default_delegated_instance()
        {
            ParametrizedConstructorTestClass item, result;

            item = null;

            result = item.defaultIfNull(() => new ParametrizedConstructorTestClass(89));
            result.Should().NotBeNull();
            result.SomeInt.Should().Be(89);
            result.SomeText.Should().Be("lorem ipsum");
        }

        [Fact]
        public void defaultIfNull_for_parametrized_classes_returns_same_instance()
        {
            ParametrizedConstructorTestClass item, result;

            item = new ParametrizedConstructorTestClass(34)
            {
                SomeText = "pepe",
            };

            result = item.defaultIfNull(() => new ParametrizedConstructorTestClass(89));
            result.Should().NotBeNull();
            result.Should().Be(item);
        }

        [Fact]
        public void defaultIfNull_for_parametrized_classes_throws_exception_on_null_return_from_delegate()
        {
            ParametrizedConstructorTestClass item;

            item = null;

            item.Invoking(i => i.defaultIfNull(() => null))
                .Should()
                .Throw<NullResultFromDelegateMethodException>();
        }
    }
}
