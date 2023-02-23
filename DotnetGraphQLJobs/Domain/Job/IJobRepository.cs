namespace DotnetGraphQLJobs.Domain.Job;

public interface IJobRepository
{
    Task<IEnumerable<Domain.Job.Job>> GetByKeyword(string? keyword = null);
}