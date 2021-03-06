using System;
using System.Linq;
using System.Text.Json;

namespace Aries
{
    public class Json
    {
        private string json { get; set; }

        public Json(string json)
        {
            this.json = json;
        }

        public object Path(string jsonPath)
        {
            PathToken[] pathTokens = TokenHelper.BuildTokens(jsonPath);
            JsonDocument jsonDocument;
            bool documentParsingResult;
            DocumentBuilder.Build(json, out jsonDocument, out documentParsingResult);
            JsonElement prevJsonElement = jsonDocument.RootElement;
            JsonElement currJsonElement = jsonDocument.RootElement;
            for (int index = 1; index < pathTokens.Length; index++)
            {
                if (!pathTokens[index].isArray)
                {
                    documentParsingResult = prevJsonElement.TryGetProperty(pathTokens[index].property, out currJsonElement);
                }
                else
                {
                    try
                    {
                        documentParsingResult = prevJsonElement.TryGetProperty(pathTokens[index].property, out currJsonElement);
                        currJsonElement = currJsonElement.EnumerateArray().ElementAt(pathTokens[index].index);
                    }
                    catch (Exception ex)
                    {
                        documentParsingResult = false;
                    }
                }

                if (!documentParsingResult)
                {
                    Range range = new Range(1, index + 1);
                    string[] invalidTokens = pathTokens[range].ToList().Select(tk => tk.property).ToArray();
                    return $"$.{string.Join('.', invalidTokens)} was not found in Json Document...!!!";
                }
                prevJsonElement = currJsonElement;
            }

            if (prevJsonElement.ValueKind == JsonValueKind.Array)
            {
               return prevJsonElement.ToString();
            }

            if (prevJsonElement.ValueKind == JsonValueKind.Number)
            {
                if (prevJsonElement.TryGetDouble(out double dbl))
                    return dbl;
                if (prevJsonElement.TryGetDecimal(out decimal dcml))
                    return dcml;
                if (prevJsonElement.TryGetInt64(out long lng))
                    return lng;
                if (prevJsonElement.TryGetInt32(out int integer))
                    return integer;
                if (prevJsonElement.TryGetInt16(out short shrt))
                    return shrt;
                if (prevJsonElement.TryGetByte(out byte byt))
                    return byt;
            }

            if (prevJsonElement.ValueKind == JsonValueKind.String)
            {
                if (prevJsonElement.TryGetDateTime(out DateTime dtm))
                    return dtm;
                if (prevJsonElement.TryGetDateTimeOffset(out DateTimeOffset dtmo))
                    return dtmo;
                if (prevJsonElement.TryGetGuid(out Guid guid))
                    return guid;
                return prevJsonElement.GetString();
            }

            if (prevJsonElement.ValueKind == JsonValueKind.True || prevJsonElement.ValueKind == JsonValueKind.False)
            {
                return prevJsonElement.GetBoolean();
            }

            return JsonValueKind.Null;
        }
    }
}
