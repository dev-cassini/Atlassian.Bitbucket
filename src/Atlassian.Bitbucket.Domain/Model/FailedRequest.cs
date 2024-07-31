using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace Atlassian.Bitbucket.Domain.Model;

public class FailedRequest
{
    public FailedRequest(
        Guid id, 
        HttpContext httpContext,
        int responseStatusCode)
    {
        var userName = httpContext.User
            .Claims.FirstOrDefault(x => x.Type == "name");
        
        var clientName = httpContext.User
            .Claims.FirstOrDefault(x => x.Type == "client_name");
        
        Id = id;
        Actor = userName?.Value ?? clientName?.Value ?? "Anonymous";
        TimestampUtc = DateTimeOffset.UtcNow;
        HttpVerb = httpContext.Request.Method;
        EncodedPathAndQuery = httpContext.Request.GetEncodedPathAndQuery();
        ResponseStatusCode = responseStatusCode;
    }

    public Guid Id { get; }
    public string Actor { get; }
    public DateTimeOffset TimestampUtc { get; }
    public string HttpVerb { get; }
    public string EncodedPathAndQuery { get; }
    public int ResponseStatusCode { get; }
}