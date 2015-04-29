using System.Collections.Generic;
using System.Security.Claims;
using NUnit.Framework;
using SimpleSec.ClaimsTransformation.Extensions;
using SimpleSec.ClaimsTransformation.Transformations;

namespace SimpleSec.Tests.ClaimsTransformation.Transformations
{
    [TestFixture]
    class MapTests
    {
        private const string SourceType = "source-claim";
        private const string SourceTypeUpperCase = "SOURCE-CLAIM";
        private const string TargetType = "target-claim";
        private const string SourceValue = "SomeValue";
        private const string ExistingTargetValue = "ExistingTargetValue";

        [Test]
        [TestCase(SourceType)]
        [TestCase(SourceTypeUpperCase)]
        public void GivenAListOfClaims_WhenMappedFromAnExistingClaim_ThenTheClaimIsCorrectlyMapped(string inputSourceType)
        {
            var claims = new List<Claim>()
            {
                new Claim("claimA", "valueA"),
                new Claim(SourceType, SourceValue),
                new Claim("claimB", "valueB"),
            };

            new Map(inputSourceType, TargetType, overwrite: false).Apply(claims);

            Assert.That(claims, Has.Count.EqualTo(3));
            Assert.That(claims.Contains(SourceType), Is.False);
            Assert.That(claims, Has.Exactly(1).Matches<Claim>(c => c.Type == TargetType && c.Value == SourceValue));
        }

        [Test]
        public void GivenAListOfClaims_WhenMappedFromANonExistingClaim_ThenTheTargetClaimIsNotAdded()
        {
            var claims = new List<Claim>()
            {
                new Claim("claimA", "valueA"),
                new Claim("claimB", "valueB"),
            };

            new Map(SourceType, TargetType, overwrite: false).Apply(claims);

            Assert.That(claims, Has.Count.EqualTo(2));
            Assert.That(claims.Contains(TargetType), Is.False);
        }

        [Test]
        public void GivenAListOfClaims_WhenMappedWithNoOverWrite_ThenTheTargetClaimIsNotChangedAndTheSourceIsRemoved()
        {
            var claims = new List<Claim>()
            {
                new Claim("claimA", "valueA"),
                new Claim(SourceType, SourceValue),
                new Claim("claimB", "valueB"),
                new Claim(TargetType, ExistingTargetValue),
            };

            new Map(SourceType, TargetType, overwrite: false).Apply(claims);

            Assert.That(claims, Has.Count.EqualTo(3));
            Assert.That(claims.Contains(SourceType), Is.False);
            Assert.That(claims.Find(TargetType).Value, Is.EqualTo(ExistingTargetValue));
        }

        [Test]
        public void GivenAListOfClaims_WhenMappedWithOverWrite_ThenTheTargetClaimIsUpdatedAndSourceIsRemoved()
        {
            var claims = new List<Claim>()
            {
                new Claim("claimA", "valueA"),
                new Claim(SourceType, SourceValue),
                new Claim("claimB", "valueB"),
                new Claim(TargetType, ExistingTargetValue),
            };

            new Map(SourceType, TargetType, overwrite: true).Apply(claims);

            Assert.That(claims, Has.Count.EqualTo(3));
            Assert.That(claims.Contains(SourceType), Is.False);
            Assert.That(claims.Find(TargetType).Value, Is.EqualTo(SourceValue));
        }
    }
}
