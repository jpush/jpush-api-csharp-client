using Newtonsoft.Json;
using System.Collections.Generic;

namespace Jiguang.JPush.Model
{
    /// <summary>
    /// 定期任务触发器。
    /// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_push_schedule/#schedule"/>
    /// </summary>
    public class Trigger
    {
        /// <summary>
        /// 定期任务开始日期，必须为 24 小时制。
        /// 类似："2017-08-01 12:00:00"
        /// </summary>
        [JsonProperty("start")]
        public string StartDate { get; set; }

        /// <summary>
        /// 定期任务终止日期，必须为 24 小时制。
        /// 类似："2017-12-30 12:00:00"
        /// </summary>
        [JsonProperty("end")]
        public string EndDate { get; set; }

        /// <summary>
        /// 具体的触发时间。
        /// 类似："12:00:00"
        /// </summary>
        [JsonProperty("time")]
        public string TriggerTime { get; set; }

        /// <summary>
        /// 定期任务执行的最小时间单位。
        /// 必须为 "day" / "week" / "month" 中的一种。
        /// </summary>
        [JsonProperty("time_unit")]
        public string TimeUnit { get; set; }

        /// <summary>
        /// 定期任务的执行周期（目前最大支持 100）。
        /// 比如，当 TimeUnit 为 "day"，Frequency 为 2 时，表示每两天触发一次推送。
        /// </summary>
        [JsonProperty("frequency")]
        public int Frequency { get; set; }

        /// <summary>
        /// 当 TimeUnit 为 "week" 或 "month"时，具体的时间表。
        /// - 如果 TimeUnit 为 "week": {"mon", "tue", "wed", "thu", "fri", "sat", "sun"};
        /// - 如果 TimeUnit 为 "month": {"01", "02"...};
        /// </summary>
        [JsonProperty("point")]
        public List<string> TimeList { get; set; } 

        private string GetJson()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            });
        }

        public override string ToString()
        {
            return GetJson();
        }
    }
}
