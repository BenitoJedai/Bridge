using Bridge;

namespace Bridge.Html5
{
    [Ignore]
    public enum HtmlEventTarget
    {
        [Template("{0}")]
        Raw=0,

        [Template("document.getElementById('{0}')")]
        GetElementById=1,

        [Template("document.querySelector('{0}')")]
        QuerySelector=2
    }

    [Ignore]
    public abstract class EventAttribute : Bridge.AdapterAttribute
    {
        public const string Format = "Bridge.on('{0}', {1}, this.{2});";
    }

    [Ignore]
    public class HtmlEventAttribute : EventAttribute
    {
        public const bool IsCommonEvent = true;

        public HtmlEventAttribute(string eventName, string selector, HtmlEventTarget target = HtmlEventTarget.QuerySelector)
        {
        }

        public HtmlEventAttribute(EventType eventName, string selector, HtmlEventTarget target = HtmlEventTarget.QuerySelector)
        {
        }
    }  

    [Ignore]
    public class ReadyAttribute : EventAttribute
    {
        new public const string Format = "Bridge.ready(this.{2});";
        public const string Event = "ready";

        public ReadyAttribute()
        {
        }
    }

    [Ignore]
    public class ClickAttribute : EventAttribute
    {
        public const string Event = "click";

        public ClickAttribute(string selector, HtmlEventTarget target = HtmlEventTarget.QuerySelector)
        {
        }
    } 
}