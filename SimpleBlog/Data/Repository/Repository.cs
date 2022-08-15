using Microsoft.EntityFrameworkCore;
using SimpleBlog.Models;
using SimpleBlog.Models.Comments;
using SimpleBlog.Util;
using SimpleBlog.ViewModels;
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



		public IndexPageViewModel GetAllPosts(string category, int pageNumber, string searchString)
		{
			//Func<Post, bool> InCategory = (post) => { return post.Category.ToLower().Equals(category.ToLower()); };
			//var query = _ctx.Post
			//	.Where(post => post.Category.ToLower().Equals(category.ToLower()))
			//	.ToList();
			bool isSearchResult = false;

			int pageSize = 3;
			int skipAmount = pageSize * (pageNumber - 1);

			var query = _ctx.Post.AsQueryable();


			if (!string.IsNullOrEmpty(category))
			{
				query = query.Where(post => post.Category.ToLower().Equals(category.ToLower()));
			}

			if (!string.IsNullOrEmpty(searchString))
			{
				query = query.Where(post => post.Title.Contains(searchString)
					|| post.Content.Contains(searchString)
					|| post.Description.Contains(searchString));
				skipAmount = 0;

			}

			int postCount = query.Count<Post>();
			int pageCount = (int)Math.Ceiling((double)postCount / pageSize);

			return new IndexPageViewModel
			{
				PageNumber = pageNumber,
				PageCount = pageCount,
				Pages = PaginationHelper.Paginate(pageNumber, pageCount),
				NextPage = postCount > (skipAmount + pageSize),
				Category = category,
				Posts = query.Skip(skipAmount).Take(pageSize).ToList(),
			};
			
		}



		public Post GetPost(int id)
		{
			return _ctx.Post
				.Include(p => p.MainComments)
				.ThenInclude(mainComments => mainComments.SubComments)
				.FirstOrDefault<Post>(post => post.Id == id);	
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

		public void AddSubComment(SubComment comment)
		{
			_ctx.SubComment.Add(comment);
		}

		public void RemoveComment(int commentId)
		{
			MainComment comment = GetComment(commentId);
			_ctx.MainComment.Remove(comment);
		}

		public MainComment GetComment(int commentId)
		{
			return _ctx.MainComment.FirstOrDefault<MainComment>(c => c.Id == commentId);
		
		}
	}
}
