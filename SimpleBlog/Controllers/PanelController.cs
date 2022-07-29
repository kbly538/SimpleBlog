using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleBlog.Data.FileManager;
using SimpleBlog.Data.Repository;
using SimpleBlog.Models;
using SimpleBlog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlog.Controllers
{
	[Authorize(Roles = "Admin")]
	public class PanelController : Controller
	{ 
		private IRepository _repository;
		private IFileManager _fileManager;
		public PanelController(IRepository repository, IFileManager fileManager)
		{
			_repository = repository;
			_fileManager = fileManager;
		}
		public IActionResult Index()
		{

			List<Post> posts = _repository.GetAllPosts();
			return View(posts);
		}

		public IActionResult Post(int id)
		{

			Post post = _repository.GetPost(id);
			return View(post);
		}

		[HttpGet]
		public IActionResult Edit(int? id)
		{
			if (id == null)
			{
				return View(new PostViewModel());
			}

			var post = _repository.GetPost((int)id);
			return View(new PostViewModel
			{
				Id = post.Id,
				Title = post.Title,
				Content = post.Content,
			}); 
		}

		[HttpPost]
		public async Task<IActionResult> Edit(PostViewModel postVM)
		{

			var post = new Post
			{

				Id = postVM.Id,
				Content = postVM.Content,
				Title = postVM.Title,
				Image = await _fileManager.SaveImage(postVM.Image)
			};

			if (post.Id > 0)
			{
				_repository.UpdatePost(post);
			}
			else
			{
				_repository.AddPost(post);
			}
			if (await _repository.SaveChangesAsync())
			{
				return RedirectToAction("Index");
			}


			return View(post);
		}

		[HttpGet]
		public async Task<IActionResult> Remove(int id)
		{
			_repository.RemovePost(id);
			await _repository.SaveChangesAsync();
			return RedirectToAction("Index");
		}
	}
}
