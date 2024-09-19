namespace TrophyTests;

/// <summary>
/// Test class for TrophyRepository.
/// I have named the test methods in three ways:
/// By "convention" the method name is in the format of "MethodUnderTest_Scenario_ExpectedBehavior".
/// The TestMethod attribute is used to identify the method as a test method and to provide information about the test in display.
/// Finally, comments are used to describe the test scenario and the expected behavior.
/// Highly superfluous, but for learning purposes, I deem it acceptable - and a way to show that "standards" are needed
/// </summary>
[TestCategory("TrophyRepository")]
[TestClass]
public class TrophyRepositoryTest
{
    private TrophyRepository trophyRepository = null!;

    /// <summary>
    /// Initializes the test setup.
    /// </summary>
    [TestInitialize]
    public void Setup()
    {
        trophyRepository = new TrophyRepository();
    }

    /// <summary>
    /// Tests Get method with no filter, expecting all trophies.
    /// </summary>
    [TestMethod("No filter, Returns all Trophies")]
    public void Get_WithNoFilter_ReturnsAllTrophies()
    {
        var result = trophyRepository.Get();

        Assert.AreEqual(5, result.Count);
    }

    /// <summary>
    /// Tests Get method with year filter, expecting trophies filtered by year.
    /// </summary>
    [TestMethod("Year filter, Returns Trophies filtered by Year")]
    public void Get_WithYearFilter_ReturnsTrophiesFilteredByYear()
    {
        var result = trophyRepository.Get(year: 2020);

        Assert.AreEqual(1, result.Count);
        Assert.AreEqual("Champions League", result[0].Competition);
    }

    /// <summary>
    /// Tests Get method with sort by competition, expecting trophies sorted by competition.
    /// </summary>
    [TestMethod("Sort by Competition, Returns Trophies sorted by Competition")]
    public void Get_WithSortByCompetition_ReturnsTrophiesSortedByCompetition()
    {
        var result = trophyRepository.Get(sortBy: "Competition", descending: true);

        Assert.AreEqual(5, result.Count);
        Assert.AreEqual("World Cup", result[0].Competition);
        Assert.AreEqual("Premier League", result[1].Competition);
        Assert.AreEqual("FA Cup", result[2].Competition);
        Assert.AreEqual("Europa League", result[3].Competition);
        Assert.AreEqual("Champions League", result[4].Competition);
    }

    /// <summary>
    /// Tests Get method with sort by year, expecting trophies sorted by year.
    /// </summary>
    [TestMethod("Sort by Year, Returns Trophies sorted by Year")]
    public void Get_WithSortByYear_ReturnsTrophiesSortedByYear()
    {
        var result = trophyRepository.Get(sortBy: "Year", descending: true);

        Assert.AreEqual(5, result.Count);
        Assert.AreEqual("Premier League", result[0].Competition);
        Assert.AreEqual("Champions League", result[1].Competition);
        Assert.AreEqual("FA Cup", result[2].Competition);
        Assert.AreEqual("World Cup", result[3].Competition);
        Assert.AreEqual("Europa League", result[4].Competition);
    }

    /// <summary>
    /// Tests GetById method with a valid ID, expecting the corresponding trophy.
    /// </summary>
    [TestMethod("Valid ID, Returns Trophy")]
    public void GetById_WithValidId_ReturnsTrophy()
    {
        var result = trophyRepository.GetById(1);

        Assert.IsNotNull(result);
        Assert.AreEqual("Champions League", result.Competition);
    }

    /// <summary>
    /// Tests GetById method with an invalid ID, expecting null.
    /// </summary>
    [TestMethod("GetBy Invalid ID, Returns Null")]
    public void GetById_WithInvalidId_ReturnsNull()
    {
        var result = trophyRepository.GetById(10);

        Assert.IsNull(result);
    }

    /// <summary>
    /// Tests Add method with a valid trophy, expecting the added trophy with a new ID.
    /// </summary>
    [TestMethod("Valid Trophy, Returns Added Trophy with New ID")]
    public void Add_WithValidTrophy_ReturnsAddedTrophyWithNewId()
    {
        var trophy = new Trophy { Competition = "Super Cup", Year = 2022 };

        var result = trophyRepository.Add(trophy);

        Assert.IsNotNull(result);
        Assert.AreEqual(6, result.Id);
        Assert.AreEqual("Super Cup", result.Competition);
        Assert.AreEqual(2022, result.Year);
    }

    /// <summary>
    /// Tests Remove method with a valid ID, expecting the removed trophy.
    /// </summary>
    [TestMethod("Valid ID, Returns Removed Trophy")]
    public void Remove_WithValidId_ReturnsRemovedTrophy()
    {
        var result = trophyRepository.Remove(1);

        Assert.IsNotNull(result);
        Assert.AreEqual("Champions League", result.Competition);
    }

    /// <summary>
    /// Tests Remove method with an invalid ID, expecting null.
    /// </summary>
    [TestMethod("Remove Invalid ID, Returns Null")]
    public void Remove_WithInvalidId_ReturnsNull()
    {
        var result = trophyRepository.Remove(10);

        Assert.IsNull(result);
    }

    /// <summary>
    /// Tests Update method with a valid ID, expecting the updated trophy.
    /// </summary>
    [TestMethod("Valid ID, Returns Updated Trophy")]
    public void Update_WithValidId_ReturnsUpdatedTrophy()
    {
        var updatedTrophy = new Trophy { Competition = "Champions League", Year = 2022 };

        var result = trophyRepository.Update(1, updatedTrophy);

        Assert.IsNotNull(result);
        Assert.AreEqual("Champions League", result.Competition);
        Assert.AreEqual(2022, result.Year);
    }

    /// <summary>
    /// Tests Update method with an invalid ID, expecting null.
    /// </summary>
    [TestMethod("Update Invalid ID, Returns Null")]
    public void Update_WithInvalidId_ReturnsNull()
    {
        var updatedTrophy = new Trophy { Competition = "Champions League", Year = 2022 };

        var result = trophyRepository.Update(10, updatedTrophy);

        Assert.IsNull(result);
    }
}
