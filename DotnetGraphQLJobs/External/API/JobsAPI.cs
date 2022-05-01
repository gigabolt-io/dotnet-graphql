using System.Net.Http.Headers;
using System.Text;
using DotnetGraphQLJobs.Models;
using Newtonsoft.Json;

namespace DotnetGraphQLJobs.External.API;

public class JobsApi
{
    private readonly string _baseUrl = "https://www.reed.co.uk/api/1.0/search";
    private readonly string? _apiKey = Environment.GetEnvironmentVariable("API_KEY");

    public async Task<ApiResult> GetJobs(string? keyword = null)
    {
        var url = $"{_baseUrl}?keywords={keyword}";
        if (string.IsNullOrEmpty(keyword))
        {
            url = $"{_baseUrl}";
        }

        var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_apiKey}:")));
        var response = await client.GetAsync(url);
        if (!response.IsSuccessStatusCode) return new ApiResult();
        var json = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<ApiResult>(json);
        return result;
    }

    public async Task<Job?> GetJob(int id)
    {
        var url = $"{_baseUrl}?jobId={id}";
        var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_apiKey}:")));
        var response = await client.GetAsync(url);
        if (!response.IsSuccessStatusCode) return new Job();
        var json = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<ApiResult>(json);
        
        return (result.Results ?? Array.Empty<Job>()).FirstOrDefault(j=> j.Id == id);
    }
}