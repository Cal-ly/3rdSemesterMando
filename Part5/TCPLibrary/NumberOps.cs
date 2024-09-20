namespace TCPLibrary;

public static class NumberOps
{
    /// <summary>
    /// Validates if the given command is one of the predefined commands.
    /// </summary>
    /// <param name="command">The command to validate.</param>
    /// <returns>True if the command is valid; otherwise, false.</returns>
    public static bool IsValidCommand(string command) =>
        command is "Random" or "Add" or "Subtract";

    /// <summary>
    /// Performs the specified operation on two numbers from the JSON object.
    /// </summary>
    /// <param name="command">The command specifying the operation ("Random", "Add", "Subtract").</param>
    /// <param name="input1">The first number.</param>
    /// <param name="input2">The second number.</param>
    /// <returns>The result of the operation wrapped in a JSON object.</returns>
    public static string PerformOperationJson(string command, int input1, int input2)
    {
        int number1 = (int)input1;
        int number2 = (int)input2;

        var result = command switch
        {
            "Random" => GenerateRandomNumber(number1, number2),
            "Add" => number1 + number2,
            "Subtract" => number1 - number2,
            _ => 0
        };

        return CreateJsonResponse("success", "Operation completed successfully", result);
    }

    /// <summary>
    /// Tries to validate the numbers in the JSON object and ensures they are valid integers.
    /// </summary>
    /// <param name="json">The JSON string containing the method and two numbers.</param>
    /// <param name="number1">The first number extracted from the JSON.</param>
    /// <param name="number2">The second number extracted from the JSON.</param>
    /// <returns>True if both numbers are valid; otherwise, false.</returns>
    public static bool TryParseNumbersJson(string json, out int number1, out int number2)
    {
        number1 = number2 = 0;

        try
        {
            var request = System.Text.Json.JsonSerializer.Deserialize<JsonRequest>(json);

            if (request == null || !IsValidCommand(request.Method))
            {
                return false;
            }

            number1 = request.Number1;
            number2 = request.Number2;

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Parses the command and numbers from the input string.
    /// </summary>
    /// <param name="input">The input string containing the command and two numbers.</param>
    /// <param name="command">The parsed command.</param>
    /// <param name="number1">The first parsed number.</param>
    /// <param name="number2">The second parsed number.</param>
    /// <returns>True if the input is valid; otherwise, false.</returns>
    public static bool TryParseCommandAndNumbers(string input, out string command, out int number1, out int number2)
    {
        command = string.Empty;
        number1 = number2 = 0;

        var parts = input.Split(' ');
        if (parts.Length != 3)
        {
            return false;
        }

        command = parts[0];
        if (!IsValidCommand(command))
        {
            return false;
        }

        if (!int.TryParse(parts[1], out number1) || !int.TryParse(parts[2], out number2))
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// Generates a random number between the specified range.
    /// </summary>
    /// <param name="number1">The lower bound of the range.</param>
    /// <param name="number2">The upper bound of the range.</param>
    /// <returns>A random number between number1 and number2 (inclusive).</returns>
    private static int GenerateRandomNumber(int number1, int number2)
    {
        if (number1 > number2)
        {
            (number1, number2) = (number2, number1);
        }
        return new Random().Next(number1, number2 + 1);
    }

    /// <summary>
    /// Creates a JSON response with the given status, message, and result.
    /// </summary>
    /// <param name="status">The status of the response ("success" or "error").</param>
    /// <param name="message">The message of the response.</param>
    /// <param name="result">The result of the operation (can be null for errors).</param>
    /// <returns>A JSON-formatted string with the status, message, and result.</returns>
    public static string CreateJsonResponse(string status, string message, int result = 0)
    {
        var responseObject = new JsonResponse
        {
            Status = status,
            Message = message,
            Result = result
        };

        return System.Text.Json.JsonSerializer.Serialize(responseObject);
    }

    /// <summary>
    /// Creates a JSON error response with a given message.
    /// </summary>
    /// <param name="errorMessage">The error message to include.</param>
    /// <returns>A JSON-formatted error response.</returns>
    public static string CreateErrorResponse(string errorMessage)
    {
        return CreateJsonResponse("error", errorMessage);
    }
}
