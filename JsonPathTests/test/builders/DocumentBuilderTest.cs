using Aries;
using NUnit.Framework;
using System.IO;
using System.Text.Json;

namespace JsonPath.Tests.test.builders
{
    [TestFixture]
    public class DocumentBuilderTest
    {
        string validJson;
        string invalidJson;

        [OneTimeSetUp]
        public void SetUp()
        {
            validJson = File.ReadAllText(@"test/inputs/Weather.json");  
            invalidJson = File.ReadAllText(@"test/inputs/InvalidWeather.json");  
        }

        [Test]
        public void ShouldBuildJsonDocumentFromJson()
        {
            bool buildResult;
            JsonDocument jsonDocument;
            
            DocumentBuilder.Build(validJson, out jsonDocument, out buildResult);

            Assert.IsTrue(buildResult);
            Assert.IsNotNull(jsonDocument);
        }

        [Test]
        public void ShouldNotBuildJsonDocumentFromInvalidJson()
        {
            bool buildResult;
            JsonDocument jsonDocument;

            DocumentBuilder.Build(invalidJson, out jsonDocument, out buildResult);

            Assert.IsFalse(buildResult);
            Assert.IsNull(jsonDocument);
        }
    }
}
