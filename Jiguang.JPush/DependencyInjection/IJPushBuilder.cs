#if NET45
#else
namespace Jiguang.JPush.DependencyInjection
{
    /// <summary>
    /// JPush Builder
    /// </summary>
    public interface IJPushBuilder
    {
        /// <summary>
        /// 添加JPush客户端
        /// </summary>
        void AddJPushClient();
    }
}
#endif