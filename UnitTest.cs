using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
// ReSharper disable UnusedParameter.Local

namespace RecordFactoryDemo
{
    public class UnitTest
    {
        [Fact]
        public void Test1()
        {
            
            // register the services and build the container
            var services = new ServiceCollection();
            
            // As we are running a test, inject our mock date time wrapper (below).
            // the startup will only add the registration in the Startup if a 
            // registration is not already present

            var mockDateTimeInstance = new MockDateTimeWrapper();
            var mockDate = mockDateTimeInstance.GetCurrentDateTimeUtc();

            var mockUser = new MockUser("Steve Talks Code", 1);

            services.AddSingleton<IDateTimeWrapper>(mockDateTimeInstance);
            services.AddTransient<IUser>(_ => mockUser);

            var startup = new StartUp();
            startup.ConfigureServices(services);
            var provider =services.BuildServiceProvider();

            // get and instance of the factory and call the factory method to create
            // and instance of Product
            var sut = provider.GetRequiredService<IProductFactory>();
            const string prodName = "My new product";
            
            // make sure the registered IUser returns the expected mock
            var user = provider.GetRequiredService<IUser>();
            Assert.Equal(mockUser, user);

            // use record deconstruction for comparison
            var (productName, sku, productUser, dateTime) = sut.Create(prodName);

            // verify result contains expected values
            Assert.Equal(prodName, productName);
            Assert.Equal(mockDate, dateTime);
            Assert.NotEmpty(sku);
            Assert.Equal(mockUser.Name, productUser.Name);
            Assert.Equal(mockUser.UserId, productUser.UserId);
        }

        private class MockDateTimeWrapper : IDateTimeWrapper
        {
            private readonly DateTimeOffset _baselineDateTime =
                new DateTimeOffset(2020, 10, 22, 19, 30, 10, TimeSpan.FromHours(1));
            public DateTime GetCurrentDateTimeUtc() => _baselineDateTime.UtcDateTime;
            public DateTime GetCurrentDateTimeLocal() => _baselineDateTime.DateTime;
            public DateTimeOffset GetCurrentDateTimeOffset() => _baselineDateTime;
        }

        private record MockUser(string Name, int UserId) : IUser;
    }
}
