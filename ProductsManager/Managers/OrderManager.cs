using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductsManager.Models;

namespace ProductsManager.Managers
{
	public class OrderManager
	{
		private ApplicationState state;


		public List<Order> OrdersWithoutCanceled
		{
			get
			{
				List<Order> orders = new List<Order>();
				//foreach all orders, if isn't canceled, add to list which then return 
				foreach (Order order in OrdersWithCanceled)
				{
					if(order.OrderValidity == validity.Valid)
						orders.Add(order);
				}

				return orders;
			}
		}

		public List<Order> OrdersWithCanceled
		{
			get { return state.Orders; }
		}
		/// <summary>
		/// inicializate orderManager
		/// </summary>
		/// <param name="state"></param>
		public OrderManager(ApplicationState state)
		{
			this.state = state;
			//smazat
			if (state.Orders.Count == 0)
			{
				state.LastOrderId = 1;
				state.Orders.Clear();
				for (int i = 0; i < 23; i++)
				{
					AddOrder(new List<OrderItem>());
				}
			}
			//smazat

		}

		public void AddOrder(List<OrderItem> Cart)
		{
			int id = state.LastOrderId++;
			state.Orders.Add(new Order(){orderId = id,OrderItems = Cart});
			
		}

		public void RemoveOrder(int orderId)
		{
			if (orderId <= state.LastOrderId)
			{
				state.Orders.Remove(state.Orders.Find((order => order.orderId == orderId)));
			}
		}

		public Order FindOrderByOrderId(int orderId)
		{
			return state.Orders.Find((order => order.orderId == orderId));
		}


		/// <summary>
		/// set validity of order to canceled
		/// </summary>
		/// <param name="orderId"></param>
		public void CancelOrder(int orderId)
		{
			Order ord = OrdersWithCanceled.Find((order => order.orderId == orderId));
			ord.OrderValidity = validity.Canceled;
		}
	}
}
