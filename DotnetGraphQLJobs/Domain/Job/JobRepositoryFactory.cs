using DotnetGraphQLJobs.External.Reed;

namespace DotnetGraphQLJobs.Domain.Job;

public interface IJobRepositoryFactory
{
    IJobRepository Create(string key);
}

public class JobRepositoryFactory : IJobRepositoryFactory
{
    private readonly IEnumerable<IJobRepository> _jobRepositories;

    public JobRepositoryFactory(IEnumerable<IJobRepository> jobRepositories)
    {
        _jobRepositories = jobRepositories;
    }

    public IJobRepository Create(string token)
    {
        return token switch
        {
            "REED" => GetRepository(typeof(ReedApi)),
            _ => throw new ArgumentException("Invalid token")
        };
    }

    private IJobRepository GetRepository(Type type)
    {
        return _jobRepositories.FirstOrDefault(x => x.GetType() == type);
    }
}