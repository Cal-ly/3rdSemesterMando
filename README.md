# 3rd Semester Mandatory Assignment

This repository contains solutions to the first mandatory assignment, 3rd semester, AP Computer Science, Fall 2024. The assignment consists of five tasks covering C#, web development, and TCP communication protocols. Assignment outline can be found at [Obligatorisk Opgave](https://docs.google.com/document/d/1kBH8y0o0zqKAJJkMvj3e_kfeYc8ka4oDeXYCR4s4tGI/pub). 

## Assignment Details

### Trophy Management Repository (Tasks 1 & 2)

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


### Task 4: TCP Server and Client
- **Description:** Implemented a TCP server-client system in C# that handles three commands: `Random`, `Add`, and `Subtract`. The communication follows a simple protocol where the server asks for input, processes the command, and responds with the result.
- **Repository Location:** [Task 4 Repository](Link to Task 4 repository)

### Task 5: JSON Protocol
- **Description:** Modified the TCP protocol to use JSON objects for communication. The server now accepts JSON requests and responds accordingly, handling the same commands as Task 4 but through a JSON format.
- **Repository Location:** [Task 5 Repository](Link to Task 5 repository)

## Instructions

### Prerequisites
- [.NET SDK](https://dotnet.microsoft.com/download) (for C# projects)
- [Visual Studio](https://visualstudio.microsoft.com/) or any compatible IDE
- [Azure Account](https://azure.microsoft.com/en-us/free/) for website deployment

### Running the C# Solutions
1. **Clone the Repository**  
   Run the following command:
   ```bash
   git clone https://github.com/Cal-ly/3rdSemesterMando.git
   ```
2. **Navigate to the Project Directory**  
   Open the project in Visual Studio or any compatible IDE.

3. **Build the Project**  
   Build the project to restore any necessary dependencies.

4. **Run the Unit Tests**  
   The unit tests for Tasks 1 and 2 can be run via the Test Explorer in Visual Studio.

5. **Run the TCP Client and Server**  
   For Tasks 4 and 5:
   - Start the server by running the server project.
   - Start the client by running the client project.
   - Follow the on-screen prompts for interaction.

### Deploying the Website to Azure
1. Navigate to the project directory for Task 3.
2. Use the FTP or Kudu console to deploy the website files to Azure.
3. Ensure the website is responsive by resizing the browser window to test adaptability.

## Additional Resources
- [Azure FTP Deployment Guide](https://docs.microsoft.com/en-us/azure/app-service/deploy-ftp)
- [C# TCP Client-Server Example](https://docs.microsoft.com/en-us/dotnet/framework/network-programming/asynchronous-client-socket-example)
- [JSON in .NET](https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-overview)
- [Bootstrap Grid System](https://getbootstrap.com/docs/4.0/layout/grid/)

---

Does this look good? Let me know if you'd like any changes or additional sections.
