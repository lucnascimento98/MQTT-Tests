// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using System.Text;

CancellationTokenSource tks = new CancellationTokenSource();

var factory = new MqttFactory();
var mqttClient = factory.CreateMqttClient();

var options = new MqttClientOptionsBuilder()
    .WithTcpServer("3.81.226.141", 1883) // Port is optional
    .WithCredentials("mqtt-test", "mqtt-test")
    .Build();

await mqttClient.ConnectAsync(options, tks.Token);

await mqttClient.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic("sla").Build());

mqttClient.UseApplicationMessageReceivedHandler(async e =>
{
    Console.WriteLine("### RECEIVED APPLICATION MESSAGE ###");
    Console.WriteLine($"+ Topic = {e.ApplicationMessage.Topic}");
    Console.WriteLine($"+ Payload = {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}");
    Console.WriteLine($"+ QoS = {e.ApplicationMessage.QualityOfServiceLevel}");
    Console.WriteLine($"+ Retain = {e.ApplicationMessage.Retain}");
    Console.WriteLine();
    await Task.Delay(100);
});

await Task.Delay(Timeout.Infinite);

