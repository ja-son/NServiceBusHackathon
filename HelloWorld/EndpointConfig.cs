
namespace HelloWorld
{
    using Messages;
    using NServiceBus;
    using NServiceBus.Log4Net;
    using NServiceBus.Logging;
    using NServiceBus.Persistence;
    using System;
    using System.Linq;

    /*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/the-nservicebus-host
	*/
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Client, IWantToRunWhenBusStartsAndStops
    {
        public void Customize(BusConfiguration configuration)
        {
            configuration.UseSerialization<XmlSerializer>()
                .Namespace("http://acme.com/");

            log4net.Config.XmlConfigurator.Configure();
            LogManager.Use<Log4NetFactory>();

            configuration.UsePersistence<RavenDBPersistence>();
            // Or InMemoryPersistence if RavenDB is not installed
            //configuration.Conventions();
            //    .DefiningMessagesAs(t => t.Assembly == typeof(Request).Assembly && t.Name.EndsWith("Message"))
            //    .DefiningTimeToBeReceivedAs(GetExpiration);

        }

        public void Start()
        {
            LogManager.GetLogger("EndpointConfig").Info("Hello Distributed World!");
        }

        public void Stop()
        {

        }

        private static TimeSpan GetExpiration(Type type)
        {
            dynamic expiresAttribute = type.GetCustomAttributes(true)
                        .SingleOrDefault(t => t.GetType()
                        .Name == "ExpiresAttribute");
            return expiresAttribute == null
                       ? TimeSpan.MaxValue
                       : expiresAttribute.ExpiresAfter;
        }
    }
}
