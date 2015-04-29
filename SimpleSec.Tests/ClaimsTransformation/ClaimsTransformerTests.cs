using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using NSubstitute;
using NUnit.Framework;
using SimpleSec.ClaimsTransformation;
using SimpleSec.ClaimsTransformation.Transformations;

namespace SimpleSec.Tests.ClaimsTransformation
{
    class TestClaimsTransformer : ClaimsTransformer
    {
        public TestClaimsTransformer(IList<IClaimsTransformation> transformations)
        {
            Transformations = transformations;
        }
    }

    class ClaimsTransformerTests
    {
        private IList<IClaimsTransformation> _transformations;
        private TestClaimsTransformer _transformer;

        [SetUp]
        public void Setup()
        {
            _transformations = new List<IClaimsTransformation>();
            _transformer = new TestClaimsTransformer(_transformations);
        }

        [Test]
        public void CopyTransformation()
        {
            _transformer.Copy("Source", "Target", true);

            Assert.That(_transformations, Has.Count.EqualTo(1));
            Assert.That(_transformations[0], Is.TypeOf<Copy>());
        }

        [Test]
        public void SetDefaultValueTransformation()
        {
            _transformer.SetDefaultValue("Claim", "DefaultValue");

            Assert.That(_transformations, Has.Count.EqualTo(1));
            Assert.That(_transformations[0], Is.TypeOf<SetDefaultValue>());
        }

        [Test]
        public void MapTransformation()
        {
            _transformer.Map("Source", "Target", true);

            Assert.That(_transformations, Has.Count.EqualTo(1));
            Assert.That(_transformations[0], Is.TypeOf<Map>());
        }

        [Test]
        public void RemoveTransformation()
        {
            _transformer.Remove("Claim");

            Assert.That(_transformations, Has.Count.EqualTo(1));
            Assert.That(_transformations[0], Is.TypeOf<Remove>());
        }

        [Test]
        public void FilterTransformation()
        {
            _transformer.Filter("Claim1", "Claim2");

            Assert.That(_transformations, Has.Count.EqualTo(1));
            Assert.That(_transformations[0], Is.TypeOf<Filter>());
        }

        [Test]
        public void CustomTransformation()
        {
            var customTransformation = Substitute.For<IClaimsTransformation>();
            _transformer.Transform(customTransformation);

            Assert.That(_transformations, Has.Count.EqualTo(1));
            Assert.That(_transformations[0], Is.SameAs(customTransformation));
        }

        [Test]
        public void GivenANumberOfTransformation_WhenApplied_ThenAllTransformationAreAppliedInOrder()
        {
            var trans1 = Substitute.For<IClaimsTransformation>();
            var trans2 = Substitute.For<IClaimsTransformation>();
            var trans3 = Substitute.For<IClaimsTransformation>();
            _transformer.Transform(trans1);
            _transformer.Transform(trans2);
            _transformer.Transform(trans3);
            var claims = new List<Claim> {new Claim("Claim1", "Value1")};

            _transformer.Apply(claims);

            Received.InOrder(() =>
            {
                trans1.Apply(Arg.Is<IList<Claim>>(x => x.SequenceEqual(claims)));
                trans2.Apply(Arg.Is<IList<Claim>>(x => x.SequenceEqual(claims)));
                trans3.Apply(Arg.Is<IList<Claim>>(x => x.SequenceEqual(claims)));
            });
        }
    }
}
