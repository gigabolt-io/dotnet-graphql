using DotnetGraphQLJobs.Domain;

namespace DotnetGraphQLJobs.Application.Interfaces;

public interface IJobService
{
    Task<IEnumerable<Job>> GetJobs(string? keyword = null, int? skip = null, int? take = null);
}