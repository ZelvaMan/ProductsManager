using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductsManager.Managers;
using ProductsManager.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductsManager.Controlers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CartController : Controller
	{
		private ShopManager manager;

		public CartController(ShopManager manager)
		{
			this.manager = manager;
		}


		[HttpPost("add/{productId}")]
		public ActionResult AddToCart(int productId, int quantity)
		{
			try
			{
				manager.AddToCart(productId, quantity);
			}
			catch (Exception e)
			{
				return BadRequest("Can`t add product to cart , probably bad product Id");
			}

			return Ok();
		}

		[HttpDelete("remove/{id}")]
		public ActionResult RemoveFromCart(int id)
		{
			try
			{
				manager.DeleteOrder(id);
			}
			catch (Exception e)
			{
				return BadRequest("Can`t Remove");
			}

			return Ok();
		}

		[HttpGet("showcart")]
		public ActionResult<List<OrderItem>> ShowCart()
		{
			return manager.Cart;
		}

		[HttpPost("buyall")]
		public ActionResult Buy()
		{
			try
			{
				manager.Buy();
			}
			catch (Exception e)
			{
				BadRequest($"error {e.Message}");
			}
			return Ok();
		}
	}
}
