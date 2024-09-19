namespace TrophyTests;

/// <summary>
/// Test class for Trophy.
/// </summary>
[TestCategory("Trophy")]
[TestClass]
public class TrophyTest
{
    /// <summary>
    /// Tests ValidateCompetition method with null competition, expecting ArgumentNullException.
    /// </summary>
    [TestMethod("Null Competition, Throws ArgumentNullException")]
    public void ValidateCompetition_NullCompetition_ThrowsArgumentNullException()
    {
        // Arrange
        Trophy trophy = new Trophy();
        trophy.Competition = null;

        // Act & Assert
        Assert.ThrowsException<ArgumentNullException>(() => trophy.ValidateCompetition());
    }

    /// <summary>
    /// Tests ValidateCompetition method with short competition, expecting ArgumentException.
    /// </summary>
    [TestMethod("Short Competition, Throws ArgumentException")]
    public void ValidateCompetition_ShortCompetition_ThrowsArgumentException()
    {
        // Arrange
        Trophy trophy = new Trophy();
        trophy.Competition = "AB";

        // Act & Assert
        Assert.ThrowsException<ArgumentException>(() => trophy.ValidateCompetition());
    }

    /// <summary>
    /// Tests ValidateYear method with an invalid year, expecting ArgumentException.
    /// </summary>
    [TestMethod("Invalid Year, Throws ArgumentException")]
    public void ValidateYear_InvalidYear_ThrowsArgumentException()
    {
        // Arrange
        Trophy trophy = new Trophy();
        trophy.Year = 1969;

        // Act & Assert
        Assert.ThrowsException<ArgumentException>(() => trophy.ValidateYear());
    }

    /// <summary>
    /// Tests ValidateYear method with a future year, expecting ArgumentException.
    /// </summary>
    [TestMethod("Future Year, Throws ArgumentException")]
    public void ValidateYear_FutureYear_ThrowsArgumentException()
    {
        // Arrange
        Trophy trophy = new Trophy();
        trophy.Year = DateTime.Now.Year + 1;

        // Act & Assert
        Assert.ThrowsException<ArgumentException>(() => trophy.ValidateYear());
    }

    /// <summary>
    /// Tests Validate method with a valid trophy, expecting no exception.
    /// </summary>
    [TestMethod("Valid Trophy, Does Not Throw Exception")]
    public void Validate_ValidTrophy_DoesNotThrowException()
    {
        // Arrange
        Trophy trophy = new Trophy();
        trophy.Competition = "Championship";
        trophy.Year = 2022;

        // Act & Assert
        try
        {
            trophy.Validate();
        }
        catch (Exception)
        {
            Assert.Fail("Expected no exception, but got one.");
        }
    }

    /// <summary>
    /// Tests ToString method, expecting the expected string representation.
    /// </summary>
    [TestMethod("ToString, Returns Expected String")]
    public void ToString_ReturnsExpectedString()
    {
        // Arrange
        Trophy trophy = new Trophy();
        trophy.Id = 1;
        trophy.Competition = "Championship";
        trophy.Year = 2022;

        // Act
        string result = trophy.ToString();

        // Assert
        Assert.AreEqual("Trophy: Id=1, Competition=Championship, Year=2022", result);
    }

    /// <summary>
    /// Tests Equals method with the same trophy, expecting true.
    /// </summary>
    [TestMethod("Same Trophy, Returns True")]
    public void Equals_SameTrophy_ReturnsTrue()
    {
        // Arrange
        Trophy trophy1 = new Trophy();
        trophy1.Id = 1;
        trophy1.Competition = "Championship";
        trophy1.Year = 2022;

        Trophy trophy2 = new Trophy();
        trophy2.Id = 1;
        trophy2.Competition = "Championship";
        trophy2.Year = 2022;

        // Act
        bool result = trophy1.Equals(trophy2);

        // Assert
        Assert.IsTrue(result);
    }

    /// <summary>
    /// Tests Equals method with a different trophy, expecting false.
    /// </summary>
    [TestMethod("Different Trophy, Returns False")]
    public void Equals_DifferentTrophy_ReturnsFalse()
    {
        // Arrange
        Trophy trophy1 = new Trophy();
        trophy1.Id = 1;
        trophy1.Competition = "Championship";
        trophy1.Year = 2022;

        Trophy trophy2 = new Trophy();
        trophy2.Id = 2;
        trophy2.Competition = "Cup";
        trophy2.Year = 2021;

        // Act
        bool result = trophy1.Equals(trophy2);

        // Assert
        Assert.IsFalse(result);
    }

    /// <summary>
    /// Tests GetHashCode method, expecting the expected hash code.
    /// </summary>
    [TestMethod("GetHashCode, Returns Expected HashCode")]
    public void GetHashCode_ReturnsExpectedHashCode()
    {
        // Arrange
        Trophy trophy = new Trophy();
        trophy.Id = 1;
        trophy.Competition = "Championship";
        trophy.Year = 2022;

        // Act
        int hashCode = trophy.GetHashCode();

        // Assert
        Assert.AreEqual(HashCode.Combine(1, "Championship", 2022), hashCode);
    }
}
