using System.Net;
using System.Net.Sockets;

namespace TCPLibrary;

/// <summary>
/// Represents a TCP server that listens for client connections and processes commands.
/// </summary>
/// <param name="serverHost">The server host address. Default is "127.0.0.1".</param>
/// <param name="serverPort">The server port. Default is 13000.</param>
public class TcpServerClass(string serverHost = "127.0.0.1", int serverPort = 13000)
{
    /// <summary>
    /// Starts the TCP server and listens for client connections asynchronously.
    /// </summary>
    public async Task StartAsync()
    {
        var listener = new TcpListener(IPAddress.Parse(serverHost), serverPort);
        listener.Start();
        Console.WriteLine($"Server started on port {serverPort}.");

        while (true)
        {
            var client = await listener.AcceptTcpClientAsync();
            Console.WriteLine("Client connected.");
            _ = HandleClientAsync(client);
        }
    }

    /// <summary>
    /// Handles the client connection asynchronously.
    /// </summary>
    /// <param name="client">The connected TCP client.</param>
    private async Task HandleClientAsync(TcpClient client)
    {
        try
        {
            using (client)
            await using (var networkStream = client.GetStream())
            using (var reader = new StreamReader(networkStream))
            await using (var writer = new StreamWriter(networkStream) { AutoFlush = true })
            {
                var command = await ReceiveCommandAsync(reader, writer);
                if (command == null) return;

                await writer.WriteLineAsync("Input numbers");

                var numbersInput = await reader.ReadLineAsync();
                Console.WriteLine($"Received numbers: {numbersInput}");

                if (numbersInput == null || !NumberOps.TryParseNumbers(numbersInput, out var number1, out var number2))
                {
                    await writer.WriteLineAsync("Invalid numbers");
                    return;
                }

                var result = NumberOps.PerformOperation(command, number1, number2);
                await writer.WriteLineAsync(result);
                Console.WriteLine($"Sent result: {result}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Client disconnected.");
        }
    }

    /// <summary>
    /// Receives the command from the client asynchronously.
    /// </summary>
    /// <param name="reader">The stream reader to read data from the client.</param>
    /// <param name="writer">The stream writer to send data to the client.</param>
    /// <returns>The received command if valid; otherwise, null.</returns>
    private async Task<string?> ReceiveCommandAsync(StreamReader reader, StreamWriter writer)
    {
        var command = await reader.ReadLineAsync();
        Console.WriteLine($"Received command: {command}");

        if (command == null || !NumberOps.IsValidCommand(command))
        {
            await writer.WriteLineAsync("Invalid command");
            return null;
        }

        return command;
    }
}
