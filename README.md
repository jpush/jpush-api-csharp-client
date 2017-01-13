# JPush API client library for CSharp

## 概述
这是 JPush REST API 的 C# 版本封装开发包，是由极光推送官方提供的，一般支持最新的 API 功能。

对应的 REST API 文档：<http://docs.jiguang.cn/jpush/server/push/server_overview/>

## 支持版本
Microsoft. NET Framework 4.0 （包括）以上版本

## 环境配置
在 [jpush-api-csharp-client](https://github.com/jpush/jpush-api-csharp-client) 项目根目录可以下载下面的两个文件。

*   在项目引用中添加依赖包： Newtonsoft.Json.dll

*   在项目引用中添加： cn.jpush.api.dll

在线方式配置：

*   在项目->引用->管理 NuGet 程序包中搜索 cn.jpush.api。（如果已经安装了 Newtonsoft 需要先卸载一下。）
*   NuGet 包管理工具会下载 jpush-api-csharp-client 和 Newtonsoft 依赖。

## Push API v3

向某单个设备或者某设备列表推送一条通知或者消息

推送的载体：PushPayload

对应 REST API 中 Push API v3 的 json 格式说明文档 <http://docs.jpush.io/server/rest_api_v3_push/>

推送样例

>以下片断来自项目代码里的文件：cn.jpush.api.example 中的 JPushApiExample.cs

```
PushPayload payload = PushObject_All_All_Alert();
try
{
    var result = client.SendPush(payload);
    System.Threading.Thread.Sleep(10000);
    var apiResult = client.getReceivedApi(result.msg_id.ToString());
    var apiResultv3 = client.getReceivedApi_v3(result.msg_id.ToString());
    var queryResultWithV2 = client.getReceivedApi("1739302794");
    var querResultWithV3 = client.getReceivedApi_v3("1739302794");
}
catch (APIRequestException e)
{
    Console.WriteLine("Error response from JPush server. Should review and fix it. ");
    Console.WriteLine("HTTP Status: " + e.Status);
    Console.WriteLine("Error Code: " + e.ErrorCode);
    Console.WriteLine("Error Message: " + e.ErrorMessage);
}
catch (APIConnectionException e)
{
    Console.WriteLine(e.Message);
}
```
进行推送的关键在于构建一个 PushPayload 对象。以下示例一般的构建对象的用法。
* 快捷地构建推送对象：所有平台，所有设备，内容为 ALERT 的通知。
```
public static PushPayload PushObject_All_All_Alert()
{
    PushPayload pushPayload = new PushPayload();
    pushPayload.platform = Platform.all();
    pushPayload.audience = Audience.all();
    pushPayload.notification = new Notification().setAlert(ALERT);
    return pushPayload;
}
```

* 构建推送对象：所有平台，推送目标是别名为 "alias1"，通知内容为 ALERT。
```
public static PushPayload PushObject_all_alias_alert()
{
    PushPayload pushPayload_alias = new PushPayload();
    pushPayload_alias.platform = Platform.android();
    pushPayload_alias.audience = Audience.s_alias("alias1");
    pushPayload_alias.notification = new Notification().setAlert(ALERT);
    return pushPayload_alias;
}
```

* 构建推送对象：平台是 Android，目标是 tag 为 "tag1" 的设备，内容是 Android 通知 ALERT，并且标题为 TITLE。

```
public static PushPayload PushObject_Android_Tag_AlertWithTitle()
{
    PushPayload pushPayload = new PushPayload();
    pushPayload.platform = Platform.android();
    pushPayload.audience = Audience.s_tag("tag1");
    pushPayload.notification =  Notification.android(ALERT,TITLE);
    return pushPayload;
}

```

* 构建推送对象：平台是 iOS，推送目标是 "tag1", "tag_all" 的并集，推送内容同时包括通知与消息 - 通知信息是 ALERT，角标数字为 5，通知声音为 "happy"，并且附加字段 from = "JPush"；消息内容是 MSG_CONTENT。通知是 APNs 推送通道的，消息是 JPush 应用内消息通道的。APNs 的推送环境是“生产”（如果不显式设置的话，Library 会默认指定为开发）

```
public static PushPayload PushObject_ios_tagAnd_alertWithExtrasAndMessage()
{
    PushPayload pushPayload = new PushPayload();
    pushPayload.platform = Platform.android_ios();
    pushPayload.audience = Audience.s_tag_and("tag1", "tag_all");
    var notification = new Notification();
    notification.IosNotification = new IosNotification().setAlert(ALERT)
                                                        .setBadge(5)
                                                        .setSound("happy")
                                                        .AddExtra("from","JPush";
    pushPayload.notification = notification;
    pushPayload.message = Message.content(MSG_CONTENT);
    return pushPayload;
}
```

* 构建推送对象：平台是 Android 与 iOS，推送目标是 （"tag1" 与 "tag2" 的交集）并（"alias1" 与 "alias2" 的交集），推送内容是 - 内容为 MSG_CONTENT 的消息，并且附加字段 from = JPush。

```
public static PushPayload PushObject_ios_audienceMore_messageWithExtras()
{         
    var pushPayload = new PushPayload();
    pushPayload.platform = Platform.android_ios();
    pushPayload.audience = Audience.s_tag("tag1","tag2");
    pushPayload.message = Message.content(MSG_CONTENT).AddExtras("from", "JPush");
    return pushPayload;
}

```

* 构建推送对象：推送内容包含 SMS 信息

```
public static PushPayload PushSendSmsMessage()
{
    var pushPayload = new PushPayload();
    pushPayload.platform = Platform.all();
    pushPayload.audience = Audience.all();
    pushPayload.notification = new Notification().setAlert(ALERT);
    SmsMessage sms_message = new SmsMessage();
    sms_message.setContent(SMSMESSAGE);
    sms_message.setDelayTime(DELAY_TIME);
    pushPayload.sms_message = sms_message;
    return pushPayload;
}

```

## Report API V3
JPush Report API V3 提供各类统计数据查询功能。
>以下片断来自项目代码里的文件：cn.jpush.api.examples 中的 ReportsExample.cs

```
try
{
    var result = jpushClient.getReceivedApi("1942377665");
    Console.WriteLine("Got result - " + result.ToString());
}
catch (APIRequestException e)
{
    Console.WriteLine("Error response from JPush server. Should review and fix it.");
    Console.WriteLine("HTTP Status: " + e.Status);
    Console.WriteLine("Error Code: " + e.ErrorCode);
    Console.WriteLine("Error Message: " + e.ErrorMessage);
}
catch (APIConnectionException e)
{
    Console.WriteLine(e.Message);
}

```

## Device API
Device API 用于在服务器端查询、设置、更新、删除设备的 tag, alias 信息，使用时需要注意不要让服务端设置的标签又被客户端给覆盖了。

>以下片断来自项目代码里的文件：cn.jpush.api.examples 中的 DeviceApiExample.cs

```
try
{
    var result = client.updateDevice(REGISTRATION_ID, ALIAS, MOBILE,
        TAG_HASHSET, TAG_HASHSET_REMOVE);
    System.Threading.Thread.Sleep(10000);
    Console.WriteLine(result);
}
catch (APIRequestException e)
{
    Console.WriteLine("Error response from JPush server. Should review and fix it.");
    Console.WriteLine("HTTP Status: " + e.Status);
    Console.WriteLine("Error Code: " + e.ErrorCode);
    Console.WriteLine("Error Message: " + e.ErrorMessage);
}
catch (APIConnectionException e)
{
    Console.WriteLine(e.Message);
}
```

## API Push Schedule
API 层面支持定时功能。

>以下片断来自项目代码里的文件：cn.jpush.api.examples 中的 ScheduleApiExample.cs

```
TriggerPayload triggerConstructor = new TriggerPayload(START, END,
    TIME_PERIODICAL, TIME_UNIT, FREQUENCY, POINT);
SchedulePayload schedulepayloadperiodical = new SchedulePayload(NAME,
    ENABLED, triggerConstructor, pushPayload);
try
{
    var result = scheduleclient.sendSchedule(schedulepayloadperiodical);
    System.Threading.Thread.Sleep(10000);
    Console.WriteLine(result);
    schedule_id = result.schedule_id;
}
catch (APIRequestException e)
{
    Console.WriteLine("Error response from JPush server. Should review and fix it.");
    Console.WriteLine("HTTP Status: " + e.Status);
    Console.WriteLine("Error Code: " + e.ErrorCode);
    Console.WriteLine("Error Message: " + e.ErrorMessage);
}
catch (APIConnectionException e)
{
    Console.WriteLine(e.Message);
}
```

## 异常

+ APIRequestException

    + 包含 http 错误码：如401, 404等，http 错误信息
    + JPush return code 和 JPush return message

+ APIConnectionException
    + 包含错误的信息：比如超时，无网络等情况

## HTTP 状态码

参考文档：<http://docs.jiguang.cn/jpush/server/push/http_status_code/>

Push v3 API 状态码 参考文档：<http://docs.jiguang.cn/jpush/server/push/rest_api_v3_push/>　

Report API  状态码 参考文档：<http://docs.jiguang.cn/jpush/server/push/rest_api_v3_report/>

Device API 状态码 参考文档：<http://docs.jiguang.cn/jpush/server/push/rest_api_v3_device/>

Push Schedule API 状态码 参考文档：<http://docs.jiguang.cn/jpush/server/push/rest_api_push_schedule/>　

[Release页面](https://github.com/jpush/jpush-api-csharp-client/releases/) 有详细的版本发布记录与下载。
