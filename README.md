# JPush API client library for C Sharp

## 概述
这是 JPush REST API 的 C# 版本封装开发包，是由极光推送官方提供的，一般支持最新的 API 功能。

所有的接口都在cn.jpush.api.JPushClient中

对应的 REST API 文档：<http://docs.jpush.cn/display/dev/REST+API>

## 环境配置

## Push-API-v3

向某单个设备或者某设备列表推送一条通知或者消息

## 推送的载体：类PushPayload

对应REST API中Push-API-v3的json格式说明文档<http://docs.jpush.cn/display/dev/Push-API-v3>

先说名下每个成员变量的用法，再演示下PushPayload如何创建

###Platform     平台信息（必填）
JPush 当前支持Android,Ios,Windows Phone三个平台的推送
支持一下几种方法创建Platform

#### Platform API

将构造函数私有化，不提供new方法进行创建

+ 所有平台推送

		Platform platform=Platform.all() 
+ ios平台
	
		Platform platform=Platform.ios()
+ android平台
	
		Platform platform=Platform.android() 
+ windows phone平台

		Platform platform=Platform.winphone()
+ android和ios平台
	
		Platform platform=Platform.android_ios() 
+ android和winphone平台

		Platform platform=Platform.android_winphone()
+ ios平台和winphone平台

		Platform platform=Platform.ios_winphone() 

###Audience     推送目标（必填）

推送设备对象，表示一条推送可以被推送到那些设备，确认推送设备的对象，JPush提供了多种方式，比如：别名，标签，注册id，分群，广播等。

创建Audience使用`all`和`s_xxxx` 函数，再添加条件时使用不带`s_`的函数，

s_表示是静态函数的意思，Audience中还有个tag的成员函数，为了避免命名冲突

+ 推送所有目标

		Audience audience= Audience.all()

+ 推送给多个标签（只要任意一个标签满足）：在深圳，广州或者北京

		Audience audience= Audience.s_tag("深圳","广州","北京")；
		

+ 推送给多个标签（需要同时在多个标签范围之内）：在深圳并且是女的
		
		Audience audience= Audience.s_tag_and("深圳","女")；

+ 推送给多个别名：
		
		Audience audience= Audience.s_alias("4314", "892", "4531")；

+ 推送给多个注册ID

		Audience audience= Audience.s_registrationId("4312kjklfds2", "8914afd2", "45fdsa31")；

+ 可同时推送指定多类推送目标：在深圳或者广州，并且是 ”女“ “会员”

		Audience audience= Audience.s_tag("广州", "深圳").tag("女"，"会员")；

###Audience API

将构造函数私有化，不提供new方法进行创建，只能用all和s_xxx的函数创建Audience

+ 推送所有目标

		public static Audience all()

+ 推送给多个标签，参数为：HashSet<string> 

		public static Audience s_tag(HashSet<string> values)

+ 推送给多个标签 参数为："xxxxx1","xxxxxx2","xxxxxx3"
 
		public static Audience s_tag(params string[] values)

+ 推送给多个标签（需要同时在多个标签范围之内），参数为：HashSet<string> 

		public static Audience s_tag_and(HashSet<string> values)

+ 推送给多个标签（需要同时在多个标签范围之内），参数为："xxxxx1","xxxxxx2","xxxxxx3"
 
		public static Audience s_tag_and(params string[] values)

+ 推送给多个别名 ，参数为：HashSet<string> ：

		public static Audience s_alias(HashSet<string> values)

+ 推送给多个别名，参数为："xxxxx1","xxxxxx2","xxxxxx3"：

		public static Audience s_alias(params string[] values)

+ 推送给多个segment ，参数为：HashSet<string> ：

		public static Audience s_segment(HashSet<string> values)

+ 推送给多个segment，参数为："xxxxx1","xxxxxx2","xxxxxx3"：

		public static Audience s_segment(params string[] values)

