using DotnetGraphQLJobs.Application.Interfaces;
using DotnetGraphQLJobs.Domain.Job;

namespace DotnetGraphQLJobs.Application.Services.Jobs;

public class JobService : IJobService
{
    
    private readonly IJobRepositoryFactory _jobRepositoryFactory;

    public JobService(IJobRepositoryFactory jobRepositoryFactory)
    {
        _jobRepositoryFactory = jobRepositoryFactory;
    }

    public async Task<IEnumerable<Job>> GetJobs(string? keyword = null, int? skip = null, int? take = null)
    {
        var repo = _jobRepositoryFactory.Create(Constants.Reed);
        var jobs = await repo.GetByKeyword(keyword);
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