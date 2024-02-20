using Silicon1.Models.Components;

namespace Silicon1.Models.Sections;

public class ShowCaseViewModel
{
	public string? Id { get; set; }
	public ImageViewModel ShowCaseImage { get; set; } = null!;
	public string? Title { get; set; }
	public string? Text { get; set; }
	public string? BrandsText { get; set; }
	public LinkViewModel Link { get; set; } = new LinkViewModel();
	public List<ImageViewModel>? Brands { get; set; }
}
