using System.Runtime.InteropServices.JavaScript;
using System.Text.Json.Serialization;

namespace tgBot.org.example.ApiWorker;

public class HistoryApplication
{
    // public int IdHistoryApplication { get; set; }
    // public string Status { get; set; }
    // public string Address { get; set; }
    // public string Cabinet { get; set; }
    // public string Fullname { get; set; }
    // public string NumberPhone { get; set; }
    // public DateTime DateTime { get; set; }
    // public string Description { get; set; }

    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("title")] public string Title { get; set; }
    [JsonPropertyName("body")] public string Body { get; set; }
    [JsonPropertyName("userId")] public int UserId { get; set; }
}