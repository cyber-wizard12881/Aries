using System;

namespace Aries
{
    class Program
    {
        static void Main(string[] args)
        {
            string jsonString = @"{""Weather"" : {""Date"": ""2021-03-06T10:10:00+05:30"",""TemperatureCelsius"": 24,""Summary"": {""Climate"": [""Hot"", ""Sunny"", ""Windy"", ""Humid"", ""Cloudy""]}}}";
            Json json = new Json(jsonString);
            object jsonObject = json.Path("$");
            Console.WriteLine(jsonObject);
            jsonObject = json.Path("$.Weather.Date");
            Console.WriteLine(jsonObject);
            jsonObject = json.Path("$.Weather.TemperatureCelsius");
            Console.WriteLine(jsonObject);
            jsonObject = json.Path("$.Weather.Summary.Climate");
            Console.WriteLine(jsonObject);
            jsonObject = json.Path("$.Weather.Summary.Climate[2]");
            Console.WriteLine(jsonObject);
            jsonObject = json.Path("$.Weather.Climate.Cloudy");
            Console.WriteLine(jsonObject);
            Console.ReadKey();
        }
    }
}
