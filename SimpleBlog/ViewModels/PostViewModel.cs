using Microsoft.AspNetCore.Http;

namespace SimpleBlog.ViewModels
{
	public class PostViewModel
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public string Description { get; set; } = "";
		public string Tags { get; set; } = "";
		public string Category { get; set; } = "";
		public IFormFile Image { get; set; } = null;
		public string CurrentImage { get; set; } = "";
		
	}
}