+ 推送给多个设备id ，参数为：HashSet<string> ：

		public static Audience s_registrationId(HashSet<string> values)

+ 推送给多个设备id，参数为："xxxxx1","xxxxxx2","xxxxxx3"：

		public static Audience s_registrationId(params string[] values)

+ 推送给多个标签，参数为：HashSet<string> 

		public Audience tag(HashSet<string> values)

+ 推送给多个标签 参数为："xxxxx1","xxxxxx2","xxxxxx3"

		public Audience tag(params string[] values)

+ 推送给多个标签（需要同时在多个标签范围之内），参数为：HashSet<string> 

		public Audience tag_and(HashSet<string> values)

+ 推送给多个标签（需要同时在多个标签范围之内），参数为："xxxxx1","xxxxxx2","xxxxxx3"
 
		public Audience tag_and(params string[] values)

+ 推送给多个别名 ，参数为：HashSet<string> ：

		public Audience alias(HashSet<string> values)

+ 推送给多个别名，参数为："xxxxx1","xxxxxx2","xxxxxx3"：

		public Audience alias(params string[] values)

+ 推送给多个segment ，参数为：HashSet<string> ：

		public Audience segment(HashSet<string> values)

+ 推送给多个segment，参数为："xxxxx1","xxxxxx2","xxxxxx3"：

		public Audience segment(params string[] values)

+ 推送给多个设备id ，参数为：HashSet<string> ：
		
		public Audience registrationId(HashSet<string> values)

+ 推送给多个设备id，参数为："xxxxx1","xxxxxx2","xxxxxx3"：

		public Audience registrationId(params string[] values)

###Notification 通知内容（可选）

创建方式为 `var not = new Notification()`然后根据需要设置相关字段

Notification字段简介如下：
#### alert

是一个快捷定义，各平台的 alert 信息如果都一样，则可不定义。如果各平台有定义，则覆盖这里的定义。

设置方法如下:
		
	Notification not = new Notification().setAlert("alert");
#### ios
	
iOS 平台上 APNs 通知。

该通知内容会由 JPush 代理发往 Apple APNs 服务器，并在 iOS 设备上在系统通知的方式呈现。

该通知内容满足 APNs 的规范，支持的字段如下：

+ alert 
	+ 通知内容，
	+ 这里指定了，将会覆盖上级统一指定的 alert 信息
	+ 内容为空则不展示到通知栏
	+ 支持 emoji 表情。
	+ 这里不指定则上级 notification 必须指定。
+ sound
	+ 通知提示声音
	+ 如果无此字段，则此消息无声音提示
	+ 有此字段，如果找到了指定的声音就播放该声音，否则播放默认声音
	+ 如果此字段为空字符串，iOS 7 为默认声音，iOS 8 为无声音。
	+ 说明：JPush 官方 API Library (SDK) 会默认填充声音字段。提供另外的方法关闭声音

+ badge
	+ 应用角标
	+ 如果不填，表示不改变角标数字；否则把角标数字改为指定的数字；为 0 表示清除。
	+ 新增支持 "+1" 功能，详情参考：http://blog.jpush.cn/ios_apns_badge_plus/
	+  说明：JPush 官方 API Library (SDK) 会默认填充 badge 值为 "+1"。提供另外的方法不变更 badge 值。

+ content-available
	+ 如果为 true 表示要静默推送。
+ category
	+ 设置APNs payload中的"category"字段值
	+ 说明：ios8才支持该字段。
+ extras
	+ 扩展字段	
	+ 这里自定义 Key/value 信息，以供业务使用。

> IOS 通知 JPush 要转发给 APNs 服务器。APNs 协议定义通知长度为 255 字节。JPush 因为需要重新组包，并且考虑一点安全冗余，要求 “iOS”:{ } 内的总体长度不超过：220 个字节。
> 
> 另外，JPush 在推送时使用 utf-8 编码，所以一个汉字占用 3 个字节长度。


	
#####IosNotification API 
	
+ 设置alert字段
		
		 public IosNotification setAlert(String alert)
