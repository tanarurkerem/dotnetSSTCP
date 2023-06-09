using SuperSimpleTcp;
using System.Text;

class Program {
    static SimpleTcpServer server;

    static void Main(string[] args)
    {
        // instantiate
        server = new SimpleTcpServer("0.0.0.0:9000");

        // set events
        server.Events.ClientConnected += ClientConnected;
        server.Events.ClientDisconnected += ClientDisconnected;
        server.Events.DataReceived += DataReceived;

        // let's go!
        server.Start();

        while (true) {}
    }

    static void ClientConnected(object? sender, ConnectionEventArgs e)
    {
        Console.WriteLine($"[{e.IpPort}] client connected");
        server.Send($"{e.IpPort}", "Hello, Client!");
    }

    static void ClientDisconnected(object? sender, ConnectionEventArgs e)
    {
        Console.WriteLine($"[{e.IpPort}] client disconnected: {e.Reason}");
    }

    static void DataReceived(object? sender, DataReceivedEventArgs e)
    {
        Console.WriteLine($"Data Received on Server - [{e.IpPort}]: {Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count)}");
    }
}
