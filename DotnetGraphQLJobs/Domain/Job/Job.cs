namespace DotnetGraphQLJobs.Domain;

public class Job
{
    public Job() {}
    public int Id { get; set; }
    public int? EmployerId { get; set; }
    public string? EmployerName { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Location { get; set; }

    public string? Url { get; set; }

    public string? DateAdded { get; set; }

    public decimal? SalaryMin { get; set; }

    public decimal? SalaryMax { get; set; }
    public int? Applications { get; set; }
}

