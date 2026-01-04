using MQTTnet;
using MQTTnet.Server;
using System;
using System.Threading.Tasks;

namespace MQTT_SimpleServer
{
    class Program
    {


        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello MQTT World!");


            var mqttFactory = new MqttFactory();

            // The port for the default endpoint is 1883.
            // The default endpoint is NOT encrypted!
            // Use the builder classes where possible.
            var mqttServerOptions = new MqttServerOptionsBuilder()
                .WithDefaultEndpoint()
                .Build();



            using (var mqttServer = mqttFactory.CreateMqttServer(mqttServerOptions))
            {
                mqttServer.InterceptingPublishAsync += MqttServer_InterceptingPublishAsync;
                mqttServer.ClientConnectedAsync += MqttServer_ClientConnectedAsync;
                mqttServer.ClientDisconnectedAsync += MqttServer_ClientDisconnectedAsync;
                await mqttServer.StartAsync();

                Console.WriteLine("Press Enter to exit.");
                Console.ReadLine();

                // Stop and dispose the MQTT server if it is no longer needed!
                await mqttServer.StopAsync();
            }
        }

        private static Task MqttServer_ClientDisconnectedAsync(ClientDisconnectedEventArgs arg)
        {
            Console.WriteLine("Client Disconnected: " + arg.ClientId);
            return Task.CompletedTask;
        }

        private static Task MqttServer_ClientConnectedAsync(ClientConnectedEventArgs arg)
        {
            Console.WriteLine("Client Connected: " + arg.ClientId);
            return Task.CompletedTask;
        }

        private static Task MqttServer_InterceptingPublishAsync(InterceptingPublishEventArgs arg)
        {
            Console.WriteLine("Intercept Message: " + arg.ClientId);
            return Task.CompletedTask;
        }
    }
}
