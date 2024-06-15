using System.Text.Json;
using MediatR;

namespace Atlassian.Bitbucket.Application.Commits.Queries.GetDiffStats;

public class QueryHandler(HttpClient httpClient) : IRequestHandler<Query, Response>
{
    public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
    {
        var response = await httpClient.GetAsync(
            $"repositories/dev-falc/{request.RepositoryId}/diffstat/{request.CommitHash}",
            cancellationToken);

        var contentStream = await response.Content.ReadAsStreamAsync(cancellationToken);
        return await JsonSerializer.DeserializeAsync<Response>(contentStream, cancellationToken: cancellationToken) ?? throw new Exception("Failed to deserialize diff stat response.");
    }
}