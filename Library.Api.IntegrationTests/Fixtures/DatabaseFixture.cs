using System;

namespace Library.Api.IntegrationTests.Fixtures
{
    public class DatabaseFixture : IDisposable
    {
        public DatabaseFixture()
        {
            Console.WriteLine($"Calling constructor of {nameof(DatabaseFixture)}");
        }
        public void Dispose()
        {
            Console.WriteLine($"Calling {nameof(Dispose)} on {nameof(DatabaseFixture)}");
        }
    }
}