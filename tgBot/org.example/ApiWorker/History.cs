using System.Text.Json.Serialization;

namespace tgBot.org.example.ApiWorker;

public class History
{
    // public int Id { get; set; }
    // public long ChatId { get; set; }
    // public int AddressId { get; set; }
    // public string Cabinet { get; set; }
    // public string Fullname { get; set; }
    // public string PhoneNumber { get; set; }
    // public string Description { get; set; }
    // public DateTime CreatedDateTime { get; set; }
    // public int StatusId { get; set; }

    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("title")] public string Title { get; set; }
    [JsonPropertyName("body")] public string Body { get; set; }
    [JsonPropertyName("userId")] public int UserId { get; set; }
}