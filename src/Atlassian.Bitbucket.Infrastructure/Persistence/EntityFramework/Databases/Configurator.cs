using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Atlassian.Bitbucket.Infrastructure.Persistence.EntityFramework.Databases;

public class Configurator(
    DbContextOptionsBuilder dbContextOptionsBuilder,
    IConfiguration configuration)
{
    public Configurator UsePostgres()
    {
        dbContextOptionsBuilder.UseNpgsql(configuration.GetConnectionString("Postgres"));
        return this;
    }

    public Configurator EnableSensitiveDataLogging()
    {
        dbContextOptionsBuilder.EnableSensitiveDataLogging();
        return this;
    }
}