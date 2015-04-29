using System.Security.Claims;
using NUnit.Framework;
using SimpleSec.ClaimsTransformation.Extensions;

namespace SimpleSec.Tests.ClaimsTransformation.Extensions
{
    [TestFixture]
    class ClaimsExtensions
    {
        private Claim[] _claims;

        [SetUp]
        public void Setup()
        {
            _claims = new[]
            {
                new Claim("claimA", "xxxx"),
                new Claim("testclaim", "firstValue"),
                new Claim("claimB", "yyyy"),
                new Claim("testclaim", "secondValue"),
            };
        }

        [Test]
        [TestCase("testclaim")]
        [TestCase("TEstCLaim")]
        public void GivenAListOfClaims_WhenFindIsCalledForAValidClaim_ThenTheFirstOccuranceIsReturned(string claimType)
        {
            var result = _claims.Find(claimType);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.EqualTo("firstValue"));
        }

        [Test]
        public void GivenAListOfClaims_WhenFindIsCalledForAnInvalidClaim_ThenNullIsReturned()
        {
            var result = _claims.Find("nonexistingclaim");

            Assert.That(result, Is.Null);
        }

        [Test]
        [TestCase("testclaim")]
        [TestCase("TEstCLaim")]
        public void GivenAListOfClaims_WhenContainsIsCalledForAValidClaim_ThenTrueIsReturned(string claimType)
        {
            var result = _claims.Contains(claimType);

            Assert.That(result, Is.True);
        }

        [Test]
        public void GivenAListOfClaims_WhenContainsIsCalledForAnInvalidClaim_ThenTrueIsReturned()
        {
            var result = _claims.Contains("nonexistingclaim");

            Assert.That(result, Is.False);
        }
    }
}
