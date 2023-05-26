using System.Text.Json.Serialization;

namespace WebInterractionLib
{
    public class WordsDto
    {
        [JsonPropertyName("WordsCount")]
        public Dictionary<string, int> WordsCount { get; set; } = default!;
    }
}
