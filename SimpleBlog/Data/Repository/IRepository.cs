using SimpleBlog.Models;
using SimpleBlog.Models.Comments;
using SimpleBlog.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleBlog.Data.Repository
{
	public interface IRepository
	{
		Post GetPost(int id);
		List<Post> GetAllPosts();
		IndexPageViewModel GetAllPosts(string Category, int pageNumber, string searchString);
		void AddPost(Post post);
		void RemovePost(int id);
		void UpdatePost(Post post);
		void AddSubComment(SubComment comment);
		void RemoveComment(int commentId);
		MainComment GetComment(int commentId);

		Task<bool> SaveChangesAsync();
	}
}
