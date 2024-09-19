namespace TrophyLibrary;
/// <summary>
/// Repository for managing Trophy objects.
/// </summary>
public class TrophyRepository
{
    private readonly List<Trophy> trophies = new List<Trophy>();

    /// <summary>
    /// Initializes a new instance of the <see cref="TrophyRepository"/> class with predefined trophies.
    /// </summary>
    public TrophyRepository()
    {
        // Initialize with at least 5 objects
        trophies.Add(new Trophy { Id = 1, Competition = "Champions League", Year = 2020 });
        trophies.Add(new Trophy { Id = 2, Competition = "World Cup", Year = 2018 });
        trophies.Add(new Trophy { Id = 3, Competition = "Premier League", Year = 2021 });
        trophies.Add(new Trophy { Id = 4, Competition = "FA Cup", Year = 2019 });
        trophies.Add(new Trophy { Id = 5, Competition = "Europa League", Year = 2017 });
    }

    /// <summary>
    /// Gets a list of trophies with optional filtering by year and sorting.
    /// </summary>
    /// <param name="year">Optional year to filter trophies.</param>
    /// <param name="sortBy">Optional sorting criteria ("Competition" or "Year").</param>
    /// <param name="descending">Optional flag to sort in descending order. Default is true.</param>
    /// <returns>A list of trophies.</returns>
    public List<Trophy> Get(int? year = null, string? sortBy = null, bool descending = true)
    {
        var result = new List<Trophy>(trophies);

        if (year.HasValue)
        {
            result = result.Where(t => t.Year == year.Value).ToList();
        }
        if (sortBy != null)
        {
            result = FilterBy(result, sortBy, descending);
        }

        return result;
    }

    /// <summary>
    /// Gets a trophy by its ID.
    /// </summary>
    /// <param name="id">The ID of the trophy.</param>
    /// <returns>The trophy with the specified ID, or null if not found.</returns>
    public Trophy? GetById(int id)
    {
        return trophies.Find(t => t.Id == id);
    }

    /// <summary>
    /// Adds a new trophy to the repository.
    /// </summary>
    /// <param name="trophy">The trophy to add.</param>
    /// <returns>The added trophy with its new ID.</returns>
    public Trophy Add(Trophy trophy)
    {
        trophy.Id = GetNextId();
        trophies.Add(trophy);
        return trophy;
    }

    /// <summary>
    /// Removes a trophy by its ID.
    /// </summary>
    /// <param name="id">The ID of the trophy to remove.</param>
    /// <returns>The removed trophy, or null if not found.</returns>
    public Trophy? Remove(int id)
    {
        var trophy = GetById(id);
        if (trophy != null)
        {
            trophies.Remove(trophy);
        }
        return trophy;
    }

    /// <summary>
    /// Updates an existing trophy by its ID.
    /// </summary>
    /// <param name="id">The ID of the trophy to update.</param>
    /// <param name="updatedTrophy">The updated trophy data.</param>
    /// <returns>The updated trophy, or null if not found.</returns>
    public Trophy? Update(int id, Trophy updatedTrophy)
    {
        var existingTrophy = GetById(id);
        if (existingTrophy != null)
        {
            existingTrophy.Competition = updatedTrophy.Competition;
            existingTrophy.Year = updatedTrophy.Year;
        }
        return existingTrophy;
    }

    #region Support methods
    /// <summary>
    /// Gets the next available ID for a new trophy.
    /// </summary>
    /// <returns>The next available ID.</returns>
    public int GetNextId()
    {
        return trophies.Count != 0 ? trophies.Max(t => t.Id) + 1 : 1;
    }

    /// <summary>
    /// Gets all trophies in the repository.
    /// </summary>
    /// <returns>A list of all trophies.</returns>
    public List<Trophy> GetAll()
    {
        return new List<Trophy>(trophies);
    }

    /// <summary>
    /// Filters and sorts a list of trophies.
    /// </summary>
    /// <param name="trophies">The list of trophies to filter and sort.</param>
    /// <param name="sortBy">The property to sort by ("Competition" or "Year").</param>
    /// <param name="descending">Flag to sort in descending order.</param>
    /// <returns>A filtered and sorted list of trophies.</returns>
    public static List<Trophy> FilterBy(List<Trophy> trophies, string sortBy, bool descending)
    {
        if (sortBy == "Competition")
        {
            return descending ? trophies.OrderByDescending(t => t.Competition).ToList() : trophies.OrderBy(t => t.Competition).ToList();
        }
        else if (sortBy == "Year")
        {
            return descending ? trophies.OrderByDescending(t => t.Year).ToList() : trophies.OrderBy(t => t.Year).ToList();
        }
        return trophies;
    }
    #endregion
}
