using System.Text;
using System.Text.Json;

namespace tgBot.org.example.ApiWorker;

public class ApplicationApiWorker
{
    public ApplicationEntity GetByIdApplication(long id)
    {
        HttpClient httpClient = new HttpClient();
        string jsonAsString = httpClient.GetStringAsync($"https://jsonplaceholder.typicode.com/posts/{id}").Result;

        ApplicationEntity applicationEntity = JsonSerializer.Deserialize<ApplicationEntity>(jsonAsString);

        return applicationEntity;
    }

    public ApplicationEntity AddNewApplication(ApplicationEntity insertFakePost)
    {
        HttpClient httpClient = new HttpClient();
        string insertFakePostAsJson = JsonSerializer.Serialize(insertFakePost);

        HttpContent httpContent = new StringContent(insertFakePostAsJson, Encoding.UTF8, "application/json");

        string addedFakePostAsJson = httpClient.PostAsync("https://jsonplaceholder.typicode.com/posts", httpContent)
            .Result.Content.ReadAsStringAsync().Result;

        ApplicationEntity addedFakePost = JsonSerializer.Deserialize<ApplicationEntity>(addedFakePostAsJson);

        return addedFakePost;
    }
    
    public List<HistoryApplication> GetByAllApplication()
    {
        HttpClient httpClient = new HttpClient();
        string jsonAsString = httpClient.GetStringAsync($"https://jsonplaceholder.typicode.com/posts/").Result;

        List<HistoryApplication> historyApplications =
            JsonSerializer.Deserialize<List<HistoryApplication>>(jsonAsString);

        return historyApplications;
    }
}