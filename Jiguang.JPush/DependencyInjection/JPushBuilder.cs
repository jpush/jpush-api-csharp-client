#if NET45
#else
using Jiguang.JPush.DependencyInjection.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace Jiguang.JPush.DependencyInjection
{
    public class JPushBuilder : IJPushBuilder
    {
        private readonly IServiceCollection _services;
        private readonly IServiceProvider _provider;
        private readonly JPushOptions _options;

        public JPushBuilder(IServiceCollection services)
        {
            _services = services;
            _provider = services.BuildServiceProvider();//get an instance of IServiceProvider
            _options = _provider.GetRequiredService<IOptions<JPushOptions>>().Value;//resolve an instance of AquariusWeixinOptions
        }

        public void AddJPushClient()
        {
            _services.AddSingleton<JPushClient, JPushClient>(provider => new JPushClient(_options.AppKey, _options.MasterSecret));
        }
    }
}
#endif