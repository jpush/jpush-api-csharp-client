using cn.jpush.api.common;
using cn.jpush.api.common.resp;
using cn.jpush.api.device;
using cn.jpush.api.push;
using cn.jpush.api.push.mode;
using cn.jpush.api.report;
using cn.jpush.api.util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api
{
    /// <summary>
    /// Main Entrance - 该类为JPush服务的主要入口
    /// </summary>
    public class JPushClient
    {
        private PushClient _pushClient;
        private ReportClient _reportClient;
        private DeviceClient _deviceClient;
        /// <summary>
        /// 带两个参数的构造函数，该状态下，ApnsProduction默认为false
        /// </summary>
        /// <param name="app_key">Portal上产生的app_key</param>
        /// <param name="masterSecret">你的API MasterSecret</param>
        public JPushClient(String app_key, String masterSecret)
        {
            _pushClient = new PushClient(app_key, masterSecret);
            _reportClient = new ReportClient(app_key, masterSecret);
            _deviceClient = new DeviceClient(app_key, masterSecret);

        }
        // ----------------------------- Push API
        /// <summary>
        /// 向某个设备或者某设别列表推送一条通知，或者消息
        /// </summary>
        /// <param name="PushPayload">推送的数据结构，包含平台信息推送目标，通知内容，消息内容与可选参数</param>
        /// <returns>成功时返回sendno和messageid，失败时有异常抛出</returns>
        /// <exception cref="APIRequestException">包含http错误码：如401,404等，错误信息，JPush returen code和JPush returen mssage</exception>
        /// <exception cref="APIConnectionException">包含错误的信息</exception>
        /// <see cref="http://docs.jpush.cn/display/dev/Push-API-v3"/>
        public MessageResult SendPush(PushPayload payload)
        {
            Preconditions.checkArgument(payload!=null, "pushPayload should not be empty");
            return _pushClient.sendPush(payload);
        }
        /// <summary>
        /// 向某个设备或者某设别列表推送一条通知，或者消息
        /// </summary>
        /// <param name="PushPayload">推送的json结构，包含平台信息推送目标，通知内容，消息内容与可选参数</param>
        /// <returns>成功时返回sendno和messageid，失败时有异常抛出</returns>
        /// <exception cref="APIRequestException">包含http错误码：如401,404等，错误信息，JPush returen code和JPush returen mssage</exception>
        /// <exception cref="APIConnectionException">包含错误的信息</exception>
        /// <see cref="http://docs.jpush.cn/display/dev/Push-API-v3"/>
        public MessageResult SendPush(string payloadString)
        {
             Preconditions.checkArgument(!string.IsNullOrEmpty(payloadString), "payloadString should not be empty");
             return _pushClient.sendPush(payloadString);
        }
        // ------------------------------- Report API
        /// <summary>
        /// Get received report. 
        /// </summary>
        /// <param name="msgIds">100 msgids to batch getting is supported.</param>
        /// <returns> Can be printed to JSON.</returns>
        /// <exception cref="APIRequestException">包含http错误码：如401,404等，错误信息，JPush returen code和JPush returen mssage</exception>
        /// <exception cref="APIConnectionException">包含错误的信息</exception>
        /// <see cref="100 msgids to batch getting is supported."/> 
        public ReceivedResult getReceivedApi(String msg_ids)
        {
            return _reportClient.getReceiveds(msg_ids);
        }
        /// <summary>
       /// Get received report v3. 
       /// </summary>
       /// <param name="msgIds">100 msgids to batch getting is supported.</param>
       /// 
        public ReceivedResult getReceivedApi_v3(String msg_ids)
        {
            return _reportClient.getReceiveds_v3(msg_ids);
        }
        /// <summary>
        /// 用户统计查询接口，这个接口是vip用户专用
        /// </summary>
        /// <param name="timeUnit">时间单位，有三个取值:HOUR、DAY、MONTH</param>
        /// <param name="start">起始时间</param>
        /// <param name="duration">持续时间</param>
        /// <returns>包含ios android的用户情况</returns>
        public UsersResult getReportUsers(TimeUnit timeUnit, String start, int duration)
        {
            return _reportClient.getUsers(timeUnit, start, duration);
        }
        /// <summary>
        /// 消息统计查询接口，这个接口是vip用户专用
        /// </summary>
        /// <param name="msgIds">用逗号分隔的多个消息id</param>
        /// <returns>包含各个mssageid和ios android平台</returns>
        public MessagesResult getReportMessages(params String[] msgIds)
        {
            return _reportClient.getReportMessages(msgIds);
        }
        // ------------------------------- Device API
        /// <summary>
        /// 获取当前设备的所有属性，包含tags,alias
        /// </summary>
        /// <param name="registrationId">设备的registrationID</param>
        /// <returns>找不到的统计项是null，否则为统计项的值</returns>
        public TagAliasResult getDeviceTagAlias(String registrationId)
        {
            return _deviceClient.getDeviceTagAlias(registrationId);
        }
        /// <summary>
        /// 清理当前设备指定的属性，当前支持tags,alias
        /// </summary>
        /// <param name="clearAlias">是否清除alias</param>
        /// <param name="clearTag">是否清除tags</param>
        /// <returns>找不到的统计项是null，否则为统计项的值</returns>
        /// <exception cref="APIRequestException">包含http错误码：如401,404等，错误信息，JPush returen code和JPush returen mssage</exception>
        /// <exception cref="APIConnectionException">包含错误的信息</exception>
        /// <see cref="http://docs.jpush.cn/display/dev/Device-API"/> 
        public DefaultResult updateDeviceTagAlias(String registrationId, bool clearAlias, bool clearTag)
        {
            return _deviceClient.updateDeviceTagAlias(registrationId, clearAlias, clearTag);
        }
        /// <summary>
        /// 更新当前设备指定的属性，当前支持tags,alias
        /// </summary>
        /// <param name="alias">alias名称，传递null:不改变，传递""：清空</param>
        /// <param name="tagsToAdd">新添加的tags</param>
        /// <param name="tagsToRemove">删除的tags</param>
        /// <returns>更新成功时isResultOK==true</returns>
        /// <exception cref="APIRequestException">包含http错误码：如401,404等，错误信息，JPush returen code和JPush returen mssage</exception>
        /// <exception cref="APIConnectionException">包含错误的信息</exception>
        /// <see cref="http://docs.jpush.cn/display/dev/Device-API"/> 
        public DefaultResult updateDeviceTagAlias(String registrationId,
                                                   String alias,
                                                   String mobile,
                                                   HashSet<String> tagsToAdd,
                                                   HashSet<String> tagsToRemove)
        {
            return _deviceClient.updateDevice(registrationId, alias, mobile, tagsToAdd, tagsToRemove);
        }
        /// <summary>
        /// 获取当前应用的所有标签
        /// </summary>
        /// <returns>标签列表</returns>
        /// <exception cref="APIRequestException">包含http错误码：如401,404等，错误信息，JPush returen code和JPush returen mssage</exception>
        /// <exception cref="APIConnectionException">包含错误的信息</exception>
        /// <see cref="http://docs.jpush.cn/display/dev/Device-API"/> 
        public TagListResult getTagList()
        {
            return _deviceClient.getTagList();
        }
        /// <summary>
        /// 查询某个设备是否在tag下
        /// </summary>
        /// <param name="theTag">查询的tag </param>
        /// <param name="registrationID">需要确认的设备的registrationID</param>
        /// <returns>成功result=true,失败result=false</returns>
        /// <exception cref="APIRequestException">包含http错误码：如401,404等，错误信息，JPush returen code和JPush returen mssage</exception>
        /// <exception cref="APIConnectionException">包含错误的信息</exception>
        /// <see cref="http://docs.jpush.cn/display/dev/Device-API"/> 
        public BooleanResult isDeviceInTag(String theTag, String registrationID)
        {
            return _deviceClient.isDeviceInTag(theTag, registrationID);
        }
        /// <summary>
        /// 为一个标签添加或者删除设备
        /// </summary>
        /// <param name="theTag">操作的tag </param>
        /// <param name="toAddUsers">需要添加的registrationID的集合</param>
        /// <param name="toRemoveUsers">需要删除的registrationID的集合</param>
        /// <returns>成功isResultOK()=true,失败isResultOK()=false</returns>
        /// <exception cref="APIRequestException">包含http错误码：如401,404等，错误信息，JPush returen code和JPush returen mssage</exception>
        /// <exception cref="APIConnectionException">包含错误的信息</exception>
        /// <see cref="http://docs.jpush.cn/display/dev/Device-API"/> 
        public DefaultResult addRemoveDevicesFromTag(String theTag,
                                                     HashSet<String> toAddUsers,
                                                     HashSet<String> toRemoveUsers)
        {
            return _deviceClient.addRemoveDevicesFromTag(theTag, toAddUsers, toRemoveUsers);
        }
        /// <summary>
        /// 删除一个标签，以及标签与设备之间的关联关系
        /// </summary>
        /// <param name="theTag">要删除的tag </param>
        /// <param name="platform">可选参数，不填则默认为所有平台</param>
        /// <returns>成功result=true,失败result=false</returns>
        /// <exception cref="APIRequestException">包含http错误码：如401,404等，错误信息，JPush returen code和JPush returen mssage</exception>
        /// <exception cref="APIConnectionException">包含错误的信息</exception>
        /// <see cref="http://docs.jpush.cn/display/dev/Device-API"/> 
        public DefaultResult deleteTag(String theTag, String platform)
        {
            return _deviceClient.deleteTag(theTag, platform);

        }
        /// <summary>
        /// 查询别名
        /// </summary>
        /// <param name="alias">要查询的别名 </param>
        /// <param name="platform">可选参数，不填则默认为所有平台</param>
        /// <returns>返回alias的列表</returns>
        /// <exception cref="APIRequestException">包含http错误码：如401,404等，错误信息，JPush returen code和JPush returen mssage</exception>
        /// <exception cref="APIConnectionException">包含错误的信息</exception>
        /// <see cref="http://docs.jpush.cn/display/dev/Device-API"/> 
        public AliasDeviceListResult getAliasDeviceList(String alias, String platform)
        {
            return _deviceClient.getAliasDeviceList(alias, platform);
        }
        /// <summary>
        /// 删除别名，以及该别名与设别之间的绑定关系
        /// </summary>
        /// <param name="alias">要删除的别名 </param>
        /// <param name="platform">可选参数，不填则默认为所有平台</param>
        /// <returns>成功isResultOK()=true,失败isResultOK()=false</returns>
        /// <exception cref="APIRequestException">包含http错误码：如401,404等，错误信息，JPush returen code和JPush returen mssage</exception>
        /// <exception cref="APIConnectionException">包含错误的信息</exception>
        /// <see cref="http://docs.jpush.cn/display/dev/Device-API"/>
        public DefaultResult deleteAlias(String alias, String platform)
        {
            return _deviceClient.deleteAlias(alias, platform);
        }

    }
   
}
