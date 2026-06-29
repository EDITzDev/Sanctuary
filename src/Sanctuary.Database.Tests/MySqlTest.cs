using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Sanctuary.Core.Configuration;

namespace Sanctuary.Database.Tests;

[TestClass]
public class MySqlTest
{
    private DatabaseContext _context = null!;

    [TestInitialize]
    public void Setup()
    {
        var databaseOptions = new DatabaseOptions
        {
            Provider = DatabaseProvider.MySql,
            ConnectionString = "server=localhost;uid=user;pwd=password;database=sanctuary_test",
            VersionString = "11.6.0-MariaDB"
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