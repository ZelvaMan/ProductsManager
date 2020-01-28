using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ProductsManager.Managers;
using ProductsManager.Models;

namespace ProductsManager.Controlers
{
	public class HomeController : Controller
	{
		private ShopManager manager;

		public HomeController(ShopManager manager)
		{
			this.manager = manager;
		}

		public ActionResult Index()
		{
			//return RedirectToAction(nameof(ShowProducts));
			//redirect to all Products
			return null;
		}


	}
}