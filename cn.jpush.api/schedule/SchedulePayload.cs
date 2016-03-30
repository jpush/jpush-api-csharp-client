using cn.jpush.api.push.mode;
using cn.jpush.api.util;
using Newtonsoft.Json;
using System;
using System.Diagnostics;

namespace cn.jpush.api.schedule
{

    public class SchedulePayload
    {
        private JsonSerializerSettings jSetting;
        private const String NAME = "name";
        private const String ENABLED = "enabled";
        private const String TRIGGER = "trigger";
        private const String PUSH = "push";

        public PushPayload push { get; set; }
        public String name { get; set; }
        public bool enabled { get; set; }
        public TriggerPayload trigger { get; set; }
        public String schedule_id;

        public SchedulePayload()
        {
            this.name = null;
            this.enabled = true;
            this.trigger = new TriggerPayload();
            this.push = new PushPayload();
            schedule_id = null;
            jSetting = new JsonSerializerSettings();
            jSetting.NullValueHandling = NullValueHandling.Ignore;
            jSetting.DefaultValueHandling = DefaultValueHandling.Ignore;
        }

        public SchedulePayload(String name, Boolean enabled, TriggerPayload trigger, PushPayload push)
        {
            schedule_id = null;
            Debug.Assert(name != null);
            Debug.Assert(enabled);
            Debug.Assert(trigger != null);
            Debug.Assert(push != null);
            this.name = name;
            this.enabled = enabled;
            this.trigger = trigger;
            this.push = push;
            jSetting = new JsonSerializerSettings();
            jSetting.NullValueHandling = NullValueHandling.Ignore;
            jSetting.DefaultValueHandling = DefaultValueHandling.Ignore;
        }

        public SchedulePayload(Name name, Enabled enabled, TriggerPayload trigger, PushPayload push)
        {
            schedule_id = null;
            Debug.Assert(name != null);
            Debug.Assert(enabled.getEnable());
            Debug.Assert(trigger != null);
            Debug.Assert(push != null);
            this.name = name.getName();
            this.enabled = true;
            this.trigger = trigger;
            this.push = push;
            jSetting = new JsonSerializerSettings();
            jSetting.NullValueHandling = NullValueHandling.Ignore;
            jSetting.DefaultValueHandling = DefaultValueHandling.Ignore;

        }

        public SchedulePayload setName(String name) {
            Preconditions.checkArgument(StringUtil.IsValidName(name), "The name must be the right format.");
            this.name = name;
            return this;
        }

        public SchedulePayload setEnabled(Boolean enabled)
        {
            this.enabled = enabled;
            return this;
        }

        public SchedulePayload setTrigger(TriggerPayload trigger)
        {
            this.trigger = trigger;
            return this;
        }

        public SchedulePayload setPushPayload(PushPayload push)
        {
            this.push = push;
            return this;
        }

        public string ToJson()
        {
            jSetting = new JsonSerializerSettings();
            jSetting.NullValueHandling = NullValueHandling.Ignore;
            jSetting.DefaultValueHandling = DefaultValueHandling.Ignore;       
            return JsonConvert.SerializeObject(this, jSetting);
        }
        public SchedulePayload Check()
        {
            Preconditions.checkArgument(!(null == push), "pushpayload should be set.");
            Preconditions.checkArgument(!(null == name), "name should be set.");
            Preconditions.checkArgument(enabled, "enabled should be true.");
            Preconditions.checkArgument(!(null == trigger), "trigger should be set.");
            Preconditions.checkArgument(StringUtil.IsValidName(name), "The name must be the right format.");
            Preconditions.checkArgument((name.Length < 255), "The name must be less than 255 bytes.");
            return this;
        }

    }
}
