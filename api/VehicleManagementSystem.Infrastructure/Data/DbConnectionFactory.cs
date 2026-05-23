using Microsoft.Extensions.Configuration;
using System.Data;

namespace VehicleManagementSystem.Infrastructure.Data;

public class DbConnectionFactory
{
    private readonly string _connectionString;

    public DbConnectionFactory(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
    }

    public IDbConnection Create()
    {
        throw new NotImplementedException("Database connections are disabled. The system is currently running in-memory.");
    }
}
