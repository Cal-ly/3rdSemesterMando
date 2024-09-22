# 3rd Semester Mandatory Assignment

This repository contains solutions to the mandatory assignment for the Computer Science course in Fall 2024. The assignment consists of five tasks covering C#, web development, and TCP communication protocols.

## Assignment Details

### Task 1: Class Library and Unit Test
- **Description:** Created a `Trophy` class with properties such as `Id`, `Competition`, and `Year`, with validation methods to ensure correct input. 
- **Additional Requirements:** Implemented a unit test to ensure good code coverage for all validation methods.
- **Repository Location:** [Task 1 & 2 Repository](Link to Task 1 repository)

### Task 2: Repository Class
- **Description:** Developed a `TrophiesRepository` class that manages a list of `Trophy` objects with the following methods:
  - `Get()` with filtering and sorting capabilities.
  - `GetById(int id)` to retrieve a specific trophy.
  - `Add()`, `Remove()`, and `Update()` for trophy management.
- **Unit Tests:** Added unit tests for three methods to ensure proper functionality.
- **Repository Location:** [Task 1 & 2 Repository](Link to Task 1 repository)

### Task 3: Modernized Website
- **Description:** Modernized the website [anbo-easj.dk/index2.htm](https://anbo-easj.dk/index2.htm) using HTML, CSS, and Bootstrap, ensuring responsiveness across different screen sizes.
- **Deployment:** The website has been deployed to Azure using FTP.
- **Repository Location:** [Task 3 Repository](Link to Task 3 repository)
- **Azure Deployment Link:** [Azure Website]([Link to Azure website](https://purple-bay-0d1b29203.5.azurestaticapps.net/)

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
