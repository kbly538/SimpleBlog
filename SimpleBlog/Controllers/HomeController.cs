using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleBlog.Data;
using SimpleBlog.Data.Repository;
using SimpleBlog.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleBlog.ControllerS
{
	public class HomeController : Controller
	{
		private IRepository _repository;
		public HomeController(IRepository repository)
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

		
	}
}

