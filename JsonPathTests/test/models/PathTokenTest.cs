using Aries;
using NUnit.Framework;

namespace JsonPathTests.test.models
{
    [TestFixture]
    public class PathTokenTest
    {
        [Test]
        public void ShouldLoadPathTokensCorrectlyForNonArray()
        {
            var pathTokens = new PathToken();

            pathTokens.property = "Weather";
            pathTokens.isArray = false;
            pathTokens.index = -1;

            Assert.IsNotNull(pathTokens);
            Assert.AreEqual("Weather",pathTokens.property);
            Assert.AreEqual(-1,pathTokens.index);
            Assert.IsFalse(pathTokens.isArray);
        }

        [Test]
        public void ShouldLoadPathTokensCorrectlyForArray()
        {
            var pathTokens = new PathToken();

            pathTokens.property = "Climate";
            pathTokens.isArray = true;
            pathTokens.index = 2;

            Assert.IsNotNull(pathTokens);
            Assert.AreEqual("Climate", pathTokens.property);
            Assert.AreEqual(2, pathTokens.index);
            Assert.IsTrue(pathTokens.isArray);
        }

        [Test]
        public void ShouldLoadPathTokensCorrectlyForEmptyObject()
        {
            var pathTokens = new PathToken();

            Assert.IsNotNull(pathTokens);
            Assert.AreEqual(null, pathTokens.property);
            Assert.AreEqual(0, pathTokens.index);
            Assert.IsFalse(pathTokens.isArray);
        }

        [Test]
        public void ShouldNotLoadPathTokensForNullObject()
        {
            PathToken pathTokens = null;

            Assert.IsNull(pathTokens);           
        }
    }
}
