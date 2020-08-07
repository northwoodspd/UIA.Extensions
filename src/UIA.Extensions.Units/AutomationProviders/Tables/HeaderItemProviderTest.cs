using System.Windows.Automation;
using FluentAssertions;
using NUnit.Framework;
using UIA.Extensions.AutomationProviders.Interfaces.Tables;

namespace UIA.Extensions.AutomationProviders.Tables
{
    [TestFixture]
    public class HeaderItemProviderTest
    {
        [Test]
        public void ItHasTheHeaderItemType()
        {
            new HeaderItemProvider(null, null, 0).GetPropertyValue(AutomationElementIdentifiers.ControlTypeProperty.Id)
                .Should().BeEquivalentTo(ControlType.HeaderItem.Id);
        }

        [TestFixture]
        public class AreEqual
        {
            [Test]
            public void SameValueSameIndex()
            {
                var firstHeader = HeaderFor("value", 0);
                var secondHeader = HeaderFor("value", 0);
                firstHeader.Should().BeEquivalentTo(secondHeader);
            }

            [Test]
            public void ItHashesTheCodes()
            {
                HeaderFor("value", 7).GetHashCode().Should().Be("value".GetHashCode() ^ 7.GetHashCode());
            }
        }

        [TestFixture]
        public class AreDifferent
        {
            [Test]
            public void SameValueDifferentIndex()
            {
                var firstHeader = HeaderFor("value", 0);
                var secondHeader = HeaderFor("value", 1);
                firstHeader.Should().NotBe(secondHeader);
            }

            [Test]
            public void DifferentValuesSameIndex()
            {
                var firstHeader = HeaderFor("value", 0);
                var secondHeader = HeaderFor("other value", 0);
                firstHeader.Should().NotBe(secondHeader);
            }
        }

        private static HeaderItemProvider HeaderFor(string value, int index)
        {
            return new HeaderItemProvider(null, new HeaderInformation(value), index);
        }
    }
}
