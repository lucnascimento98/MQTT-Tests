// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using MQTTnet;
using MQTTnet.Client.Options;

CancellationTokenSource tks = new CancellationTokenSource();

var factory = new MqttFactory();
var mqttClient = factory.CreateMqttClient();

var options = new MqttClientOptionsBuilder()
    .WithTcpServer("3.81.226.141", 1883) // Port is optional
    .WithCredentials("mqtt-test", "mqtt-test")
    .Build();

await mqttClient.ConnectAsync(options, tks.Token);

var i = 0;
while (true)
{
    var msg = i.ToString();

    var message = new MqttApplicationMessageBuilder()
    .WithTopic("sla")
    .WithPayload(msg)
    //.WithExactlyOnceQoS()
    //.WithRetainFlag()
    .Build();

    await mqttClient.PublishAsync(message, tks.Token);
    Console.WriteLine($"{i}");
    //await Task.Delay(1);
    i++;

}