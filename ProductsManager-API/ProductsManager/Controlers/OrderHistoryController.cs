using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using ProductsManager.Managers;
using ProductsManager.Models;


namespace ProductsManager.Controlers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderHistoryController : Controller
	{
		private ShopManager manager;
		

		public OrderHistoryController(ShopManager manager)
		{
			//Logger = logger;
			this.manager = manager;
		}


		[HttpGet("getorderhistory")]
		public ActionResult<OrderHistoryResponseData> ShowOrderHistory([FromQuery]int startingPosition, [FromQuery] bool canceled)
		{
			bool canceledBool;
			//if cant parse, cancelBool is false
			if (!bool.TryParse(canceled.ToString(), out canceledBool))
				canceledBool = false;
			OrderHistoryResponseData OHRD = new OrderHistoryResponseData(
				manager.LastStartingPostion(canceledBool),
				canceledBool,
				startingPosition,
				manager.GetFiveOrders(canceledBool, startingPosition)
			);
			
			return OHRD;

		}



		[HttpGet("orderdetail")]
		public ActionResult<Order> GetOrderDetail(int orderId)
		{
			Order o;
			try
			{
				o =  manager.FindOrderByOrderId(orderId);
			
			}
			catch (Exception e)
			{
			
				return BadRequest("Can`t find order, check Id");
			}
			return o;
		}

		[HttpDelete("cancel/{orderId}")]
		public ActionResult CancelOrder(int orderId)
		{
			try
			{
				manager.CancelOrder(orderId);
				
			}
			catch (Exception e)
			{
				return BadRequest("Can`t cancel order, check order id");
			}
			return Ok();

		}
	}
}
