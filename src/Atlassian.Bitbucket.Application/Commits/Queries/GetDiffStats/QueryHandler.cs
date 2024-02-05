using MediatR;
using Newtonsoft.Json;

namespace Atlassian.Bitbucket.Application.Commits.Queries.GetDiffStats;

public class QueryHandler(HttpClient httpClient) : IRequestHandler<Query, Response>
{
    public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
    {
        var response = await httpClient.GetAsync(
            $"repositories/dev-falc/{request.RepositoryId}/diffstat/{request.CommitHash}",
            cancellationToken);

        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonConvert.DeserializeObject<Response>(content)!;
    }
}