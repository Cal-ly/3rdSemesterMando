namespace TrophyLibrary;

/// <summary>
/// Represents a trophy with an ID, competition name, and year.
/// </summary>
public class Trophy
{
    /// <summary>
    /// Gets or sets the ID of the trophy.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the competition.
    /// </summary>
    public string? Competition { get; set; }

    /// <summary>
    /// Gets or sets the year the trophy was awarded.
    /// </summary>
    public int Year { get; set; }

    /// <summary>
    /// Validates the Competition property.
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown when Competition is null.</exception>
    /// <exception cref="ArgumentException">Thrown when Competition is less than 3 characters long.</exception>
    public void ValidateCompetition()
    {
        if (Competition == null)
            throw new ArgumentNullException("Competition can't be null");
        if (Competition.Length < 3)
            throw new ArgumentException("Competition must be at least 3 characters long");
    }

    /// <summary>
    /// Validates the Year property.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when Year is not between 1970 and the current year.</exception>
    public void ValidateYear()
    {
        int currentYear = DateTime.Now.Year;
        if (Year < 1970 || Year > currentYear)
            throw new ArgumentException($"Year must be between 1970 and {currentYear}.");
    }

    /// <summary>
    /// Validates both Competition and Year properties.
    /// </summary>
    public void Validate()
    {
        ValidateCompetition();
        ValidateYear();
    }

    /// <summary>
    /// Returns a string representation of the Trophy object.
    /// </summary>
    /// <returns>A string that represents the current Trophy object.</returns>
    public override string ToString()
    {
        return $"Trophy: Id={Id}, Competition={Competition}, Year={Year}";
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current Trophy object.
    /// </summary>
    /// <param name="obj">The object to compare with the current Trophy object.</param>
    /// <returns>true if the specified object is equal to the current Trophy object; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Trophy other = (Trophy)obj;
        return Id == other.Id &&
               Competition == other.Competition &&
               Year == other.Year;
    }

    /// <summary>
    /// Serves as the default hash function.
    /// </summary>
    /// <returns>A hash code for the current Trophy object.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Competition, Year);
    }
}
