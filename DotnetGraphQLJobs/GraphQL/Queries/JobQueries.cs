using DotnetGraphQLJobs.Application.Interfaces;
using DotnetGraphQLJobs.GraphQL.Types;

namespace DotnetGraphQLJobs.GraphQL.Queries;

[ExtendObjectType(typeof(Job))]
public class JobQueries
{
    // public async Task<Job?> GetJob(int id)
    // {
    //     // var jobApi = new JobsApi();
    //     // var job = await jobApi.GetJob(id);
    //     // return job;
    //     return new Job();
    // }

    public async Task<IEnumerable<Job>?> GetJobs([Service] IJobService jobService, string? keywords = null, int? skip = null, int? take = null)
    {
        var jobs = await jobService.GetJobs(keywords, skip, take);

        return jobs.Select(job => new Job
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
            })
            .ToList();
    }
}