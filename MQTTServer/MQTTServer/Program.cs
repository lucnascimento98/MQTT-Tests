// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
using MQTTnet;
using MQTTnet.Server;

var optionsBuilder = new MqttServerOptionsBuilder();
   // .WithConnectionBacklog(100)
    //.WithDefaultEndpointPort(1883);

var mqttServer = new MqttFactory().CreateMqttServer();

await mqttServer.StartAsync(optionsBuilder.Build());
Console.WriteLine("Press any key to exit.");
Console.ReadLine();
await mqttServer.StopAsync();
