FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DotnetGraphQLJobs/DotnetGraphQLJobs.csproj", "DotnetGraphQLJobs/"]
RUN dotnet restore "DotnetGraphQLJobs/DotnetGraphQLJobs.csproj"
COPY . .
WORKDIR "/src/DotnetGraphQLJobs"
RUN dotnet build "DotnetGraphQLJobs.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DotnetGraphQLJobs.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DotnetGraphQLJobs.dll"]
