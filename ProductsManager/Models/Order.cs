using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsManager.Models
{
	public class Order
	{
		public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
		public int orderId { get; set; }
		public validity OrderValidity { get; set; } = validity.Valid;

		public int TotalPrice
		{
			get
			{
				int price = 0;
				foreach (OrderItem item in OrderItems)
				{
					price += item.TotalPrice;
				}

				return price;
			}
		}
		public int NumberOfProducts
		{
			get
			{
				return OrderItems.Count;
			}
		}
		public int TotalNumberOfProducts
		{
			get
			{

				int number = 0;
				foreach (OrderItem item in OrderItems)
				{
					number += item.Quantity;
				}

				return number;
			}
		}

	}
}
