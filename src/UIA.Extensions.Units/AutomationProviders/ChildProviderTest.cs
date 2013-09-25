using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation.Provider;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace UIA.Extensions.AutomationProviders
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
                .Should().BeSameAs(_parent.Object);
        }

        [Test]
        public void ItNeverForgetsItsRoots()
        {
            var expectedRoot = new Mock<AutomationProvider>();
            _parent.Setup(x => x.FragmentRoot).Returns(expectedRoot.Object);

            _childProvider.FragmentRoot.Should().BeSameAs(expectedRoot.Object);
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
                Before(child).Should().BeNull();
                After(child).Should().BeNull();
            }

            [Test]
            public void KnowsAboutTheNextChild()
            {
                var first = AddChild();
                var second = AddChild();
                After(first).Should().BeSameAs(second);
            }

            [Test]
            public void KnowsAboutThePreviousChild()
            {
                var first = AddChild();
                var second = AddChild();
                Before(second).Should().BeSameAs(first);
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
