# 3rd Semester Mandatory Assignment

This repository contains solutions to the mandatory assignment for the Computer Science course in Fall 2024. The assignment consists of five tasks covering C#, web development, and TCP communication protocols.

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

`Note` While support methods by convention should be private, they are public in order to test them. Usually such methods are tested as part of another `public` unit test, but for education purposes, they are tested separatly. 

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

### Task 3: Modernized Website
- **Description:** Modernized the website [anbo-easj.dk/index2.htm](https://anbo-easj.dk/index2.htm) using HTML, CSS, and Bootstrap, ensuring responsiveness across different screen sizes.
- **Deployment:** The website has been deployed to Azure using FTP.
- **Repository Location:** [Task 3 Repository](Link to Task 3 repository)
- **Azure Deployment Link:** [Azure Website](https://purple-bay-0d1b29203.5.azurestaticapps.net/)

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