+ 使用sound字段无效 

		public IosNotification disableSound()

+ 使用badge字段无效
+ 
	 	public IosNotification disableBadge()
	
+ 设置声音，C# V3 sdk 会默认设置声音为:""
	
		public IosNotification setSound(String sound)
	
+ 设置Badge 
		
		public IosNotification setBadge(int badge)

+ 在原有的badge基础上+1

		 public IosNotification autoBadge()
	 
+ 在原有的badge值加上你所设定值 
	
		public IosNotification incrBadge(int badge)

+ 设置ontentAvailable字段 
	
		public IosNotification setContentAvailable(bool contentAvailable)


+ 增加key和string类型value的Extra字段 
		
		public IosNotification AddExtra(string key, string value)

+ 增加key和int类型value的Extra字段 

		public IosNotification AddExtra(string key, int value) 

+ 增加key和bool类型value的Extra字段
	
		public IosNotification AddExtra(string key, bool value)

那么如何设置Notification的ios字段呢？请参考：

	Notification not = new Notification().setIos(new IosNotification()
	                                             .setAlert("alert")
	                                             .incrBadge(5));
复杂的请类比

#### android
	
Android 平台上的通知。

被 JPush SDK 按照一定的通知栏样式展示。

支持的字段有：

+ alert 
	+ 这里指定了，则会覆盖上级统一指定的 alert 信息
	+ 内容可以为空字符串，则表示不展示到通知栏
	+ 这里不指定则上级 notification 必须指定。
+ title
	+ 如果指定了，则通知里原来展示App名称的地方，将展示成这个字段。
+ builder_id
	+ Android SDK 可设置通知栏样式，这里根据样式 ID 来指定该使用哪套样式。
+ extras
	+ 这里自定义 JSON 格式的 Key/Value 信息，以供业务使用。
	
##### AndroidNotification API 

+ 设置alert字段
	
		public AndroidNotification setAlert(String alert)

+ 设置title字段
	
		 public AndroidNotification setTitle(string title)

+ 设置builder_id字段

		public AndroidNotification setBuilderID(int builder_id)

+ 增加key和string类型value的Extra字段 

		 public AndroidNotification AddExtra(string key, string value)

+ 增加key和int类型value的Extra字段
		
		 public AndroidNotification AddExtra(string key, int value)

+ 增加key和bool类型value的Extra字段
	
		public AndroidNotification AddExtra(string key, bool value)

如何设置Notification的android字段呢？请参考：

	   PushPayload payload = new PushPayload();
	        payload.platform = Platform.all();
	        payload.audience = Audience.all();
	        payload.notification = new Notification()
	                               .setAlert(ALERT)
	                               .setAndroid(new AndroidNotification()
	                                             .AddExtra("key1", "value1")
	                                             .AddExtra("key2", 222));
复杂的请类比

#### winphone

Windows Phone 平台上的通知。

该通知由 JPush 服务器代理向微软的 MPNs 服务器发送，并在 Windows Phone 客户端的系统通知栏上展示。

该通知满足 MPNs 的相关规范。当前 JPush 仅支持 toast 类型：

+ alert
	+ 会填充到 toast 类型 text2 字段上。
	+ 这里指定了，将会覆盖上级统一指定的 alert 信息；
	+ 内容为空则不展示到通知栏。这里不指定则上级 notification 必须指定。
+ title
	+ 会填充到 toast 类型 text1 字段上。
	+ 点击打开的页面。会填充到推送信息的 param 字段上，表示由哪个 App 页面打开该通知。可不填，则由默认的首页打开。
	+ 作为参数附加到上述打开页面的后边。
+ _open_page
	+ 点击打开的页面
	+ 会填充到推送信息的 param 字段上，表示由哪个 App 页面打开该通知
	+ 可不填，则由默认的首页打开
+ extras
	+ + 作为参数附加到上述打开页面的后边
##### WinphoneNotification

