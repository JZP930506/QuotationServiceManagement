using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace QuotationServiceManagement.Application.Web
{
    public static class JsonSerializerOptionsFactory
    {
        public static JsonSerializerOptions GetCommonOptions()
        {
            return new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }
    }
}