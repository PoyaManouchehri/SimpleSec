using System.Collections.Generic;
using System.Security.Claims;
using NUnit.Framework;
using SimpleSec.ClaimsTransformation.Extensions;
using SimpleSec.ClaimsTransformation.Transformations;

namespace SimpleSec.Tests.ClaimsTransformation.Transformations
{
    [TestFixture]
    class FilterTests
    {
        [Test]
        public void GivenAListOfClaims_WhenFilteredWithAWhiteList_ThenTheOnlyTheClaimsInTheWhiteListAreKept()
        {
            var claims = new List<Claim>()
            {
                new Claim("ClaimA", "ValueA"),
                new Claim("ClaimB", "ValueB"),
                new Claim("ClaimC", "ValueC"),
                new Claim("ClaimD", "ValueD"),
            };

            new Filter("ClaimB", "CLAimc", "ClaimX", "ClaimY").Apply(claims);

            Assert.That(claims, Has.Count.EqualTo(2));
            Assert.That(claims.Contains("ClaimB"));
            Assert.That(claims.Contains("ClaimC"));
        }
    }
}
