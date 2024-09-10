namespace csharpwebapi.Models;

public class Tutorial{
    public int? Id { get; set; }       // Nullable integer for id
    public required string Title { get; set; }  // String for title
    public required string Description { get; set; } // String for description
    public bool? Published { get; set; } // Nullable boolean for published status
}