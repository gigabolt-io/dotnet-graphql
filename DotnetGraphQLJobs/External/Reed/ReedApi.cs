using DotnetGraphQLJobs.Domain;
using DotnetGraphQLJobs.Domain.Job;
using DotnetGraphQLJobs.External.Reed.DTO;
using Newtonsoft.Json;
using Job = DotnetGraphQLJobs.Domain.Job.Job;

namespace DotnetGraphQLJobs.External.Reed;

public class ReedApi : IJobRepository
{
    private readonly IHttpClientFactory _httpClientFactory;
    private const string SearchUrl = "?keywords={0}";
    public ReedApi(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    
    //
    // public async Task<Job?> GetJob(int id)
    // {
    //     var url = $"{_baseUrl}?jobId={id}";
    //     var client = new HttpClient();
    //     client.DefaultRequestHeaders.Authorization =
    //         new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_apiKey}:")));
    //     var response = await client.GetAsync(url);
    //     if (!response.IsSuccessStatusCode) return new Job();
    //     var json = await response.Content.ReadAsStringAsync();
    //     var result = JsonConvert.DeserializeObject<ApiResult>(json);
    //     
    //     return (result.Results ?? Array.Empty<Job>()).FirstOrDefault(j=> j.Id == id);
    // }

    public async Task<IEnumerable<Job>> GetByKeyword(string? keyword = null)
    {
        var search = string.Format(SearchUrl, keyword);
        if (string.IsNullOrEmpty(keyword))
        {
            search = null;
        }

        using var httpclient = _httpClientFactory.CreateClient("ReedAPIClient");

        List<Job> JobList = new List<Job>();
        var response = await httpclient.GetAsync(search);
        if (!response.IsSuccessStatusCode) return JobList;
        var json = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<ReedApiResult>(json);
        if (result.Results != null)
            foreach (var job in result.Results)
            {
                JobList.Add(new Job
                {
                    Id = job.Id,
                    Title = job.Title,
                    Description = job.Description,
                    Location = job.Location,
                    Url = job.Url,
                    DateAdded = job.DateAdded,
                    SalaryMin = job.SalaryMin,
                    SalaryMax = job.SalaryMax,
                    Applications = job.Applications
                });
            }

        return JobList;
    }
}