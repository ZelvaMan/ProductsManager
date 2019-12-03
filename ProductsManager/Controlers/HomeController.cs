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
			return RedirectToAction(nameof(ShowProducts));
		}

		#region Products

		[HttpGet("Product")]
		public ActionResult ShowProducts()
		{
			List<Product> products;
			if ((products = manager.Products) != null)
			{
				ViewBag.data = products;
			}

			ViewBag.manager = manager;
			return View();
		}

		[HttpGet("Product/{productId}")]
		public ActionResult ShowBuyProduct(int productId)
		{
			ViewBag.Product = manager.FindProductById(productId);
			return View();
		}

		//[HttpPost("Product/buy/{productId}")]

		[HttpGet("Product/remove/{productId}")]
		public ActionResult RemoveProduct(int productId)
		{
			manager.DeleteProduct(productId);
			return RedirectToAction(nameof(ShowProducts));
		}

		[HttpGet("product/add")]
		public ActionResult ShowProductAddForm()
		{
			return View("ShowAddForm");
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="data"> data from form</param>
		/// <returns></returns>
		[HttpPost("product/add")]
		public ActionResult AddProduct(ProductFormModel data)
		{
			manager.AddProduct(data);
			return RedirectToAction(nameof(ShowProducts));
		}
		#endregion

		#region Cart

		[HttpPost("Product/{productId}")]
		public ActionResult AddToCart(int productId, int quantity)
		{
			manager.AddToCart(productId, quantity);
			return RedirectToAction(nameof(ShowProducts));
		}

		[HttpGet("cart/delete/{id}")]
		public ActionResult DeleteOrder(int id)
		{
			manager.DeleteOrder(id);
			return RedirectToAction(nameof(ShowOrderItems));
		}

		[HttpGet("cart")]
		public ActionResult ShowOrderItems()
		{
			ViewBag.Orders = manager.Cart;
			return View();
		}

		[HttpPost("cart/buyall")]
		public ActionResult Buy()
		{
			manager.Buy();
			return RedirectToAction(nameof(ShowProducts));
		}

		#endregion

		#region Order History

		[HttpGet("orders")]
		public ActionResult ShowOrderHistory([FromQuery]int startingPosition, [FromQuery] bool canceled)
		{
			bool canceledBool;
			if (!bool.TryParse(canceled.ToString(), out canceledBool))
				canceledBool = false;

			ViewBag.last = manager.LastStartingPostion(canceledBool);
			ViewBag.canceled = canceledBool;
				
			
			ViewBag.start = startingPosition;
			ViewBag.orders = manager.GetFiveOrders(canceledBool, startingPosition);
			return View();
		}


		[HttpGet("orders/info/{orderId}")]
		public ActionResult ShowOrderInfo(int orderId, [FromQuery]int startingPosition, [FromQuery] bool canceled)
		{
			
			//get last starting index
			int lastStartIndex = manager.LastStartingPostion(canceled);
			ViewBag.last = manager.LastStartingPostion(canceled);
			ViewBag.canceled = canceled;
			//if starting index is invalid(due to changed visibility of canceled orders) set starting position to last start index
			if (startingPosition > lastStartIndex)
			{
				startingPosition = lastStartIndex;
			}
			ViewBag.start = startingPosition;
			ViewBag.orders = manager.GetFiveOrders(canceled, startingPosition);
			return View();
		}


		[HttpGet("orders/info/{orderId}")]
		public ActionResult ShowOrderInfo(int orderId)
		{
			ViewBag.order = manager.FindOrderByOrderId(orderId);
			return View();
		}

		[HttpGet("orders/cancel/{orderId}")]
		public ActionResult CancelOrder(int orderId,[FromQuery]int startingPosition ,[FromQuery]bool canceled)
		{
			manager.CancelOrder(orderId);
			return RedirectToAction(nameof(ShowOrderHistory), new RouteValueDictionary
			{
				{"startingPosition", startingPosition}, {"canceled", canceled}
			});
		}
		#endregion
	}
}