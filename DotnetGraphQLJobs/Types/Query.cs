using DotnetGraphQLJobs.External.API;
using DotnetGraphQLJobs.Models;

namespace DotnetGraphQLJobs.Types;

public class Query
{
    public async Task<Job?> GetJob(int id)
    {
        var jobApi = new JobsApi();
        var job = await jobApi.GetJob(id);
        return job;
    }

    public async Task<IEnumerable<Job>?> GetJobs()
    {
        var jobApi = new JobsApi();
        var jobs = await jobApi.GetJobs();
        return jobs.Results;
    }
        
}