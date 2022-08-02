namespace API.Repositories;

public abstract class Repository
{
    protected readonly string _connectionString;

    protected Repository(IConfiguration configuration) // constructor
    {
        _connectionString = configuration.GetConnectionString("MSSQL"); // get connectionString
    }
}