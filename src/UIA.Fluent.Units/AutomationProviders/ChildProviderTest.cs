using System.Windows.Automation.Provider;
using Moq;
using NUnit.Framework;
using Should.Fluent;

namespace UIA.Fluent.AutomationProviders
{
    [TestFixture]
    public class ChildProviderTest
    {
        private Mock<AutomationProvider> _parent;
        private ChildProvider _childProvider;

        [SetUp]
        public void SetUp()
        {
            _parent = new Mock<AutomationProvider>();
            _childProvider = new ChildProvider(_parent.Object);
        }

        [Test]
        public void ItHasAParent()
        {
            _childProvider.Navigate(NavigateDirection.Parent)
                .Should().Be.SameAs(_parent.Object);
        }

        [Test]
        public void ItNeverForgetsItsRoots()
        {
            var expectedRoot = new Mock<AutomationProvider>();
            _parent.Setup(x => x.FragmentRoot).Returns(expectedRoot.Object);

            _childProvider.FragmentRoot.Should().Be.SameAs(expectedRoot.Object);
        }
    }
}
