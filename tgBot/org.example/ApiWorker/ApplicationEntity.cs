using System.Text.Json.Serialization;

namespace tgBot.org.example.ApiWorker;

public class ApplicationEntity
{
    //[JsonPropertyName("id")] public string Id { get; set; }
    public long UserId { get; set; }
    public int AddressId { get; set; }
    public string NumberCabinet { get; set; }
    public string FullName { get; set; }
    public string NumberPhone { get; set; }
    public string DescriptionProblem { get; set; }
    public string Photo { get; set; }
}