+ 设置alert字段

		public WinphoneNotification setAlert(String alert)
+ 设置alert字段

		public WinphoneNotification setOpenPage(String openPage)
+ 设置alert字段

		public WinphoneNotification setTitle(String title)
+ 增加key和string类型value的Extra字段 

		public WinphoneNotification AddExtra(string key, string value)
+ 增加key和int类型value的Extra字段

		public WinphoneNotification AddExtra(string key, int value)
+ 增加key和bool类型value的Extra字段

		public WinphoneNotification AddExtra(string key, bool value)

如何设置Notification的winphone字段呢？,请参考：

	   PushPayload payload = new PushPayload();
	        payload.platform = Platform.all();
	        payload.audience = Audience.all();
	        payload.notification = new Notification()
	                               .setAlert(ALERT)
	                               .setWinphone(new WinphoneNotification()
	                                             .AddExtra("key1", "value1")
	                                             .AddExtra("key2", 222));
复杂的请类比
### Message 消息内容（可选）

应用内消息。或者称作：自定义消息，透传消息。

此部分内容不会展示到通知栏上，JPush SDK 收到消息内容后透传给 App。App 需要自行处理。

iOS 平台上，有此部分内容，才会推送应用内消息通道。

Windows Phone 平台上，暂时不支持应用内消息。

+ msg_content
	+ 消息内容本身
+ title
	+ 消息标题
+ content_type
	+ 消息内容类型
+ extras
	+ JSON 格式的可选参数

	
> Android 1.6.2及以下版本 接收notification 与 message 并存（即本次api调用同时推送通知和消息）的离线推送， 只能收到通知部分，message 部分没有透传给 App。 Android 1.6.3及以上SDK 版本已做相应调整，能正常接收同时推送通知和消息的离线记录。
> 
> iOS 1.7.3及以上的版本才能正确解析v3的message，但是无法解析v2推送通知同时下发的应用内消息
### Message API
+ 创建Message的静态工厂函数，因为cotent是必填字段，所以将狗仔函数私有，只用此API创建

		public static Message content(string msgContent)
+ 设置title字段

		public Message setTitle(String title)
+ 设置content_type字段

		public Message setContentType(String ContentType)

+ 增加key和string类型value的Extra字段

		public Message AddExtras(string key, string value)
+ 增加key和int类型value的Extra字段

		public Message AddExtras(string key, int value)
+ 增加key和bool类型value的Extra字段

		public Message AddExtras(string key, bool value)

如何设置message？请参考：

	 PushPayload payload = new PushPayload();
	    payload.platform = Platform.all();
	    payload.audience = Audience.all();
	    payload.message  = Message.content(MSG_CONTENT)
							      .AddExtras("key1", "value1")
							      .AddExtras("key2", 222)
							      .AddExtras("key3", false);

### Options      可选参数（可选）
推送可选项。

当前包含如下几个可选项：

+ sendno
	+ 推送序号
	+ 纯粹用来作为 API 调用标识，API 返回时被原样返回，以方便 API 调用方匹配请求与返回。
+ time_to_live
	+ 推送当前用户不在线时，为该用户保留多长时间的离线消息，以便其上线时再次推送
	+ 默认 86400 （1 天），最长 10 天。设置为 0 表示不保留离线消息，只有推送当前在线的用户可以收到
+ override_msg_id
	+ 如果当前的推送要覆盖之前的一条推送，这里填写前一条推送的 msg_id 就会产生覆盖效果
	+ 1）该 msg_id 离线收到的消息是覆盖后的内容
	+ 2）即使该 msg_id Android 端用户已经收到，如果通知栏还未清除，则新的消息内容会覆盖之前这条通知
	+ 覆盖功能起作用的时限是：1 天。
	+ 如果在覆盖指定时限内该 msg_id 不存在，则返回 1003 错误，提示不是一次有效的消息覆盖操作，当前的消息不会被推送。
