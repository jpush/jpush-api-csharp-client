#if NET45
#else
namespace Jiguang.JPush.DependencyInjection.Options
{
    /// <summary>
    /// JPush配置
    /// </summary>
    public class JPushOptions
    {
        /// <summary>
        /// App Key
        /// </summary>
        public string AppKey { get; set; }

        /// <summary>
        /// Master Secret
        /// </summary>
        public string MasterSecret { get; set; }
    }
}
#endif