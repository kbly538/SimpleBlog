using SimpleBlog.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleBlog.Data.Repository
{
	public interface IRepository
	{
		Post GetPost(int id);
		List<Post> GetAllPosts();
		void AddPost(Post post);
		void RemovePost(int id);
		void UpdatePost(Post post);

		Task<bool> SaveChangesAsync();
	}
}
