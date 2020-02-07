using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProductsManager.Managers;
using ProductsManager.Models;
using Serilog.Events;

namespace ProductsManager.Controlers
{

	[Route("api/[controller]")]
	[ApiController]
	public class ProductsControler : ControllerBase
	{
		
		private ShopManager manager;

		public ProductsControler(ShopManager manager)
		{
			this.manager = manager;
		}

		[HttpGet("Product")]
		public ActionResult<List<Product>> ShowProducts()
		{
			Log.Information("Returned products");
			return manager.Products;
		}

		[HttpGet("Product/{productId}")]
		public ActionResult<Product> ShowBuyProduct(int productId)
		{
			return  manager.FindProductById(productId);
		}

		

		[HttpPost("Product/remove/{productId}")]
		public ActionResult RemoveProduct(int productId)
		{
			manager.DeleteProduct(productId);
			return Ok();
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
			return Ok();
		}
	}
}