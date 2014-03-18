# JPush API client library for C Sharp

## 概述
这是 JPush REST API 的 C# 版本封装开发包，是由极光推送官方提供的，一般支持最新的 API 功能。

对应的 REST API 文档：<http://docs.jpush.cn/display/dev/REST+API>

## 环境配置

## 使用样例

### 推送样例
```


   JPushClient client = new JPushClient(app_key, master_secret);
   result = client.sendNotificationAll("notify content");

```

### 统计获取样例

```
    String msg_ids = "1613113584,1229760629,1174658841,1174658641";
    ReceivedResult receivedResult = client.getReceivedApi(msg_ids);
```


## 版本更新
[Release页面](https://github.com/jpush/jpush-api-php-client/releases/) 有详细的版本发布记录与下载。
