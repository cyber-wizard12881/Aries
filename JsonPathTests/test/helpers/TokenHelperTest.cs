using Aries;
using NUnit.Framework;

namespace JsonPathTests.test.helpers
{
    [TestFixture]
    public class TokenHelperTest
    {
        [Test]
        public void ShouldGenerateTokensForRootElementOnly()
        {
            string jsonPath = @"$";
            PathToken[] tokens = TokenHelper.BuildTokens(jsonPath);

            Assert.IsNotNull(tokens);
            Assert.AreEqual(1, tokens.Length);
            Assert.AreEqual(null, tokens[0]);
        }

        [Test]
        public void ShouldGenerateTokensForRootElementAndOneObjectOnly()
        {
            string jsonPath = @"$.Weather";
            PathToken[] tokens = TokenHelper.BuildTokens(jsonPath);

            Assert.IsNotNull(tokens);
            Assert.AreEqual(2, tokens.Length);

            Assert.AreEqual(null, tokens[0]);

            Assert.AreEqual("Weather", tokens[1].property);
            Assert.AreEqual(-1, tokens[1].index);
            Assert.IsFalse(tokens[1].isArray);
        }

        [Test]
        public void ShouldGenerateTokensForRootElementAndOneArrayOnly()
        {
            string jsonPath = @"$.Weather[0]";
            PathToken[] tokens = TokenHelper.BuildTokens(jsonPath);

            Assert.IsNotNull(tokens);
            Assert.AreEqual(2, tokens.Length);

            Assert.AreEqual(null, tokens[0]);

            Assert.AreEqual("Weather", tokens[1].property);
            Assert.AreEqual(0, tokens[1].index);
            Assert.IsTrue(tokens[1].isArray);
        }

        [Test]
        public void ShouldGenerateTokensForRootElementAndOneObjectAndOneArrayOnly()
        {
            string jsonPath = @"$.Weather.Climate[0]";
            PathToken[] tokens = TokenHelper.BuildTokens(jsonPath);

            Assert.IsNotNull(tokens);
            Assert.AreEqual(3, tokens.Length);

            Assert.AreEqual(null, tokens[0]);

            Assert.AreEqual("Weather", tokens[1].property);
            Assert.AreEqual(-1, tokens[1].index);
            Assert.IsFalse(tokens[1].isArray);

            Assert.AreEqual("Climate", tokens[2].property);
            Assert.AreEqual(0, tokens[2].index);
            Assert.IsTrue(tokens[2].isArray);
        }

        [Test]
        public void ShouldGenerateTokensForRootElementAndOneArrayAndOneObjectOnly()
        {
            string jsonPath = @"$.Weather[1].Climate";
            PathToken[] tokens = TokenHelper.BuildTokens(jsonPath);

            Assert.IsNotNull(tokens);
            Assert.AreEqual(3, tokens.Length);

            Assert.AreEqual(null, tokens[0]);

            Assert.AreEqual("Weather", tokens[1].property);
            Assert.AreEqual(1, tokens[1].index);
            Assert.IsTrue(tokens[1].isArray);

            Assert.AreEqual("Climate", tokens[2].property);
            Assert.AreEqual(-1, tokens[2].index);
            Assert.IsFalse(tokens[2].isArray);                        
        }

    }
}
