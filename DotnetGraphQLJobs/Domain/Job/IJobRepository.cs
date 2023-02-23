namespace DotnetGraphQLJobs.Domain;

public interface IJobRepository
{
    Task<IEnumerable<Job>> GetByKeyword(string? keyword = null);
}