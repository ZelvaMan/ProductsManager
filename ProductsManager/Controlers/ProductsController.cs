using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductsManager.Managers;
using ProductsManager.Models;

namespace ProductsManager.Controlers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private ShopManager manager;

		public ProductsController(ShopManager manager)
		{
			this.manager = manager;
		}

		[HttpGet()]
		public ActionResult<List<Product>>GetAllProducts()
		{

			return manager.Products;
		}

		[HttpGet("{productId}")]
		public ActionResult<Product> GetProductDetail(int productId)
		{
			
			return manager.FindProductById(productId);
		}

		//[HttpPost("Product/buy/{productId}")]

		[HttpDelete("{productId}")]
		public ActionResult RemoveProduct(int productId)
		{
			try
			{
				manager.DeleteProduct(productId);
			}
			catch (Exception e)
			{
				return BadRequest("Can`t find product.Wrong product ID.");
			}

			return Ok();
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="data"> data from form</param>
		/// <returns></returns>
		[HttpPost()]
		public ActionResult AddProduct(NewProductData data)
		{
			try
			{
				manager.AddProduct(data);
			}
			catch (Exception e)
			{
				return BadRequest("Can`t add new Product.");
			}

			return Ok();
		}

		[HttpPut("{productId}")]
		public ActionResult UpdateProduct([FromQuery]int productId, [FromQuery] string name, [FromQuery] int price, [FromQuery] int inStock)
		{
			try
			{
				manager.ChangeProduct(productId, name, price, inStock);
			}
			catch (Exception e)
			{
				return BadRequest();
			}

			return Ok();

		}
	}
}
