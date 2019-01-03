# JPush Library for .NET

[![NuGet](https://img.shields.io/badge/NuGet-v1.1.4-blue.svg)](https://preview.nuget.org/packages/Jiguang.JPush/)

由[极光](https://www.jiguang.cn/)官方支持的 JPush .NET API Client。

> 注意：**Jiguang.JPush** 为基于 .NET Standard 的重构版本，API 用法有较大改变，不兼容旧版本（**cn.jpush.api**）,升级前请注意。

项目中的 Example 为 .NET Core 控制台应用。

开发工具：Visual Studio 2017。

## Install

- [NuGet](https://preview.nuget.org/packages/Jiguang.JPush/)

## Documents

[REST API documents](https://docs.jiguang.cn/jpush/server/push/server_overview/).

## Support

[极光社区](https://community.jiguang.cn/)

## FAQ

1. 如果调用异步方法时出现死锁，即一直没有返回 [HttpResponse](https://github.com/jpush/jsms-api-csharp-client/blob/v2-dev/Jiguang.JSMS/Model/HttpResponse.cs)，可参考这篇[文章](https://blogs.msdn.microsoft.com/jpsanders/2017/08/28/asp-net-do-not-use-task-result-in-main-context/)。

1. 如果使用的是 .NET Framework 4.0，则需要使用 [v1](https://github.com/jpush/jpush-api-csharp-client/tree/v1) 版本。

## Contribute

Please contribute! [Look at the issues](https://github.com/jpush/jpush-api-csharp-client/issues).

## License

MIT © [JiGuang](https://github.com/jpush/jpush-api-csharp-client/blob/master/license)

## Extensions

.Net core 2.0 可以使用[Jiguang.JPush.Extensions](https://github.com/Weidaicheng/jpush-api-csharp-client.Extensions)扩展添加注入，方便使用。
