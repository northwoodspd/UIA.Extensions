using System;
using FluentAssertions;
using NUnit.Framework;
using UIA.Extensions.InternalExtensions;

namespace UIA.Extensions.Extensions
{
    public class EnumerableExtensionsTest
    {
        [TestFixture]
        public class Before
        {
            [Test]
            public void CanFindItemsBeforeAValue()
            {
                var values = new[] { "first", "second", "third" };
                values.Before("second").Should().BeEquivalentTo("first");
            }

            [Test]
            public void DefaultIfAtTheBeginning()
            {
                new[] { 1, 3, 5 }.Before(1).ShouldBeEquivalentTo(0);
            }

            [Test]
            public void DefaultIfNotFound()
            {
                new[] {"blar"}.Before(String.Empty).Should().BeNull();
            }
        }

        [TestFixture]
        public class After
        {
            [Test]
            public void CanFindItemsAfter()
            {
                var values = new[] { "first", "second", "third" };
                values.After("second").Should().BeEquivalentTo("third");
            }

            [Test]
            public void DefaultIfAtTheEnd()
            {
                new[] { 1, 3, 5 }.After(5).ShouldBeEquivalentTo(0);
            }

            [Test]
            public void DefaultIfNotFound()
            {
                new[] { "blar" }.After(String.Empty).Should().BeNull();
            }
        }
    
    }
}
