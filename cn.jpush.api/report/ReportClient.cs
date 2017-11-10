using cn.jpush.api.common;
using cn.jpush.api.util;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace cn.jpush.api.report
{
    class ReportClient : BaseHttpClient
    {
        private const string REPORT_HOST_NAME = "https://report.jpush.cn";
        private const string REPORT_RECEIVE_PATH = "/v2/received";
        private const string REPORT_RECEIVE_PATH_V3 = "/v3/received";
        private const string REPORT_MESSAGE_PATH_V3 = "/v3/messages";
        private const string REPORT_USER_PATH = "/v3/users";

        private string appKey;
        private string masterSecret;

        public ReportClient(string appKey, string masterSecret)
        {
            this.appKey = appKey;
            this.masterSecret = masterSecret;
        }

        public ReceivedResult getReceiveds(string msg_ids)
        {
            checkMsgids(msg_ids);
            return getReceiveds_common(msg_ids, REPORT_RECEIVE_PATH);
        }

        public ReceivedResult getReceiveds_v3(string msg_ids)
        {
            checkMsgids(msg_ids);
            return getReceiveds_common(msg_ids, REPORT_RECEIVE_PATH_V3);
        }

        /// <summary>
        /// 查询消息送达状态。
        /// </summary>
        /// <param name="msgId">消息 Id。</param>
        /// <param name="registrationIdList">待查询设备的 Registration Id，一次调用最多支持 1000 个。</param>
        /// <param name="data">查询的日期，格式为 yyyy-MM-dd。如果传 null，默认为当天。</param>
        public ResponseWrapper getMessageSendStatus(string msgId, List<string> registrationIdList, string data)
        {
            checkMsgids(msgId);

            string url = REPORT_HOST_NAME + "/v3/status/message";

            JObject body = new JObject
            {
                { "msg_id", long.Parse(msgId) },
                { "registration_ids", JArray.FromObject(registrationIdList) }
            };

            if (!string.IsNullOrEmpty(data))
                body.Add("data", data);

            string auth = Base64.getBase64Encode(appKey + ":" + masterSecret);
            return sendPost(url, auth, body.ToString());
        }

        public UsersResult getUsers(TimeUnit timeUnit, string start, int duration)
        {
            string url = REPORT_HOST_NAME + REPORT_USER_PATH
                    + "?time_unit=" + timeUnit.ToString()
                    + "&start=" + start + "&duration=" + duration;
            string auth = Base64.getBase64Encode(appKey + ":" + masterSecret);
            ResponseWrapper response = sendGet(url, auth, null);
            return UsersResult.fromResponse(response);
        }

        public MessagesResult getReportMessages(params string[] msgIds)
        {
            return getReportMessages(StringUtil.arrayToString(msgIds));
        }

        public string checkMsgids(string msgIds)
        {
            if (string.IsNullOrEmpty(msgIds))
            {
                throw new ArgumentException("msgIds param is required.");
            }

            Regex reg = new Regex(@"[^0-9, ]");
            if (reg.IsMatch(msgIds))
            {
                throw new ArgumentException("msgIds param format is incorrect. "
                      + "It should be msg_id (number) which response from JPush Push API. "
                      + "If there are many, use ',' as interval. ");
            }

            msgIds = msgIds.Trim();
            if (msgIds.EndsWith(","))
            {
                msgIds = msgIds.Substring(0, msgIds.Length - 1);
            }
            string[] splits = msgIds.Split(',');
            List<string> list = new List<string>();
            try
            {
                foreach (string s in splits)
                {
                    string trim = s.Trim();
                    if (!string.IsNullOrEmpty(trim))
                    {
                        long.Parse(trim);
                        list.Add(trim);
                    }
                }
                return StringUtil.arrayToString(list.ToArray());
            }
            catch (Exception)
            {
                throw new Exception("Every msg_id should be valid Integer number which splits by ','");
            }
        }

        private ReceivedResult getReceiveds_common(string msg_ids, string path)
        {
            string url = REPORT_HOST_NAME + path + "?msg_ids=" + msg_ids;
            string auth = Base64.getBase64Encode(appKey + ":" + masterSecret);
            ResponseWrapper rsp = sendGet(url, auth, null);
            ReceivedResult result = new ReceivedResult();
            List<ReceivedResult.Received> list = new List<ReceivedResult.Received>();

            if (rsp.responseCode == System.Net.HttpStatusCode.OK)
            {
                list = (List<ReceivedResult.Received>)JsonTool.JsonToObject(rsp.responseContent, list);
                string content = rsp.responseContent;
            }
            result.ResponseResult = rsp;
            result.ReceivedList = list;
            return result;
        }

        private MessagesResult getReportMessages(string msgIds)
        {
            string checkMsgId = checkMsgids(msgIds);
            string url = REPORT_HOST_NAME + REPORT_MESSAGE_PATH_V3 + "?msg_ids=" + checkMsgId;
            string auth = Base64.getBase64Encode(appKey + ":" + masterSecret);
            ResponseWrapper rsp = sendGet(url, auth, null);
            MessagesResult result = new MessagesResult();
            List<MessagesResult.Message> list = new List<MessagesResult.Message>();

            Console.WriteLine("recieve content==" + rsp.responseContent);

            if (rsp.responseCode == System.Net.HttpStatusCode.OK)
            {
                list = (List<MessagesResult.Message>)JsonTool.JsonToObject(rsp.responseContent, list);
                string content = rsp.responseContent;
            }
            result.ResponseResult = rsp;
            result.messages = list;
            return result;
        }
    }
}
