using System.Net.Http.Headers;
using System.Text;
using DotnetGraphQLJobs.Application.Interfaces;
using DotnetGraphQLJobs.Application.Services.Jobs;
using DotnetGraphQLJobs.Domain;
using DotnetGraphQLJobs.Domain.Job;
using DotnetGraphQLJobs.External.Reed;
using DotnetGraphQLJobs.GraphQL.Types;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddGraphQLServer()
    .AddQueryType();

builder.Services.AddHttpClient("ReedAPIClient", client =>
{
    client.BaseAddress = new Uri("https://www.reed.co.uk/api/1.0/search");
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
        Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Environment.GetEnvironmentVariable("API_KEY")}:")));
    client.Timeout = TimeSpan.FromMinutes(1);
});

builder.Services
    .AddScoped<IJobService, JobService>()
    .AddScoped<IJobRepository, ReedApi>()
    .AddTransient<IJobRepositoryFactory, JobRepositoryFactory>();
    

var app = builder.Build();

app.MapGraphQL();

app.MapGet("/", () => "Hello World!");

app.Run();