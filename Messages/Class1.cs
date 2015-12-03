using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ExpiresAttribute : Attribute
    {
        public ExpiresAttribute(int expiresAfterSeconds)
        {
            ExpiresAfter = TimeSpan.FromSeconds(expiresAfterSeconds);
        }

        public TimeSpan ExpiresAfter { get; private set; }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public sealed class MessageAttribute : Attribute
    {
        public MessageAttribute()
        {
        }

    }

    [Expires(60)]
    [Message]
    public class Request
    {
        public string SaySomething { get; set; }
    }

    public class RequestWithResponse : Request { }
}
