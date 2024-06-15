using Atlassian.Bitbucket.Application.Commits.Queries.GetDiffStats;
using Atlassian.Bitbucket.Application.Commits.Queries.GetDiffStats.Dtos;
using Atlassian.Bitbucket.Domain.Enums;

namespace Atlassian.Bitbucket.Builders.Application.Commits.Queries.GetDiffStats;

public class ResponseBuilder
{
    public Response Build()
    {
        return new Response(
            Values: new List<DiffStatDto>
            {
                new(1, 1, CommitDiffStatStates.Modified)
            });
    }
}