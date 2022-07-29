using Microsoft.AspNetCore.Http;

namespace SimpleBlog.ViewModels
{
	public class PostViewModel
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public IFormFile Image { get; set; } = null;
	}
}
