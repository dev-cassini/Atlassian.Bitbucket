using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Atlassian.Bitbucket.Infrastructure.Persistence.EntityFramework;

public class AtlassianBitbucketDbContextDesignTimeFactory : IDesignTimeDbContextFactory<AtlassianBitbucketDbContext>
{
    public AtlassianBitbucketDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AtlassianBitbucketDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=password;Database=Atlassian.Bitbucket.Api;Include Error Detail=true");

        return new AtlassianBitbucketDbContext(optionsBuilder.Options);
    }
}