+ apns_production
	+ True 表示推送生产环境，False 表示要推送开发环境； 如果不指定则为推送生产环境。
	+  JPush 官方 API LIbrary (SDK) 默认设置为推送 “开发环境”。
+ big_push_duration
	+ 又名缓慢推送，把原本尽可能快的推送速度，降低下来，在给定的 n 分钟内，均匀地向这次推送的目标用户推送
	+ 最大值为 1440。未设置则不是定速推送。

#### Options API 

+ 设置sendno字段

		private int _sendno
+ 设置time_to_live字段

		public long override_msg_id 
+ 设置apns_production字段

		public long time_to_live 
+ 设置big_push_duration字段

		public long big_push_duration

如何设置 Options？请参考：

	 Options options = new Options();
	 options.sendno=1234;

###Method - MessageResult SendPush(PushPayload payload)

调用此API来进行推送信息，其中包含平台信息，推送目标，通知内容，消息内容与可选参数

#### 支持版本

开始支持的版本:3.0.0

#### 接口定义
	MessageResult SendPush(PushPayload payload)

####参数说明

+ payload 推送的具体数据结构
	
+ MessageResult
	+ msg_id 返回消息号
	+ sendno 由你赋值的字段，这里会传递回来
	+ ResponseResult http返回的相关信息

#### 异常

+ APIRequestException

	+ 包含http错误码：如401,404等，http错误信息
	+ JPush returen code和JPush returen mssage
	
+ APIConnectionException
	+ 包含错误的信息：比如超时，无网络等情况

## Report-API

###Method - MessageResult SendPush(string payloadString)

调用此API来进行推送信息，其中包含平台信息，推送目标，通知内容，消息内容与可选参数

#### 支持版本

开始支持的版本:3.0.0

#### 接口定义
	MessageResult SendPush(string payloadString)

#### 参数说明

+ payloadString，自行组建的json串，如果json字符串不合法，会抛出APIRequestException异常
	
+ MessageResult
	+ msg_id 返回消息号
	+ sendno 由你赋值的字段，这里会传递回来
	+ ResponseResult http返回的相关信息
	
#### 异常

+ APIRequestException

	+ 包含http错误码：如401,404等，http错误信息
	+ JPush returen code和JPush returen mssage
	
+ APIConnectionException
	+ 包含错误的信息：比如超时，无网络等情况

## Report-API

###Method -ReceivedResult getReceivedApi(String msg_ids)

该接口为v2版本的Report-API

Received API 以 msg_id 作为参数，去获取该 msg_id 的送达统计数据。

如果一次 API 调用推送有很多对象（比如广播推送），则此 API 返回的统计数据会因为持续有客户端送达而持续增加。

每条推送消息的送达统计数据最多保留 10 天。即发起推送请求后从最后一个推送送达记录时间点开始保留10天，如果保留期间有新的送达，将在这个新送达的时间点起再往后保留10天


#### 支持版本

开支持的版本:3.0.0

#### 接口定义
	  public ReceivedResult getReceivedApi(String msg_ids)

#### 参数说明

+ msg_ids
	+ msg_ids 推送API返回的 msg_id 列表，多个 msg_id 用逗号隔开，最多支持100个msg_id。
	
+ MessageResult
	+ ReceivedList 查询结果的列表
	+ 列表包含：msg_id 返回消息号
	+ 列表包含：android_received，android平台的送达	
	+ 列表包含：ios_apns_sent，   ios平台想apns发送的数目	
	
#### 异常

+ APIRequestException

	+ 包含http错误码：如401,404等，http错误信息
	+ JPush returen code和JPush returen mssage
	
+ APIConnectionException
	+ 包含错误的信息：比如超时，无网络等情况

###Method -ReceivedResult getReceivedApi_v3(String msg_ids)

该接口为v3版本的Report-API

Received API 以 msg_id 作为参数，去获取该 msg_id 的送达统计数据。

如果一次 API 调用推送有很多对象（比如广播推送），则此 API 返回的统计数据会因为持续有客户端送达而持续增加。

