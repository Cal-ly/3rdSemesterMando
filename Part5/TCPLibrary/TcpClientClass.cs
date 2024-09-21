using System.Net.Sockets;
using System.Text.Json;

namespace TCPLibrary;

/// <summary>
/// Represents a TCP client that communicates with a TCP server using JSON requests and responses.
/// </summary>
public class TcpClientClassJson(string serverAddress = "127.0.0.1", int port = 13001)
{
    /// <summary>
    /// Starts the TCP client and connects to the server.
    /// </summary>
    /// <returns>A task that represents the async operation.</returns>
    public async Task StartAsync()
    {
        //await ConnectAndCommunicateAsync(); // Uncomment this line to connect only once
        while (true)
            await ConnectAndCommunicateAsync();
    }

    /// <summary>
    /// Connects to the server and handles communication.
    /// </summary>
    /// <returns>A task that represents the async operation.</returns>
    private async Task ConnectAndCommunicateAsync()
    {
        using var client = new TcpClient();
        await client.ConnectAsync(serverAddress, port);
        Console.WriteLine("Connected to server.");

        await using var networkStream = client.GetStream();
        using var reader = new StreamReader(networkStream);
        await using var writer = new StreamWriter(networkStream) { AutoFlush = true };

        var request = GetJsonRequest();
        await writer.WriteLineAsync(request);

        var serverResponse = await reader.ReadLineAsync();
        Console.WriteLine($"Server response: {serverResponse ?? "No response from server"}");
    }

    /// <summary>
    /// Creates a JSON request based on user input.
    /// </summary>
    /// <returns>A JSON-formatted string representing the request.</returns>
    private static string GetJsonRequest()
    {
        var (command, num1, num2) = GetUserCommandAndNumbers();

        var requestObject = new JsonRequest
        {
            Method = command,
            Number1 = num1,
            Number2 = num2
        };

        return JsonSerializer.Serialize(requestObject);
    }

    /// <summary>
    /// Prompts the user to enter a command and two numbers, and parses the input.
    /// </summary>
    /// <returns>A tuple containing the command and two integers.</returns>
    private static (string, int, int) GetUserCommandAndNumbers()
    {
        while (true)
        {
            Console.WriteLine("Enter 'Add, 'Subtract' or 'Random', the first integer, the second integer, all separated by a single space: ");
            var input = Console.ReadLine();
            if (input != null && NumberOps.TryParseCommandAndNumbers(input, out var command, out var num1, out var num2))
            {
                return (command, num1, num2);
            }
            Console.WriteLine("Invalid input. Please enter a valid command and two integers separated by space.");
        }
    }
}
