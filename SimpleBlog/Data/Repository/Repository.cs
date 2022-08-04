using SimpleBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlog.Data.Repository
{
	public class Repository : IRepository
	{

		private AppDbContext _ctx;

		public Repository(AppDbContext ctx)
		{
			_ctx = ctx;
		}

		public void AddPost(Post post)
		{
			_ctx.Post.Add(post);
			
		}

		public List<Post> GetAllPosts()
		{
			return _ctx.Post.ToList<Post>();
		}

		public Post GetPost(int id)
		{
			return _ctx.Post.FirstOrDefault<Post>(post => post.Id == id);	
		}

		public void RemovePost(int id)
		{
			_ctx.Post.Remove(GetPost(id));	
			
		}


		public void UpdatePost(Post post)
		{
			_ctx.Update(post);
		}

		public async Task<bool> SaveChangesAsync()
		{
			if(await _ctx.SaveChangesAsync() > 0)
			{
				return true;
			}

			return false;
		}

		public List<Post> GetAllPostsByCategory(string category)
		{
			//Func<Post, bool> InCategory = (post) => { return post.Category.ToLower().Equals(category.ToLower()); };
			return _ctx.Post
				.Where(post => post.Category.ToLower().Equals(category.ToLower()))
				.ToList();
		}
	}
}
