﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    using NServiceBus.Logging;
    using Messages;
    using NServiceBus;

    class MessageSender : IWantToRunWhenBusStartsAndStops
    {
        public IBus Bus { get; set; }

        public void Start()
        {
            Bus.Send<RequestWithResponse>(m => m.SaySomething = "Hello World!")
            .Register<int>(response =>
            {
                Console.WriteLine("Response received:" + response);
            });

            LogManager.GetLogger("MessageSender").Info("Sent message.");
        }

        public void Stop()
        {
        }
    }

}
