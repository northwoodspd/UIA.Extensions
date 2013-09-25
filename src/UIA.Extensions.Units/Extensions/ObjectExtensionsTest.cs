using System;
using FluentAssertions;
using NUnit.Framework;
using UIA.Extensions.InternalExtensions;

namespace UIA.Extensions.Extensions
{
    internal class ObjectExtensionsTest
    {
        [TestFixture]
        public class Comparisons
        {
            [Test]
            public void NullComparisonsAreFalse()
            {
                "".CeremoniallyEquals(null, TrueResult).Should().BeFalse();
            }

            [Test]
            public void LookInsideYourself()
            {
                const string samesies = "";
                samesies.CeremoniallyEquals(samesies, FalseResult).Should().BeTrue();
            }

            [Test]
            public void DifferentStrokes()
            {
                "".CeremoniallyEquals(1, TrueResult).Should().BeFalse();
            }

            [Test]
            public void TheFinalExam()
            {
                "thes are".CeremoniallyEquals("not the same, but whatever, return a", TrueResult)
                  .Should().BeTrue();
            }

            private static bool TrueResult(object obj)
            {
                return true;
            }

            private static bool FalseResult(object obj)
            {
                return false;
            }
        }

        [TestFixture]
        public class HashingItOut
        {
            [Test]
            public void HashCodesAreCombined()
            {
                var now = DateTime.Now;
                "".CombinedHashCodes(123, "", now)
                  .ShouldBeEquivalentTo(123.GetHashCode() ^ "".GetHashCode() ^ now.GetHashCode());
            }

            [Test]
            public void SequencesAreTheSameIfDesired()
            {
                var sequence = new[] {"a", "b", "whatever"};
                sequence.GetSequenceHashCode()
                 .ShouldBeEquivalentTo("a".GetHashCode() ^ "b".GetHashCode() ^ "whatever".GetHashCode());
            }

            [Test]
            public void NullsAreCool()
            {
                "".CombinedHashCodes("", null, null)
                  .ShouldBeEquivalentTo("".GetHashCode());
            }
        }
    }
}
