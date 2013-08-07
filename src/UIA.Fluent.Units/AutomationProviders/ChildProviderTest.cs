using System.Collections.Generic;
using System.Linq;
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

        [TestFixture]
        public class Navigation
        {
            private Mock<AutomationProvider> _parent;
            private readonly List<ChildProvider> _chillins = new List<ChildProvider>();

            [SetUp]
            public void SetUp()
            {
                _parent = new Mock<AutomationProvider>();
                _parent.Setup(x => x.Children).Returns(_chillins);
            }

            [TearDown]
            public void TearDown()
            {
                _chillins.Clear();
            }

            [Test]
            public void OneChildHasNoNextOrPrevious()
            {
                var child = AddChild();
                Before(child).Should().Be.Null();
                After(child).Should().Be.Null();
            }

            [Test]
            public void KnowsAboutTheNextChild()
            {
                var first = AddChild();
                var second = AddChild();
                After(first).Should().Be.SameAs(second);
            }

            [Test]
            public void KnowsAboutThePreviousChild()
            {
                var first = AddChild();
                var second = AddChild();
                Before(second).Should().Be.SameAs(first);
            }

            private ChildProvider AddChild()
            {
                _chillins.Add(new ChildProvider(_parent.Object));
                return _chillins.Last();
            }

            private static IRawElementProviderFragment Before(IRawElementProviderFragment child)
            {
                return child.Navigate(NavigateDirection.PreviousSibling);
            }

            private static IRawElementProviderFragment After(IRawElementProviderFragment child)
            {
                return child.Navigate(NavigateDirection.NextSibling);
            }
        }
    }
}