每条推送消息的送达统计数据最多保留 10 天。即发起推送请求后从最后一个推送送达记录时间点开始保留10天，如果保留期间有新的送达，将在这个新送达的时间点起再往后保留10天


#### 支持版本

开支持的版本:3.0.0

#### 接口定义
	  public ReceivedResult getReceivedApi(String msg_ids)

#### 参数说明

+ msg_ids
	+ msg_ids 推送API返回的 msg_id 列表，多个 msg_id 用逗号隔开，最多支持100个msg_id。
	
+ MessageResult
	+ ReceivedList 查询结果的列表
	+ 列表包含：msg_id 返回消息号
	+ 列表包含：android_received，android平台的送达	
	+ 列表包含：ios_apns_sent，   ios平台想apns发送的数目	
	
#### 异常

+ APIRequestException

	+ 包含http错误码：如401,404等，http错误信息
	+ JPush returen code和JPush returen mssage
	
+ APIConnectionException
	+ 包含错误的信息：比如超时，无网络等情况


###Method -UsersResult getReportUsers(TimeUnit timeUnit, String start, int duration)

用户统计查询接口，这个接口是vip用户专用

提供一定时间段的用户相关统计数据：新增用户、在线用户、活跃用户。

时间单位支持：HOUR（小时）、DAY（天）、MONTH（月）。


#### 支持版本

开支持的版本:3.0.0

#### 接口定义
	   public UsersResult getReportUsers(TimeUnit timeUnit, String start, int duration)

#### 参数说明

+ timeUnit
	+ HOUR 小时
	+ DAY 天
	+ MONTH 月
	
+ start
	+ 如果单位是小时，则起始时间是小时（包含天），格式例：2014-06-11 09
	+ 如果单位是天，则起始时间是日期（天），格式例：2014-06-11
	+ 如果单位是月，则起始时间是日期（月），格式例：2014-06
+ duration
	+ 如果单位是天，则是持续的天数。以此类推。

+ UsersResult
	+ time_unit 请求时的时间单位。
	+ start 请求时的起始时间
	+ duration 请求时的持续时长	
	+ items 获取到的统计数据项。是一个 JSON Array。
		+ new 新增用户
		+ online 在线用户
		+ active 活跃用户
	
#### 异常

+ APIRequestException

	+ 包含http错误码：如401,404等，http错误信息
	+ JPush returen code和JPush returen mssage
	
+ APIConnectionException
	+ 包含错误的信息：比如超时，无网络等情况


###Method  MessagesResult getReportMessages(params String[] msgIds)

消息统计查询接口，这个接口是vip用户专用

与“送达统计” API 不同的是，该 API 提供更多的针对一个 msgid 的统计数据。


#### 支持版本

开支持的版本:3.0.0

#### 接口定义
	   public MessagesResult getReportMessages(params String[] msgIds)

#### 参数说明

+ msgIds
	+ msg_ids 推送API返回的 msg_id 列表，多个 msg_id 用逗号隔开，最多支持100个msg_id。
	
+ MessagesResult
	+ msg_id 查询的消息ID
	+ android Android统计数据
		+ target 推送目标数
		+ online_push 在线推送数
		+ received 推送送达数
		+ click 用户点击数
	+ ios iOS统计数据	
		+ apns_target APNs通知推送目标数
		+ apns_sent APNS通知成功推送数
		+ click 用户点击数

#### 异常

+ APIRequestException

	+ 包含http错误码：如401,404等，http错误信息
	+ JPush returen code和JPush returen mssage
	
+ APIConnectionException
	+ 包含错误的信息：比如超时，无网络等情况
## Device-API

> Device API 用于在服务器端查询、设置、更新、删除设备的 tag,alias 信息，使用时需要注意不要让服务端设置的标签又被客户端给覆盖了。
> 
> 如果不是熟悉 tag，alias 的逻辑建议只使用客户端或者服务端二者中的一种。如果是两边同时使用，请确认自己应用可以处理好标签和别名的同步。

