using SuperSimpleTcp;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        string serverName = System.Environment.GetEnvironmentVariable("SERVER_NAME");
        Console.WriteLine($"{serverName}");
        // instantiate
        SimpleTcpClient client = new SimpleTcpClient($"{serverName}:9000");

        // set events
        client.Events.Connected += Connected;
        client.Events.Disconnected += Disconnected;
        client.Events.DataReceived += DataReceived;

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
    Console.WriteLine($"Data Received on Client - [{e.IpPort}] {Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count)}");
    }
}