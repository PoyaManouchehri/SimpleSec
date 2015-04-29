using System.Collections.Generic;
using System.Security.Claims;
using NUnit.Framework;
using SimpleSec.ClaimsTransformation.Extensions;
using SimpleSec.ClaimsTransformation.Transformations;

namespace SimpleSec.Tests.ClaimsTransformation.Transformations
{
    [TestFixture]
    class SetDefaultValueTests
    {
        private const string ClaimType = "TestClaim";
        private const string ClaimTypeUpperCase = "TESTCLAIM";
        private const string ClaimValue = "ClaimValue";

        [Test]
        public void GivenAClaimDoesntExist_WhenTransformed_ThenDefaultValueIsAdded()
        {
            var claims = new List<Claim>
            {
                new Claim("ClaimA", "ValueA"),
                new Claim("ClaimB", "ValueB"),
            };

            new SetDefaultValue(ClaimType, ClaimValue).Apply(claims);

            Assert.That(claims, Has.Count.EqualTo(3));
            Assert.That(claims.Find(ClaimType).Value, Is.EqualTo(ClaimValue));
        }

        [Test]
        [TestCase(ClaimType)]
        [TestCase(ClaimTypeUpperCase)]
        public void GivenAClaimAlreadyExists_WhenTransformed_ThenDefaultValueIsNotAdded(string inputClaimType)
        {
            var claims = new List<Claim>
            {
                new Claim("ClaimA", "ValueA"),
                new Claim("ClaimB", "ValueB"),
                new Claim(ClaimType, ClaimValue)
            };

            new SetDefaultValue(inputClaimType, "SomeNewDefaultValue").Apply(claims);

            Assert.That(claims, Has.Count.EqualTo(3));
            Assert.That(claims.Find(ClaimType).Value, Is.EqualTo(ClaimValue));
        }
    }
}
