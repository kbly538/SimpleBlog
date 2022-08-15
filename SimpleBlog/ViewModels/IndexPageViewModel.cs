using SimpleBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlog.ViewModels
{
	public class IndexPageViewModel
	{
		public int PageNumber { get; set; }
		public int PageCount { get; set; }

		public IEnumerable<int> Pages { get; set; }
		public string Category { get; set; }
		public bool NextPage { get; set; }
		public List<Post> Posts { get; set; }
		public string SearchString { get; set; }

	}
}
