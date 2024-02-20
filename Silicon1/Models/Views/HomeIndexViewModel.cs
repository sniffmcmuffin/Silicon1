using Silicon1.Models.Sections;

namespace Silicon1.Models.Views;

public class HomeIndexViewModel
{
	public string Title { get; set; } = "";
	public ShowCaseViewModel ShowCase { get; set; } = null!;

    // Kan lägga in feature delen här också sen.
}
