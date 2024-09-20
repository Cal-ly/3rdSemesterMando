using System.Net;
using System.Net.Sockets;
using System.Text.Json;

namespace TCPLibrary;

/// <summary>
/// Represents a TCP server that handles JSON requests and responses.
/// </summary>
public class TcpServerClassJson(string serverHost = "127.0.0.1", int serverPort = 13001)
{
    /// <summary>
    /// Starts the TCP server and listens for incoming client connections.
    /// </summary>
    public async Task StartAsync()
    {
        var listener = new TcpListener(IPAddress.Parse(serverHost), serverPort);
        listener.Start();
        Console.WriteLine($"JSON Server started on port {serverPort}.");

        while (true)
        {
            var client = await listener.AcceptTcpClientAsync();
            Console.WriteLine("Client connected.");
            _ = HandleClientAsync(client);
        }
    }

    /// <summary>
    /// Handles communication with a connected client.
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
                var requestJson = await reader.ReadLineAsync();
                Console.WriteLine($"Received JSON: {requestJson}");

                var response = ProcessRequest(requestJson);
                var responseJson = JsonSerializer.Serialize(response);

                await writer.WriteAsync(responseJson);
                Console.WriteLine($"Sent response: {responseJson}");
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
    /// Processes the JSON request and returns a JSON response.
    /// </summary>
    /// <param name="requestJson">The JSON request string.</param>
    /// <returns>A <see cref="JsonResponse"/> object containing the response.</returns>
    private static JsonResponse ProcessRequest(string? requestJson)
    {
        if (string.IsNullOrEmpty(requestJson))
        {
            return new JsonResponse { Status = "error", Message = "Invalid request." };
        }

        try
        {
            var request = JsonSerializer.Deserialize<JsonRequest>(requestJson);

            if (request == null || !NumberOps.TryParseCommandAndNumbers($"{request.Method} {request.Number1} {request.Number2}", out var command, out var number1, out var number2))
            {
                if (request == null || !NumberOps.IsValidCommand(request.Method))
                {
                    return new JsonResponse { Status = "error", Message = "Invalid command." };
                }
                if (request.Number1.GetType() != typeof(int) || request.Number2.GetType() != typeof(int))
                {
                    return new JsonResponse { Status = "error", Message = "Invalid number." };
                }
            }

            var resultJson = NumberOps.PerformOperationJson(request.Method, request.Number1, request.Number2);
            var result = JsonSerializer.Deserialize<JsonResponse>(resultJson);
            return result ?? new JsonResponse { Status = "error", Message = "Error processing request." };
        }
        catch (Exception ex)
        {
            return new JsonResponse { Status = "error", Message = $"Error processing request: {ex.Message}" };
        }
    }
}
