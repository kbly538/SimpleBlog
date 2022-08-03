using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleBlog.Data;
using SimpleBlog.Data.FileManager;
using SimpleBlog.Data.Repository;
using SimpleBlog.Models;
using SimpleBlog.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleBlog.ControllerS
{
	public class HomeController : Controller
	{
		private IRepository _repository;
		private IFileManager _fileManager;
		public HomeController(IRepository repository, IFileManager fileManager)
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

		[HttpGet("/Image/{image}")]
		public IActionResult Image(string image)
		{
			var mime = image.Substring(image.LastIndexOf(".") + 1);
			return new FileStreamResult(_fileManager.ImageStream(image), $"image/{mime}");
		}

		
	}
}

