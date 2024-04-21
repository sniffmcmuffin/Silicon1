using idInfrastructure.Models;

namespace Silicon1.ViewModels;

public class CourseIndexViewModel
{
	public IEnumerable<CourseModel> Courses { get; set; } = [];
	public IEnumerable<CategoryModel>? Categories { get; set; }
}
