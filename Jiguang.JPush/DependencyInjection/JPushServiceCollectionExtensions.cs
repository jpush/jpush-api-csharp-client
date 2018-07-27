#if NET45
#else
using Jiguang.JPush.DependencyInjection.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Jiguang.JPush.DependencyInjection
{
    /// <summary>
    /// 添加极光推送的DI扩展
    /// </summary>
    public static class JPushServiceCollectionExtensions
    {
        /// <summary>
        /// 添加极光推送
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IJPushBuilder AddJPush(this IServiceCollection services)
        {
            IJPushBuilder builder = new JPushBuilder(services);

            builder.AddJPushClient();

            return builder;
        }

        /// <summary>
        /// 添加极光推送
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="setupAction">The setup action.</param>
        /// <returns></returns>
        public static IJPushBuilder AddJPush(this IServiceCollection services, Action<JPushOptions> setupAction)
        {
            services.Configure(setupAction);
            return services.AddJPush();
        }

        /// <summary>
        /// 添加极光推送
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static IJPushBuilder AddAquariusWeixin(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JPushOptions>(configuration);
            return services.AddJPush();
        }
    }
}
#endif