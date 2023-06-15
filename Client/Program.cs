using SuperSimpleTcp;
using System.Text;
using Microsoft.Extensions.Configuration;

class Program
{
    static void Main(string[] args)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("/app/Client/app.json")
            .AddEnvironmentVariables()
            .Build();

        string serverName = config["serverName"] ?? "127.0.0.1";
        string serverPort = config["serverPort"] ?? "9000";
        Console.WriteLine($"Server name: {serverName}");
        Console.WriteLine($"Server Port: {serverPort}");
        // instantiate
        SimpleTcpClient client = new SimpleTcpClient($"{serverName}:{serverPort}");

        // set events
        client.Events.Connected += Connected;
        client.Events.Disconnected += Disconnected;
        client.Events.DataReceived += DataReceived;
        client.Logger = Logger;

        // let's go!
        client.Connect();

        // once connected to the server...
        client.Send("Hello, Server!");
         while (true) {}
    }

    static void Connected(object? sender, ConnectionEventArgs e)
    {
        Console.WriteLine($"*** Server {e.IpPort} connected");
    }

    static void Disconnected(object? sender, ConnectionEventArgs e)
    {
        Console.WriteLine($"*** Server {e.IpPort} disconnected");
    }

    static void DataReceived(object? sender, DataReceivedEventArgs e)
    {
        if (e.Data.Array is not null) {
            Console.WriteLine($"Data Received from Server - [{e.IpPort}] {Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count)}");
        }
    }

    static void Logger(string msg)
    {
        Console.WriteLine($"Logger: {msg}");
    }
}