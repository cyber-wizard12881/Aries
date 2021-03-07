using Aries;
using NUnit.Framework;
using System;
using System.IO;

namespace JsonPathTests.test.parsers
{
    [TestFixture]
    public class JsonTest
    {
        string validJson;
        string invalidJson;

        [OneTimeSetUp]
        public void SetUpFixture()
        {
            validJson = File.ReadAllText(@"test/inputs/Weather.json");
            invalidJson = File.ReadAllText(@"test/inputs/InvalidWeather.json");
        }

        [Test]
        public void ShouldParseValidJsonWithRootElement()
        {
            string jsonPath = @"$";
            Json json = new Json(validJson);

            object outputJson = json.Path(jsonPath);

            Assert.IsNotNull(outputJson);
            Assert.AreEqual(validJson, Convert.ToString(outputJson));
        }

        [Test]
        public void ShouldNotParseInValidJsonWithRootElement()
        {
            string jsonPath = @"$";
            Json json = new Json(invalidJson);

            object outputJson = json.Path(jsonPath);

            Assert.IsNotNull(outputJson);
            Assert.AreEqual("Input Json is Malformed! ... hence Aborting the JsonPath Processing ...!!!", Convert.ToString(outputJson));
        }

        [Test]
        public void ShouldParseValidJsonWithRootElementAndObject()
        {
            string jsonPath = @"$.Weather";
            Json json = new Json(validJson);

            object outputJson = json.Path(jsonPath);

            string expectedOutputJson = File.ReadAllText(@"test/inputs/WeatherFragment.json");

            Assert.IsNotNull(outputJson);
            Assert.AreEqual(expectedOutputJson, Convert.ToString(outputJson));
        }

        [Test]
        public void ShouldNotParseInValidJsonWithRootElementAndObject()
        {
            string jsonPath = @"$.Weather";
            Json json = new Json(invalidJson);

            object outputJson = json.Path(jsonPath);

            Assert.IsNotNull(outputJson);
            Assert.AreEqual("Input Json is Malformed! ... hence Aborting the JsonPath Processing ...!!!", Convert.ToString(outputJson));
        }

        [Test]
        public void ShouldParseValidJsonWithRootElementAndArray()
        {
            string jsonPath = @"$.Weather.Summary.Climate";
            Json json = new Json(validJson);

            object outputJson = json.Path(jsonPath);

            object expectedOutputJson = @"[ ""Hot"", ""Sunny"", ""Windy"", ""Humid"", ""Cloudy"" ]";

            Assert.IsNotNull(outputJson);
            Assert.AreEqual(expectedOutputJson, Convert.ToString(outputJson));
        }

        [Test]
        public void ShouldNotParseInValidJsonWithRootElementAndArray()
        {
            string jsonPath = @"$.Weather.Summary.Climate";
            Json json = new Json(invalidJson);

            object outputJson = json.Path(jsonPath);

            Assert.IsNotNull(outputJson);
            Assert.AreEqual("Input Json is Malformed! ... hence Aborting the JsonPath Processing ...!!!", Convert.ToString(outputJson));
        }

        [Test]
        public void ShouldParseValidJsonWithRootElementAndArrayAccessor()
        {
            string jsonPath = @"$.Weather.Summary.Climate[2]";
            Json json = new Json(validJson);

            object outputJson = json.Path(jsonPath);

            object expectedOutputJson = @"Windy";

            Assert.IsNotNull(outputJson);
            Assert.AreEqual(expectedOutputJson, Convert.ToString(outputJson));
        }

        [Test]
        public void ShouldParseValidJsonWithRootElementAndArrayAccessorAndObject()
        {
            string jsonPath = @"$.Weather.Summary.Description[1]";
            Json json = new Json(validJson);

            object outputJson = json.Path(jsonPath);

            object expectedOutputJson = @"{
          ""Message"": ""Sunny Day""
        }";

            Assert.IsNotNull(outputJson);
            Assert.AreEqual(expectedOutputJson, Convert.ToString(outputJson));
        }

        [Test]
        public void ShouldParseValidJsonWithRootElementAndObjects()
        {
            string jsonPath = @"$.Weather.Summary.Description[3].Message";
            Json json = new Json(validJson);

            object outputJson = json.Path(jsonPath);

            object expectedOutputJson = @"Humid Night";

            Assert.IsNotNull(outputJson);
            Assert.AreEqual(expectedOutputJson, Convert.ToString(outputJson));
        }

        [Test]
        public void ShouldParseValidJsonWithRootElementAndObjectsAsDate()
        {
            string jsonPath = @"$.Weather.Date";
            Json json = new Json(validJson);

            object outputJson = json.Path(jsonPath);

            object expectedOutputJson = DateTime.Parse(@"2021-03-06T10:10:00+05:30");

            Assert.IsNotNull(outputJson);
            Assert.AreEqual(Convert.ToString(expectedOutputJson), Convert.ToString(outputJson));
        }

        [Test]
        public void ShouldParseValidJsonWithRootElementAndObjectsAsInteger()
        {
            string jsonPath = @"$.Weather.TemperatureCelsius";
            Json json = new Json(validJson);

            object outputJson = json.Path(jsonPath);

            object expectedOutputJson = 24;

            Assert.IsNotNull(outputJson);
            Assert.AreEqual(Convert.ToInt32(expectedOutputJson), Convert.ToInt32(outputJson));
        }

        [Test]
        public void ShouldParseValidJsonWithRootElementAndObjectsAsString()
        {
            string jsonPath = @"$.Weather.Summary.Climate[4]";
            Json json = new Json(validJson);

            object outputJson = json.Path(jsonPath);

            object expectedOutputJson = "Cloudy";

            Assert.IsNotNull(outputJson);
            Assert.AreEqual(Convert.ToString(expectedOutputJson), Convert.ToString(outputJson));
        }

        [Test]
        public void ShouldParseValidJsonWithRootElementAndObjectsAsDouble()
        {
            string jsonPath = @"$.Weather.Humidity";
            Json json = new Json(validJson);

            object outputJson = json.Path(jsonPath);

            object expectedOutputJson = 79.25;

            Assert.IsNotNull(outputJson);
            Assert.AreEqual(Convert.ToDouble(expectedOutputJson), Convert.ToDouble(outputJson));
        }

        [Test]
        public void ShouldParseValidJsonWithRootElementAndObjectsAsGuid()
        {
            string jsonPath = @"$.Weather.Guid";
            Json json = new Json(validJson);

            object outputJson = json.Path(jsonPath);

            string expectedOutputJson = "00000000-0000-0000-0000-000000000000";

            Assert.IsNotNull(outputJson);
            Assert.AreEqual(Guid.Parse(expectedOutputJson), outputJson);
        }

        [Test]
        public void ShouldParseValidJsonWithRootElementAndObjectsAsLong()
        {
            string jsonPath = @"$.Weather.Milliseconds";
            Json json = new Json(validJson);

            object outputJson = json.Path(jsonPath);

            long expectedOutputJson = 1111111111;

            Assert.IsNotNull(outputJson);
            Assert.AreEqual(expectedOutputJson, outputJson);
        }
    }
}
