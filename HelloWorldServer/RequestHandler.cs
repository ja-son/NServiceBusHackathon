using Messages;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorldServer
{
    class RequestHandler : IHandleMessages<Request>
    {
        public void Handle(Request message)
        {
            LogManager.GetLogger("RequestHandler").Info(message.SaySomething);
        }
    }

    public class RequestWithResponseHandler : IHandleMessages<RequestWithResponse>
    {
        public IBus Bus { get; set; }

        public void Handle(RequestWithResponse message)
        {
            LogManager.GetLogger("RequestHandler").Info(message.SaySomething);
            Console.WriteLine(message.SaySomething);
            Bus.Return(message.SaySomething.Length % 2);
        }
    }


}
