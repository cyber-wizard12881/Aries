using System;
using System.Text.RegularExpressions;

namespace Aries
{
    public class TokenHelper
    {
        public static PathToken[] BuildTokens(string jsonPath)
        {
            string[] properties = jsonPath.TrimStart('$').Split('.');
            PathToken[] tokens = new PathToken[properties.Length];
            for (int index = 1; index < properties.Length; index++)
            {
                tokens[index] = new PathToken();

                if (!properties[index].Contains('['))
                {
                    tokens[index].property = properties[index];
                    tokens[index].isArray = false;
                    tokens[index].index = -1;
                }
                else
                {
                    string arrayProperty = properties[index];
                    string arrayPropertyRegex = @"(\S+)\[(\d+)\]";
                    MatchCollection matches = Regex.Matches(arrayProperty, arrayPropertyRegex);
                    foreach (Match match in matches)
                    {
                        tokens[index].property = match.Groups[1].Value;
                        tokens[index].isArray = true;
                        tokens[index].index = Convert.ToInt32(match.Groups[2].Value);
                    }
                }
            }
            return tokens;
        }

    }
}