### 概述
Device API 用于在服务器端查询、设置、更新、删除设备的 tag,alias 信息。



包含了device, tag, alias 三组API。其中：

device 用于查询/设置设备的各种属性，包含tags, alias；
tag 用于查询/设置/删除设备的标签；
alias 用于查询/设置/删除设备的别名。


###Method  TagAliasResult getDeviceTagAlias(String registrationId)

获取当前设备的所有属性，包含tags, alias。

#### 支持版本

开支持的版本:3.0.0

#### 接口定义
	   public  TagAliasResult getDeviceTagAlias(String registrationId)

#### 参数说明

+ registrationId
	+ 设备id
	
+ TagAliasResult
	+ tags 
		+ 找不到统计项就是 null，否则为["tag1", "tag2"]的数据
	+ alias
		+ 找不到统计项就是 null，否则为统计项的值

#### 异常

+ APIRequestException

	+ 包含http错误码：如401,404等，http错误信息
	+ JPush returen code和JPush returen mssage
	
+ APIConnectionException
	+ 包含错误的信息：比如超时，无网络等情况


### Method - DefaultResult updateDeviceTagAlias(String registrationId, bool clearAlias, bool clearTag)

删除当前设备的所有属性，包括tags和alias

#### 支持版本

开支持的版本:3.0.0

#### 接口定义
	   public  DefaultResult updateDeviceTagAlias(String registrationId, bool clearAlias, bool clearTag)

#### 参数说明

+ registrationId
	+ 设备id
+ clearAlias
	+ true:清空alias
	+ false:保持不变
+ clearTag
	+ true:清空tag
	+ false:保持tag不变

#### 异常

+ APIRequestException

	+ 包含http错误码：如401,404等，http错误信息
	+ JPush returen code和JPush returen mssage
	
+ APIConnectionException
	+ 包含错误的信息：比如超时，无网络等情况

### Method - DefaultResult updateDeviceTagAlias(String registrationId,String alias,HashSet<String> tagsToAdd,HashSet<String> tagsToRemove)

删除当前设备的所有属性，包括tags和alias

#### 支持版本

开支持的版本:3.0.0

#### 接口定义
	   public  DefaultResult updateDeviceTagAlias(String registrationId,String alias,HashSet<String> tagsToAdd,HashSet<String> tagsToRemove)

#### 参数说明

+ registrationId
	+ 设备id
+ alias
	+ 更新设备的alias值
+ tagsToAdd
	+ 添加的tag的集合
+ tagsToRemove
	+ 删除的tag的集合

+ DefaultResult
	+ 更新成功时isResultOK()==true

#### 异常

+ APIRequestException

	+ 包含http错误码：如401,404等，http错误信息
	+ JPush returen code和JPush returen mssage
	
+ APIConnectionException
	+ 包含错误的信息：比如超时，无网络等情况

### Method - TagListResult getTagList()

获取当前应用的所有标签列表

#### 支持版本

开支持的版本:3.0.0

#### 接口定义
	      public TagListResult getTagList()

#### 参数说明

+ DefaultResult
	+ 更新成功时isResultOK()==true

#### 异常

+ APIRequestException

	+ 包含http错误码：如401,404等，http错误信息
	+ JPush returen code和JPush returen mssage
	
+ APIConnectionException
	+ 包含错误的信息：比如超时，无网络等情况

### Method - BooleanResult isDeviceInTag(String theTag, String registrationID)

查询某个设备是否在 tag 下。

#### 支持版本

开支持的版本:3.0.0

#### 接口定义
	      public BooleanResult isDeviceInTag(String theTag, String registrationID)

#### 参数说明

+ theTag
	+ 需要确认的tag

+ registrationID
	+相对应的设备

+ BooleanResult
	+ result 成功为true 失败为false
	
#### 异常

