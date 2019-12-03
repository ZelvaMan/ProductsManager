using System;
using System.Collections.Generic;
using System.Linq;
using ProductsManager.Models;
using Remotion.Linq.Parsing.Structure.IntermediateModel;

namespace ProductsManager.Managers
{
	public class ShopManager
	{
		private OrderItemManager orderItemManager;
		private ProductManager productManager;
		private UserManager userManager;
		private OrderManager orderManager;
		private ApplicationState state;
		public List<Product> Products
		{
			get { return productManager.Products; }
		}
		public List<OrderItem> Cart
		{
			get { return orderItemManager.OrderItems; }
		}
		public List<User> Users
		{
			get { return userManager.Users; }
		}

		public List<Order> Orders
		{
			get { return orderManager.OrdersWithCanceled; }
		}



		public ShopManager(OrderItemManager orderItemManager, ProductManager productManager, UserManager userManager, OrderManager orderManager, ApplicationState state)
		{
			this.orderItemManager = orderItemManager;
			this.orderManager = orderManager;
			this.productManager = productManager;
			this.userManager = userManager;
			this.state = state;
		}


		public void AddToCart(int productId, int quantity)
		{

			Product product = productManager.FindProductById(productId);
			if (quantity <= product.InStock)
			{
				if (product.ProductAvailability == Availability.Avalible)
				{
					//productManager.ChangeStock(productId, -quantity);
					orderItemManager.AddOrderItem(productId, product.Price, quantity);
					productManager.SetAvailability(Availability.InCart,productId);
				}

				orderItemManager.changeQuantity(productId, quantity);
			}
			else
			{
				throw new ArgumentOutOfRangeException(nameof(quantity), "not enough products in stock");
			}
		}

		public void Buy()
		{
			if(orderItemManager.OrderItems.Count == 0)
				return;
			foreach (OrderItem ord in orderItemManager.OrderItems)
			{
				productManager.ChangeStock(ord.ProductID, -ord.Quantity);
				
			}
			orderManager.AddOrder(orderItemManager.OrderItems);
			orderItemManager.RemoveAll();
		}

		public Product FindProductById(int productId)
		{
			return productManager.FindProductById(productId);
		}

		public void DeleteOrder(int orderId)
		{
			OrderItem orderToDelete = orderItemManager.FindOrderItemByOrderId(orderId);
			orderItemManager.RemoveOrderItem(orderToDelete);
		}

		public void DeleteProduct(int productId)
		{
			productManager.RemoveProduct(productId);
			orderItemManager.RemoveOrderItem(orderItemManager.FindOrderItemByProductId(productId));
		}

		public Order FindOrderByOrderId(int orderId)
		{
			return orderManager.FindOrderByOrderId(orderId);

		}

		public List<Order> GetFiveOrders(bool withCanceled, int startingPosition)
		{
			return (withCanceled ? orderManager.OrdersWithCanceled : orderManager.OrdersWithoutCanceled)
				.Skip(startingPosition)
				.Take(5).ToList();
		}


		public int LastStartingPostion(bool withCanceled)
		{
			int perPage = state.OrdersPerPage;
			int count = (withCanceled ? orderManager.OrdersWithCanceled : orderManager.OrdersWithoutCanceled).Count;
			int I = count % perPage;
			if (I == 0)
				return count - perPage;
			return count - I;
		}

		/// <summary>
		/// cancel order (set ist <see cref="validity"/> cancelled />
		/// </summary>
		/// <param name="orderId"></param>
		public void CancelOrder(int orderId)
		{
			orderManager.CancelOrder(orderId);
		}

		/// <summary>
		/// adds new product with data from addForm
		/// </summary>
		/// <param name="model"></param>
		public void AddProduct(ProductFormModel model)
		{
			productManager.AddProduct(model.Name,model.Price, model.InStock);
		}


	}
}