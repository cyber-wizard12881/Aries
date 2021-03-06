﻿using System.Text.Json;

namespace Aries
{
    public class DocumentBuilder
    {
        public static void Build(string json, out JsonDocument jsonDocument, out bool documentParsingResult)
        {
            JsonDocumentOptions jsonDocumentOptions = new JsonDocumentOptions();
            jsonDocumentOptions.MaxDepth = 64;
            jsonDocument = JsonDocument.Parse(json, jsonDocumentOptions);
            documentParsingResult = false;
        }
    }
}
