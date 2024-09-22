# 3rd Semester Mandatory Assignment

This repository contains solutions to the first mandatory assignment, 3rd semester, AP Computer Science, Fall 2024. The assignment consists of five tasks covering C#, web development, and TCP communication protocols. Assignment outline can be found at [Obligatorisk Opgave](https://docs.google.com/document/d/1kBH8y0o0zqKAJJkMvj3e_kfeYc8ka4oDeXYCR4s4tGI/pub). 

## Assignment Details

### Tasks 1 & 2: Trophy Management Repository

This solution consists of two projects: `TrophyLibrary` and `TrophyTests`. The `TrophyLibrary` project handles the core functionality of managing trophies, while the `TrophyTests` project ensures the reliability of the system through unit testing.

#### TrophyLibrary

The `TrophyLibrary` project contains two main classes:

- **Trophy**: Represents a trophy with properties `Id`, `Competition`, and `Year`. The class includes:
  - Validation methods (`ValidateCompetition`, `ValidateYear`, and `Validate`) to ensure valid input for both the `Competition` and `Year` properties.
  - Overridden methods: `ToString`, `Equals`, and `GetHashCode` to handle string representation, object comparison, and hashing.

- **TrophyRepository**: Manages a collection of `Trophy` objects. This class provides methods for:
  - **Main methods**
    - **Get()**: Retrieve all trophies, with optional filtering by year and sorting by `Competition` or `Year`.
    - **GetById()**: Retrieve a trophy by its ID.
    - **Add()**: Add a new trophy to the collection.
    - **Remove()**: Remove a trophy from the collection by its ID.
    - **Update()**: Update an existing trophy.
  - **Support methods**:
    - **GetNextId()**: Gets the next avaliable ID for a new thropy
    - **GetAll()**: Gets all the thropies in the repo
    - **FilterBy()**: Filters and sorts list of thropies

**Note:** While support methods by convention should be private, they are public in order to test them. Usually such methods are tested as part of another `public` unit test, but for education purposes, they are tested separatly. 

Example usage of the `TrophyRepository` class:

#### TrophyTests

The `TrophyTests` project contains unit tests to ensure the `TrophyLibrary` functions as expected. It uses the MSTest framework and is divided into two categories:

- **TrophyTest**: Tests validation, equality, string representation, and hash code generation for the `Trophy` class.
- **TrophyRepositoryTest**: Tests the methods of the `TrophyRepository` class, including adding, updating, removing, and retrieving trophies.

Example test from the `TrophyRepositoryTest` class:

```csharp
[TestMethod("No filter, Returns all Trophies")]
public void Get_WithNoFilter_ReturnsAllTrophies()
{
    var result = trophyRepository.Get();
    Assert.AreEqual(5, result.Count); // Ensures the repository returns all predefined trophies
}
```
While I personally like the **Arrange, Act, Assert** partioning of a unit test, including commenting those sections (see code snippet below), I have opted not to, for brevity.

```csharp
/// <summary>
/// Tests Update method with a valid ID, expecting the updated trophy.
/// </summary>
[TestMethod("Valid ID, Returns Updated Trophy")]
public void Update_WithValidId_ReturnsUpdatedTrophy()
{
    // Arrange
    var updatedTrophy = new Trophy { Competition = "Champions League", Year = 2022 };

    // Act
    var result = trophyRepository.Update(1, updatedTrophy);

    // Assert
    Assert.IsNotNull(result);
    Assert.AreEqual("Champions League", result.Competition);
    Assert.AreEqual(2022, result.Year);
}
```

### Task 3: Modernizing the Website

