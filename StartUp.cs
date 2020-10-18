using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace RecordFactoryDemo
{
    public class StartUp
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // this demonstrates how to use a custom delegate as a registered type for instances when a full class 
            // is not required
            services.TryAddSingleton<GetSkuDelegate>(_ => () => Guid.NewGuid().ToString().Replace("-",string.Empty));
            services.TryAddSingleton<UserAccessor>();

            // register the transient IUser so that it can be resolved directly where required ...
            services.TryAddTransient(provider => provider.GetRequiredService<UserAccessor>().GetUser());
            // ... but also register a delegate so that the transient can be accessed from a singleton
            services.AddSingleton<GetCurrentUserDelegate>(provider => provider.GetRequiredService<IUser>);

            services.TryAddSingleton<IDateTimeWrapper, DateTimeWrapper>();
            services.AddSingleton<IProductFactory, ProductFactory>();
        }

        private delegate IUser GetCurrentUserDelegate();

        private class ProductFactory : IProductFactory
        {
            private readonly GetSkuDelegate _skuFactory;
            private readonly GetCurrentUserDelegate _getUser;
            private readonly IDateTimeWrapper _dateTimeWrapper;
            public ProductFactory(
                GetSkuDelegate skuFactory, 
                GetCurrentUserDelegate getUser,
                IDateTimeWrapper dateTimeWrapper)
            {
                _dateTimeWrapper = dateTimeWrapper;
                _getUser = getUser;
                _skuFactory = skuFactory;
            }

            public Product Create(string productName)
            {
                var createdDateTime = _dateTimeWrapper.GetCurrentDateTimeUtc();
                var user = _getUser();
                var newSku = _skuFactory();
                return new Product(productName, newSku, user, createdDateTime);
            }
        }
    }
}

