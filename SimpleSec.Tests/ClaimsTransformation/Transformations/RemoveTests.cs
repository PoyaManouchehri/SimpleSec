using System.Collections.Generic;
using System.Security.Claims;
using NUnit.Framework;
using SimpleSec.ClaimsTransformation.Extensions;
using SimpleSec.ClaimsTransformation.Transformations;

namespace SimpleSec.Tests.ClaimsTransformation.Transformations
{
    [TestFixture]
    class RemoveTests
    {
        [Test]
        [TestCase("ClaimB")]
        [TestCase("ClAImB")]
        public void GivenAListOfClaims_WhenAnExistingClaimIsRemoved_ThenItCeasesToExist(string claimType)
        {
            var claims = new List<Claim>()
            {
                new Claim("ClaimA", "ValueA"),
                new Claim("ClaimB", "ValueB"),
                new Claim("ClaimC", "ValueC"),
            };

            new Remove(claimType).Apply(claims);

            Assert.That(claims, Has.Count.EqualTo(2));
            Assert.That(claims.Contains("ClaimB"), Is.False);
        }

        [Test]
        public void GivenAListOfClaims_WhenANonExistingClaimIsRemoved_ThenTheListDoesNotChange()
        {
            var claims = new List<Claim>()
            {
                new Claim("ClaimA", "ValueA"),
                new Claim("ClaimB", "ValueB"),
                new Claim("ClaimC", "ValueC"),
            };

            new Remove("ClaimD").Apply(claims);

            Assert.That(claims, Is.EquivalentTo(claims));
        }
    }
}