The solution for Task 3 is located in the [Part3](https://github.com/Cal-ly/3rdSemesterMando/tree/main/Part3) folder, which contains an updated version of a website with the following features:

- **Responsive Design**: The website has been modernized using Bootstrap (imported via CDN) to ensure the layout adapts to different screen sizes, including mobile devices.
- **Multiple Styles**: The project includes external and internal (inline) CSS as required by the assignment.
- **Dark Theme Toggle**: A JavaScript feature allows users to toggle between light and dark themes.
- **Quote of the Day**: JavaScript dynamically loads a "Quote of the Day," which can be refreshed with a button click.

#### Hosting and Continuous Integration:
I have used the Azure portal to create a Azure Static Web App. The website is hosted on Azure and automatically redeploys using GitHub Actions whenever changes are pushed to the main branch. This CI setup ensures the live site is always up to date with the latest changes from the repository.

Azure Website Link: [Azure Website](https://purple-bay-0d1b29203.5.azurestaticapps.net/)

#### Screenshot from website
![screenshot-website](https://github.com/Cal-ly/3rdSemesterMando/blob/main/Content/Screenshot-website.png)

#### Project Files:

- **index.html**: The main HTML file that includes:
  - Bootstrap integration using a CDN for responsive layout.
  - External stylesheet and inline styles to meet the assignment requirements.
  - Links to various sections, including external links and internal educational content.
  - A button for toggling dark/light theme using JavaScript.
  - A section for "Quote of the Day," where JavaScript injects a random motivational quote.
  
- **style.css**: The external stylesheet containing styles for both the default and dark themes, as well as custom styles for specific elements, like:
  - `.dark-theme`: A class to toggle dark mode.
  - `.italic`: Ensures the quote is displayed in italic.
  - `.link-cadetblue`: Adds custom link styles for specific links.

- **script.js**: The JavaScript file that adds interactivity to the page, including:
  - A `Quote of the Day` feature, which pulls a random quote from a predefined list and displays it on the page.
  - A `Dark Theme` toggle, which switches between light and dark themes.
  - Event listeners to handle user interactions, such as clicking buttons to toggle the theme or get a new quote.

Example: JavaScript code to toggle themes and update quotes.

```javascript
document.addEventListener("DOMContentLoaded", function () {
  const themeToggle = document.getElementById("themeToggle");
  const quoteElement = document.getElementById("quote");
  const newQuoteButton = document.getElementById('newQuoteButton');

  const quotes = [
    "Code is like humor. When you have to explain it, it’s bad.",
    "First, solve the problem. Then, write the code."
    // Additional quotes here...
  ];

  function toggleTheme() {
    document.body.classList.toggle("dark-theme");
  }

  function updateQuote() {
    quoteElement.textContent = quotes[Math.floor(Math.random() * quotes.length)];
  }

  themeToggle.addEventListener("click", toggleTheme);
  newQuoteButton.addEventListener('click', updateQuote);

  updateQuote(); // Set initial quote
});
```

### Task 4: TCP Server-Client System

The solution for Task 4 is contained in the [Part4](https://github.com/Cal-ly/3rdSemesterMando/tree/main/Part4) folder. It is a simple implementation of a TCP server-client system in C# that handles three commands: `Random`, `Add`, and `Subtract`. The communication follows a straightforward protocol where the server asks for partial input, processes the command, and returns the result.

#### Project Overview:

This solution consists of three projects:
1. **TCPLibrary**: Contains shared logic for both the server and client.
2. **TCPClientApp**: A client that connects to the server and communicates using the simple protocol.
3. **TCPServerApp**: A server that listens for incoming client connections and processes commands.

#### Command Overview:
The server handles three main commands:
- **Random**: Generates a random number between two provided integers.
- **Add**: Adds two provided integers.
- **Subtract**: Subtracts the second integer from the first.

#### Configuration:
The solution is configured to run as **multiple startup projects**. When you start the solution, both the **TCPServerApp** and **TCPClientApp** projects will launch simultaneously, each in its own console window:
- **Server Console**: This window listens for incoming client connections and processes commands sent by the client.
- **Client Console**: In this window, the user can enter commands (`Random`, `Add`, or `Subtract`), which are then sent to the server for processing. The server’s response is displayed in the client console.

This setup allows for real-time interaction between the client and server, demonstrating how TCP communication works through simple requests and responses.

#### Screenshot from Console
![console-screenshot](https://github.com/Cal-ly/3rdSemesterMando/blob/main/Content/TCPConsols.png)

#### Project Library Details:

- **TcpServerClass.cs**:
  - The server listens for client connections on the specified port (default: 13000).
  - It receives a command from the client, requests numbers, and then performs the requested operation using the `NumberOps` class from the library.
  - Example snippet:
    ```csharp
    var result = NumberOps.PerformOperation(command, number1, number2);
    await writer.WriteLineAsync(result);
    ```
    The server responds with the result of the operation (e.g., random number, sum, or difference).

- **TcpClientClass.cs**:
  - The client connects to the server, sends a command (e.g., `Random`, `Add`, or `Subtract`), and then provides two numbers for the server to process.
  - The client can communicate with the server three times in succession.
  - Example snippet:
    ```csharp
    await writer.WriteLineAsync(command);
    var result = await reader.ReadLineAsync();
    Console.WriteLine($"Result: {result}");
    ```

- **NumberOps.cs**:
  - This class is responsible for all the number-related logic, such as validating commands, parsing user inputs, and performing operations like `Add`, `Subtract`, and `Random`.
  
  - **Reason for Separation**: `NumberOps` is intentionally separated from the TCP logic to adhere to the **Separation of Concerns** design principle. By keeping the number operations in their own class, the TCP server and client code remain focused solely on managing connections and communication. This makes the code more modular, easier to maintain, and simpler to extend or reuse in other projects.
  
  - Example snippet:
    ```csharp
    public static string PerformOperation(string command, int number1, int number2) =>
        command switch
        {
            "Random" => GenerateRandomNumber(number1, number2).ToString(),
            "Add" => (number1 + number2).ToString(),
            "Subtract" => (number1 - number2).ToString(),
            _ => "Error"
        };
    ```
  
  - It also includes helper methods like `IsValidCommand` to validate commands and `TryParseNumbers` to ensure the user inputs two valid integers.

#### Additional Information:
- **Error Handling**: The server and client both handle invalid inputs gracefully. If the user enters an invalid command or malformed numbers, they will receive appropriate error messages without crashing the program.
- **Asynchronous Operations**: Both the server and client use asynchronous methods (`StartAsync`) to handle multiple tasks without blocking the main thread. This allows the server to continue listening for new clients while processing requests from existing clients.
  
This simple client-server system demonstrates how to build and interact with a TCP server, handling user commands through a custom protocol.

### Task 5: TCP Server-Client System with JSON Communication

Building on the solution from Task 4, Task 5 modifies the TCP server-client system to communicate using JSON objects instead of simple strings. The client sends structured requests, and the server processes these requests and returns responses, all in JSON format.

#### Project Overview:

This task builds on Task 4 but introduces JSON for communication. The client and server exchange messages in the following JSON formats:

#### JSON Request Format:
```json
{
    "Method": "Add",
    "Number1": 10,
    "Number2": 5
}
```
- **Method**: The operation to be performed (`Random`, `Add`, or `Subtract`).
- **Number1**: The first number to use in the operation.
- **Number2**: The second number to use in the operation.

#### JSON Response Format:
```json
{
    "Status": "Success",
    "Message": "Operation completed successfully",
    "Result": 15
}
```
- **Status**: Indicates whether the operation was successful (`Success`) or not.
- **Message**: Provides a description of the result or error.
- **Result**: The outcome of the operation (e.g., the result of addition, subtraction, or a random number).

#### Project Changes:

- **JsonRequest and JsonResponse Classes**:
  - The JSON communication is handled using the following two classes in the **TCPLibrary**:
    ```csharp
    public class JsonRequest
    {
        public required string Method { get; set; }
        public int Number1 { get; set; }
        public int Number2 { get; set; }
    }
    
    public class JsonResponse
    {
        public required string Status { get; set; }
        public required string Message { get; set; }
        public int Result { get; set; }
    }
    ```

- **Modifications to TcpServerClassJson**:
  - The server listens for incoming JSON requests from clients. The requests are deserialized into a `JsonRequest` object. The server processes the request and sends back a `JsonResponse` object.
  
  - Example snippet:
    ```csharp
    var requestJson = await reader.ReadLineAsync();
    var jsonRequest = JsonSerializer.Deserialize<JsonRequest>(requestJson);

    var resultJson = NumberOps.PerformOperationJson(jsonRequest.Method, jsonRequest.Number1, jsonRequest.Number2);

    var jsonResponse = JsonSerializer.Deserialize<JsonResponse>(resultJson);
    await writer.WriteLineAsync(JsonSerializer.Serialize(jsonResponse));
    ```

- **Modifications to TcpClientClassJson**:
  - The client now sends commands in the form of a `JsonRequest` and receives the result in a `JsonResponse`.
  
  - Example snippet:
    ```csharp
    var jsonRequest = new JsonRequest
    {
        Method = "Add",
        Number1 = 10,
        Number2 = 5
    };

    var requestJson = JsonSerializer.Serialize(jsonRequest);
    await writer.WriteLineAsync(requestJson);

    var responseJson = await reader.ReadLineAsync();
    var jsonResponse = JsonSerializer.Deserialize<JsonResponse>(responseJson);

    Console.WriteLine($"Status: {jsonResponse.Status}, Result: {jsonResponse.Result}");
    ```

#### NumberOps Updates:
- The `NumberOps` class now includes methods to handle JSON-specific operations:
  - **PerformOperationJson**: Executes the specified operation based on the JSON request and returns a JSON response.
  - **TryParseNumbersJson**: Validates the numbers in a JSON object to ensure they are valid integers.
  - **CreateJsonResponse**: Formats the result of the operation into a JSON object.
  
  - Example snippet:
    ```csharp
    public static string PerformOperationJson(string command, int number1, int number2)
    {
        var result = command switch
        {
            "Random" => GenerateRandomNumber(number1, number2),
            "Add" => number1 + number2,
            "Subtract" => number1 - number2,
            _ => 0
        };
        
        return CreateJsonResponse("success", "Operation completed successfully", result);
    }
    ```

#### Additional Information:
- **Error Handling**: The server responds with appropriate JSON error messages if the request is malformed or if an unsupported command is received.
- **Asynchronous Operations**: Both the server and client use asynchronous methods (`StartAsync`) to handle multiple tasks without blocking the main thread, allowing the server to process requests from multiple clients.
- **Close Connection**: After processing a request, the server gives the client the option to close the connection or continue sending requests.

This JSON-based client-server system enhances the flexibility and structure of communication, making it easier to extend and maintain.
