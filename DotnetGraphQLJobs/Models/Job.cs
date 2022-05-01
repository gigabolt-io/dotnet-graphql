using Newtonsoft.Json;

namespace DotnetGraphQLJobs.Models;

public class Job
{
    [JsonProperty("jobId")]
    public int Id { get; set; }
    public int? EmployerId { get; set; }
    public string? EmployerName { get; set; }
    [JsonProperty("jobTitle")]
    public string? Title { get; set; }
    [JsonProperty("jobDescription")]
    public string? Description { get; set; }
    [JsonProperty("locationName")]
    public string? Location { get; set; }
    [JsonProperty("jobUrl")]
    public string? Url { get; set; }
    [JsonProperty("date")]
    public string? DateAdded { get; set; }
    [JsonProperty("minimumSalary")]
    public decimal? SalaryMin { get; set; }
    [JsonProperty("maximumSalary")]
    public decimal? SalaryMax { get; set; }
    public int? Applications { get; set; }
}

public class ApiResult
{
    [JsonProperty("results")]
    public Job[]? Results { get; set; }
}