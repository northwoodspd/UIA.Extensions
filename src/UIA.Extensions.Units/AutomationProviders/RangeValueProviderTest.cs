using System.Windows.Automation;
using System.Windows.Forms;
using NUnit.Framework;
using Should.Fluent;

namespace UIA.Extensions.AutomationProviders
{
    [TestFixture]
    class RangeValueProviderTest
    {
        private RangeValueProvider _provider;

        [SetUp]
        public void SetUp()
        {
            _provider = new RangeValueProvider(new Control());
        }

        [Test]
        public void ItHasTheCorrectPattern()
        {
            _provider.GetPatternProvider(RangeValuePatternIdentifiers.Pattern.Id)
                .Should().Be.SameAs(_provider);
        }
    }
}