+ APIRequestException

	+ 包含http错误码：如401,404等，http错误信息
	+ JPush returen code和JPush returen mssage
	
+ APIConnectionException
	+ 包含错误的信息：比如超时，无网络等情况


### Method -  public DefaultResult addRemoveDevicesFromTag(String theTag,HashSet<String> toAddUsers,                                            HashSet<String> toRemoveUsers)

为某个标签增加或者删除设备

#### 支持版本

开支持的版本:3.0.0

#### 接口定义
	   public DefaultResult addRemoveDevicesFromTag(String theTag,
	                                             HashSet<String> toAddUsers,
	                                             HashSet<String> toRemoveUsers)

#### 参数说明

+ theTag
	+ 要在该tag下删除或添加设备

+ toAddUsers
	+ 要添加的设备列表

+ toRemoveUsers
	+ 要删除的设备列表

+ DefaultResult
	+ 成功 isResultOK()==true,否则isResultOK()==false
	
#### 异常

+ APIRequestException

	+ 包含http错误码：如401,404等，http错误信息
	+ JPush returen code和JPush returen mssage
	
+ APIConnectionException
	+ 包含错误的信息：比如超时，无网络等情况




### Method -  public DefaultResult deleteTag(String theTag, String platform)

删除一个标签，以及标签与设备之间的关联关系

#### 支持版本

开支持的版本:3.0.0

#### 接口定义
	  public DefaultResult deleteTag(String theTag, String platform)

#### 参数说明

+ theTag
	+ 删除的标签

+ platform
	+ platform  可选参数，不填则默认为所有平台。

+ DefaultResult
	+ 成功 isResultOK()==true,否则isResultOK()==false
	
#### 异常

+ APIRequestException

	+ 包含http错误码：如401,404等，http错误信息
	+ JPush returen code和JPush returen mssage
	
+ APIConnectionException
	+ 包含错误的信息：比如超时，无网络等情况


### Method - AliasDeviceListResult getAliasDeviceList(String alias, String platform)

获取指定alias下的设备，最多输出10个

#### 支持版本

开支持的版本:3.0.0

#### 接口定义
	   public AliasDeviceListResult getAliasDeviceList(String alias, String platform)

#### 参数说明

+ alias
	+ 指定别名

+ platform
	+ platform  可选参数，不填则默认为所有平台。

+ AliasDeviceListResult
	+ 找不到统计项就是 registration_ids是null，否则为["reg_id1", "reg_id2"]
	
#### 异常

+ APIRequestException

	+ 包含http错误码：如401,404等，http错误信息
	+ JPush returen code和JPush returen mssage
	
+ APIConnectionException
	+ 包含错误的信息：比如超时，无网络等情况

### Method -  public DefaultResult deleteAlias(String alias, String platform)

删除一个别名，以及该别名与设备的绑定关系。

#### 支持版本

开支持的版本:3.0.0

#### 接口定义
	     public DefaultResult deleteAlias(String alias, String platform)
#### 参数说明

+ alias
	+ 指定别名

+ platform
	+ platform  可选参数，不填则默认为所有平台。

+ DefaultResult
	+ + 成功 isResultOK()==true,否则isResultOK()==false
	
#### 异常

+ APIRequestException

	+ 包含http错误码：如401,404等，http错误信息
	+ JPush returen code和JPush returen mssage
	
+ APIConnectionException
	+ 包含错误的信息：比如超时，无网络等情况


## HTTP 状态码

参考文档：<http://docs.jpush.cn/display/dev/HTTP-Status-Code>


Push v3 API 状态码 参考文档　<http://docs.jpush.cn/display/dev/Push-API-v3>　拉到最后


ReportAPI  状态码 参考文档　<http://docs.jpush.cn/display/dev/Report-API>　拉到最后


Device API 状态码 参考文档　<http://docs.jpush.cn/display/dev/Device-API>　拉到最后


[Release页面](https://github.com/jpush/jpush-api-csharp-client/releases/) 有详细的版本发布记录与下载。
