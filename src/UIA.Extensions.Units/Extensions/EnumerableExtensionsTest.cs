using System;
using NUnit.Framework;
using Should.Fluent;

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
                values.Before("second").Should().Equal("first");
            }

            [Test]
            public void DefaultIfAtTheBeginning()
            {
                new[] { 1, 3, 5 }.Before(1).Should().Equal(0);
            }

            [Test]
            public void DefaultIfNotFound()
            {
                new[] { "blar" }.Before(String.Empty).Should().Be.Null();
            }
        }

        [TestFixture]
        public class After
        {
            [Test]
            public void CanFindItemsAfter()
            {
                var values = new[] { "first", "second", "third" };
                values.After("second").Should().Equal("third");
            }

            [Test]
            public void DefaultIfAtTheEnd()
            {
                new[] { 1, 3, 5 }.After(5).Should().Equal(0);
            }

            [Test]
            public void DefaultIfNotFound()
            {
                new[] { "blar" }.After(String.Empty).Should().Be.Null();
            }
        }
    
    }
}
