using System.Net;
using System.Net.Sockets;
using System.Text.Json;

namespace TCPLibrary;

/// <summary>
/// Represents a TCP server that handles JSON requests and responses.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="TcpServerClassJson"/> class.
/// </remarks>
/// <param name="serverHost">The server host address.</param>
/// <param name="serverPort">The server port number.</param>
public class TcpServerClassJson(string serverHost = "127.0.0.1", int serverPort = 13001)
{
    /// <summary>
    /// Starts the TCP server and listens for incoming client connections.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task StartAsync()
    {
        var listener = new TcpListener(IPAddress.Parse(serverHost), serverPort);
        listener.Start(5); // Start listening with a maximum of 5 pending connections.
        Console.WriteLine($"JSON Server started on port {serverPort}.");

        while (true)
        {
            var client = await listener.AcceptTcpClientAsync();
            Console.WriteLine("Client connected.");
            _ = Task.Run(() => HandleClientAsync(client));
        }
    }

    /// <summary>
    /// Handles communication with a connected client.
    /// </summary>
    /// <param name="client">The connected TCP client.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    private async Task HandleClientAsync(TcpClient client)
    {
        try
        {
            using (client)
            await using (var networkStream = client.GetStream())
            using (var reader = new StreamReader(networkStream))
            await using (var writer = new StreamWriter(networkStream) { AutoFlush = true })
            {
                while (true)
                {
                    string? requestJson;
                    try
                    {
                        requestJson = await reader.ReadLineAsync();
                    }
                    catch (IOException ioEx)
                    {
                        Console.WriteLine($"IOException: {ioEx.Message}");
                        break;
                    }

                    if (string.IsNullOrEmpty(requestJson))
                    {
                        Console.WriteLine("Received empty request. Sending error response.");
                        var errorResponse = new JsonResponse { Status = "error", Message = "Empty request received." };
                        var errorResponseJson = JsonSerializer.Serialize(errorResponse);
                        await writer.WriteLineAsync(errorResponseJson);
                        continue;
                    }

                    Console.WriteLine($"Received JSON: {requestJson}");

                    var response = ProcessRequest(requestJson);
                    var responseJson = JsonSerializer.Serialize(response);

                    await writer.WriteLineAsync(responseJson);
                    Console.WriteLine($"Sent response: {responseJson}");

                    // Prompt the client to decide whether to close the connection
                    await writer.WriteLineAsync("Do you want to close the connection? (yes/no)");
                    var closeResponse = await reader.ReadLineAsync();
                    if (closeResponse?.Trim().ToLower() == "yes" || closeResponse?.Trim().ToLower() == "y")
                    {
                        Console.WriteLine("Client chose to close the connection.");
                        break;
                    }
                }
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
