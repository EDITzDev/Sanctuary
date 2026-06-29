using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Sanctuary.Core.Configuration;

namespace Sanctuary.Database.Tests;

[TestClass]
public class SqliteTest
{
    private DatabaseContext _context = null!;

    [TestInitialize]
    public void Setup()
    {
        var databaseOptions = new DatabaseOptions
        {
            Provider = DatabaseProvider.Sqlite,
            ConnectionString = "Data Source=:memory:"
        };

        var builder = new DbContextOptionsBuilder<DatabaseContext>();

        DatabaseFactory.CreateInstance(builder, databaseOptions);

        _context = new DatabaseContext(builder.Options);
    }

    [TestCleanup]
    public void Cleanup()
    {
        Assert.IsTrue(_context.Database.EnsureDeleted());

        _context.Dispose();
    }

    [TestMethod]
    public void IsValid()
    {
        _context.Database.Migrate();

        Assert.IsTrue(_context.Database.CanConnect());
    }
}