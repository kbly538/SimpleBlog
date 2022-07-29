using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleBlog.Data.Repository;
using SimpleBlog.Models;
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
		public PanelController(IRepository repository)
		{
			_repository = repository;
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
				return View(new Post());
			}

			var post = _repository.GetPost((int)id);
			return View(post);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Post post)
		{

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
