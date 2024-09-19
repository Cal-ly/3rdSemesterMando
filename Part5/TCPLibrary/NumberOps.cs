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
    /// Tries to parse two integers from a given input string.
    /// </summary>
    /// <param name="input">The input string containing two space-separated numbers.</param>
    /// <param name="num1">The first parsed number.</param>
    /// <param name="num2">The second parsed number.</param>
    /// <returns>True if both numbers are successfully parsed; otherwise, false.</returns>
    public static bool TryParseNumbers(string input, out int num1, out int num2)
    {
        num1 = num2 = 0;
        string[] parts = input.Split(' ');

        if (parts.Length != 2)
            return false;

        return int.TryParse(parts[0], out num1) && int.TryParse(parts[1], out num2);
    }

    /// <summary>
    /// Performs the specified operation on two numbers.
    /// </summary>
    /// <param name="command">The command specifying the operation ("Random", "Add", "Subtract").</param>
    /// <param name="number1">The first number.</param>
    /// <param name="number2">The second number.</param>
    /// <returns>The result of the operation as a string.</returns>
    public static string PerformOperation(string command, int number1, int number2) =>
        command switch
        {
            "Random" => GenerateRandomNumber(number1, number2).ToString(),
            "Add" => (number1 + number2).ToString(),
            "Subtract" => (number1 - number2).ToString(),
            _ => "Error"
        };

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
}
