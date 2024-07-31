using Atlassian.Bitbucket.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlassian.Bitbucket.Infrastructure.Persistence.EntityFramework.Configurations;

public class FailedRequestConfiguration : IEntityTypeConfiguration<FailedRequest>
{
    public void Configure(EntityTypeBuilder<FailedRequest> builder)
    {
        builder.ToTable(nameof(AtlassianBitbucketDbContext.FailedRequests));

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Actor);
        builder.Property(x => x.TimestampUtc);
        builder.Property(x => x.HttpVerb);
        builder.Property(x => x.EncodedPathAndQuery);
        builder.Property(x => x.ResponseStatusCode);
    }
}