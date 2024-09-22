## Trophy Management System (Tasks 1 & 2)

This solution consists of two projects: `TrophyLibrary` and `TrophyTests`. The `TrophyLibrary` project handles the core functionality of managing trophies, while the `TrophyTests` project ensures the reliability of the system through unit testing.

### TrophyLibrary

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

### TrophyTests

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
