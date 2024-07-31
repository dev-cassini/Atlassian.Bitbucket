using System.Reflection;
using Atlassian.Bitbucket.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Atlassian.Bitbucket.Infrastructure.Persistence.EntityFramework;

public class AtlassianBitbucketDbContext : DbContext
{
    public DbSet<FailedRequest> FailedRequests { get; set; }
    
    protected AtlassianBitbucketDbContext() { }
    
    public AtlassianBitbucketDbContext(DbContextOptions<AtlassianBitbucketDbContext> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entity.GetProperties().Where(x => x.IsPrimaryKey()))
            {
                property.ValueGenerated = ValueGenerated.Never;
            }
        }
    }
}