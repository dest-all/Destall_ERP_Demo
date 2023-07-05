using Protocol.Models;
using Protocol.Models.DataHolders;

namespace Protocol.Tests
{

    public class ReferencesComparison
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void JsonModels_ShouldBeEqual()
        {
            var ref1 = new UserReference()
            {
                Id = 15
            };

            var ref2 = new UserReference()
            {
                Id = 15
            };

            Assert.AreEqual(ref1, ref2);
        }

        [Test]
        public void ReferrableModels_ToString_ShouldContainRepresentation()
        {
            const string repr = "Representation to show";

            IModelBase user = new UserModel()
            {
                Reference = new UserReference
                {
                    Id = 15,
                    Representation = repr
                }
            };

            var userString = user.ToString();

            Assert.IsTrue(userString.Contains(repr));
        }

    }
}