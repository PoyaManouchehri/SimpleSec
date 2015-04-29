using System.Collections.Generic;
using System.Security.Claims;
using NUnit.Framework;
using SimpleSec.ClaimsTransformation.Extensions;
using SimpleSec.ClaimsTransformation.Transformations;

namespace SimpleSec.Tests.ClaimsTransformation.Transformations
{
    [TestFixture]
    class CopyTests
    {
        private const string SourceType = "source-claim";
        private const string SourceTypeUpperCase = "SOURCE-CLAIM";
        private const string TargetType = "target-claim";
        private const string SourceValue = "SomeValue";
        private const string ExistingTargetValue = "ExistingTargetValue";

        [Test]
        [TestCase(SourceType)]
        [TestCase(SourceTypeUpperCase)]
        public void GivenAListOfClaims_WhenCopiedFromAnExistingClaim_ThenTheClaimIsCorrectlyCopiedToTarget(string inputSourceType)
        {
            var claims = new List<Claim>()
            {
                new Claim("claimA", "valueA"),
                new Claim(SourceType, SourceValue),
                new Claim("claimB", "valueB"),
            };

            new Copy(inputSourceType, TargetType, overwrite: false).Apply(claims);

            Assert.That(claims, Has.Count.EqualTo(4));
            Assert.That(claims, Has.Exactly(1).Matches<Claim>(c => c.Type == TargetType && c.Value == SourceValue));
        }

        [Test]
        public void GivenAListOfClaims_WhenCopiedFromANonExistingClaim_ThenTheTargetClaimIsNotAdded()
        {
            var claims = new List<Claim>()
            {
                new Claim("claimA", "valueA"),
                new Claim("claimB", "valueB"),
            };

            new Copy(SourceType, TargetType, overwrite: false).Apply(claims);

            Assert.That(claims, Has.Count.EqualTo(2));
            Assert.That(claims, Has.None.Matches<Claim>(c => c.Type == TargetType));
        }

        [Test]
        public void GivenAListOfClaims_WhenCopiedWithNoOverWrite_ThenTheTargetClaimIsNotChanged()
        {
            var claims = new List<Claim>()
            {
                new Claim("claimA", "valueA"),
                new Claim(SourceType, SourceValue),
                new Claim("claimB", "valueB"),
                new Claim(TargetType, ExistingTargetValue),
            };

            new Copy(SourceType, TargetType, overwrite: false).Apply(claims);

            Assert.That(claims, Has.Count.EqualTo(4));
            Assert.That(claims.Find(TargetType).Value, Is.EqualTo(ExistingTargetValue));
        }

        [Test]
        public void GivenAListOfClaims_WhenCopiedWithOverWrite_ThenTheTargetClaimIsUpdated()
        {
            var claims = new List<Claim>()
            {
                new Claim("claimA", "valueA"),
                new Claim(SourceType, SourceValue),
                new Claim("claimB", "valueB"),
                new Claim(TargetType, ExistingTargetValue),
            };

            new Copy(SourceType, TargetType, overwrite: true).Apply(claims);

            Assert.That(claims, Has.Count.EqualTo(4));
            Assert.That(claims.Find(TargetType).Value, Is.EqualTo(SourceValue));
        }
    }
}
