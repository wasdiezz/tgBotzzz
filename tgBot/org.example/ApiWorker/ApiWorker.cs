using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace tgBot.org.example.ApiWorker;

public class ApiWorker
{
    public ApplicationId GetByIdApplication()
    {
        HttpClient httpClient = new HttpClient();

        string jsonAsString = httpClient.GetStringAsync($"https://jsonplaceholder.typicode.com/posts/1").Result;

        JsonObject jsonObject = JsonObject.Parse(jsonAsString).AsObject();

        int id = int.Parse(jsonObject["id"].ToString());

        return new ApplicationId() { Id = id };
    }

    public Application AddNewApplication(Application insertFakePost)
    {
        HttpClient httpClient = new HttpClient();
        string insertFakePostAsJson = JsonSerializer.Serialize(insertFakePost);

        HttpContent httpContent = new StringContent(insertFakePostAsJson, Encoding.UTF8, "application/json");

        string addedFakePostAsJson = httpClient.PostAsync("https://jsonplaceholder.typicode.com/posts", httpContent)
            .Result.Content.ReadAsStringAsync().Result;

        Application addedFakePost = JsonSerializer.Deserialize<Application>(addedFakePostAsJson);

        return addedFakePost;
    }

    public List<History> GetByAllApplication()
    {
        HttpClient httpClient = new HttpClient();
        string jsonAsString = httpClient.GetStringAsync($"https://jsonplaceholder.typicode.com/posts/").Result;

        List<History> historyApplications =
            JsonSerializer.Deserialize<List<History>>(jsonAsString);

        return historyApplications;
    }
}