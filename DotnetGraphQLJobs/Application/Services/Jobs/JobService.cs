using DotnetGraphQLJobs.Application.Interfaces;
using DotnetGraphQLJobs.Domain;

namespace DotnetGraphQLJobs.Application.Services.Jobs;

public class JobService : IJobService
{
    private readonly IJobRepository _jobs;

    public JobService(IJobRepository jobs)
    {
        _jobs = jobs;
    }

    public async Task<IEnumerable<Job>> GetJobs(string? keyword = null, int? skip = null, int? take = null)
    {
        var jobs = await _jobs.GetByKeyword(keyword);
        if (skip != null)
        {
            jobs = jobs.Skip(skip.Value);
        }
        if (take != null)
        {
            jobs = jobs.Take(take.Value);
        }
        return jobs;
    }
}