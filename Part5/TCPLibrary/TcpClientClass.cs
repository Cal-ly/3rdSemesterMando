using System.Net.Sockets;

namespace TCPLibrary;

/// <summary>
/// Represents a TCP client that connects to a server and sends commands.
/// </summary>
/// <param name="serverAddress">The server address to connect to. Default is "127.0.0.1".</param>
/// <param name="port">The server port to connect to. Default is 13000.</param>
public class TcpClientClass(string serverAddress = "127.0.0.1", int port = 13000)
{
    /// <summary>
    /// Starts the TCP client and attempts to connect to the server multiple times.
    /// </summary>
    public async Task StartAsync()
    {
        for (var i = 0; i < 3; i++)
        {
            await ConnectAndCommunicateAsync(i + 1);
            Console.WriteLine();
        }
    }

    /// <summary>
    /// Connects to the server and communicates with it asynchronously.
    /// </summary>
    /// <param name="attempt">The current attempt number.</param>
    private async Task ConnectAndCommunicateAsync(int attempt)
    {
        using var client = new TcpClient();
        await client.ConnectAsync(serverAddress, port);
        Console.WriteLine($"Connected to server (Attempt {attempt}).");

        await using var networkStream = client.GetStream();
        using var reader = new StreamReader(networkStream);
        await using var writer = new StreamWriter(networkStream) { AutoFlush = true };
        await SendCommandAndProcessResponseAsync(writer, reader);
    }

    /// <summary>
    /// Sends a command to the server and processes the server's response asynchronously.
    /// </summary>
    /// <param name="writer">The stream writer to send data to the server.</param>
    /// <param name="reader">The stream reader to read data from the server.</param>
    private static async Task SendCommandAndProcessResponseAsync(StreamWriter writer, StreamReader reader)
    {
        var command = GetUserCommand();
        await writer.WriteLineAsync(command);

        var serverResponse = await reader.ReadLineAsync();
        if (serverResponse != "Input numbers")
        {
            Console.WriteLine($"Server response: {serverResponse ?? "No response from server"}");
            return;
        }

        var numbersInput = GetUserNumbers();
        await writer.WriteLineAsync(numbersInput);

        var result = await reader.ReadLineAsync();
        Console.WriteLine($"Result: {result ?? "No result from server"}");
    }

    /// <summary>
    /// Prompts the user to enter a valid command.
    /// </summary>
    /// <returns>The valid command entered by the user.</returns>
    private static string GetUserCommand()
    {
        while (true)
        {
            Console.Write("Enter command (Random, Add, Subtract): ");
            var command = Console.ReadLine();
            if (command != null && NumberOps.IsValidCommand(command))
            {
                return command;
            }
            Console.WriteLine("Invalid command. Please try again.");
        }
    }

    /// <summary>
    /// Prompts the user to enter two valid integers separated by space.
    /// </summary>
    /// <returns>The input string containing two valid integers.</returns>
    private static string GetUserNumbers()
    {
        while (true)
        {
            Console.Write("Enter two numbers separated by space: ");
            var input = Console.ReadLine();
            if (input != null && NumberOps.TryParseNumbers(input, out _, out _))
            {
                return input;
            }
            Console.WriteLine("Invalid input. Please enter two valid integers separated by space.");
        }
    }
}
