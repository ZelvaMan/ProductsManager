using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsManager.Models
{
	public class ApplicationState
	{
		public List<Product> Products { get; set; } = new List<Product>();

		public List<OrderItem> Cart { get; set; } = new List<OrderItem>();

		public List<User> Users { get; set; } = new List<User>();

		public List<Order> Orders { get; set;  } = new List<Order>();

		public int LastProductId { get; set; } = 1;
		public int LastOrderItemId { get; set; } = 1;
		public int LastUserId { get; set; } = 1;
		public int LastOrderId { get; set; } = 1;
		public int OrdersPerPage { get; set; } = 5;
		public ApplicationState()
		{
		}

	}
}
