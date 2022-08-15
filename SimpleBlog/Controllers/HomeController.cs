using Microsoft.AspNetCore.Mvc;
using SimpleBlog.Data.FileManager;
using SimpleBlog.Data.Repository;
using SimpleBlog.Models;
using SimpleBlog.Util;
using System.Collections.Generic;
using SimpleBlog.ViewModels;
using SimpleBlog.Models.Comments;
using System;
using System.Threading.Tasks;

namespace SimpleBlog.ControllerS
{
	public class HomeController : Controller
	{
		private readonly IRepository _repository;
		private readonly IFileManager _fileManager;
		public HomeController(IRepository repository, IFileManager fileManager)
		{
			_repository = repository;
			_fileManager = fileManager;
		}
		public IActionResult Index(string category, int pageNumber, string searchString)
		{
			if (pageNumber < 1)
			{
				return RedirectToAction("Index", new { pageNumber = 1, category });
			}


			IndexPageViewModel indexPageViewModel = _repository.GetAllPosts(category, pageNumber, searchString);
			return View(indexPageViewModel);

		}

		public IActionResult Post(int id) => View(_repository.GetPost(id));
		

		[HttpGet("/Image/{image}")]
		[ResponseCache(CacheProfileName = "Monthly")]
		public IActionResult Image(string image)
		
		=> new FileStreamResult(
			_fileManager.ImageStream(image), 
			$"image/{image[(image.LastIndexOf(".") + 1)..]}");
		

		public async Task<IActionResult> Comment(CommentViewModel commentViewModel)
		{
			if (!ModelState.IsValid)
			{
				return RedirectToAction("Post", new {id = commentViewModel.PostId});
			}


			var post = _repository.GetPost(commentViewModel.PostId);

			if (commentViewModel.MainCommentId == 0)
			{
				post.MainComments = post.MainComments ?? new List<MainComment>();

				post.MainComments.Add(new MainComment
				{
					Message = commentViewModel.Message,
					Created = System.DateTime.Now
				});
			} else
			{
				var comment = new SubComment
				{
					MainCommentId = commentViewModel.MainCommentId,
					Message = commentViewModel.Message,
					Created = DateTime.Now
				};

				_repository.AddSubComment(comment);
			}


			await _repository.SaveChangesAsync();


			return RedirectToAction("Post", new { id = commentViewModel.PostId });
		}

		public async  Task<IActionResult> RemoveComment(int comId, int postId)
		{

			_repository.RemoveComment(comId);

			await _repository.SaveChangesAsync();

			return RedirectToAction("Post", new {id = postId });
		}


	}
}

