using SimpleBlog.Models.Comments;
using System;
using System.Collections.Generic;

namespace SimpleBlog.Models
{
	public class Post
	{

		public int Id { get; set; }
		public string Title { get; set; } = "";
		public string Content { get; set; } = "";
		public string Image { get; set; } = "";
		public string Description { get; set; } = "";
		public string Tags { get; set; } = "";
		public string Category { get; set; } = "";
		public DateTime CreatedAt { get; set; } = DateTime.Now;
		public List<MainComment> MainComments { get; set; }
	}
}
