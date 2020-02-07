using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ProductsManager.Models;

namespace ProductsManager.Managers
{
	public class OrderItemManager
	{
		private ApplicationState state;

		public OrderItemManager(ApplicationState state)
		{
			this.state = state;
		}

		public List<OrderItem> OrderItems
		{
			get { return state.Cart; }
		}

		#region Removing

		/// <summary>
		/// remove order Item
		/// </summary>
		/// <param name="order"></param>
		public void RemoveOrderItem(OrderItem order)
		{
			state.Cart.Remove(order);
		}

		/// <summary>
		/// remove multiple order items
		/// </summary>
		/// <param name="orders"></param>
		public void RemoveOrderItems(List<OrderItem> orders)
		{
			foreach (var order in orders)
			{
				RemoveOrderItem(order);
			}
		}

		/// <summary>
		/// clear cart
		/// </summary>
		public void RemoveAll()
		{
			state.Cart = new List<OrderItem>();
		}
		#endregion

		//finding order items
		#region Finding


		/// <summary>
		/// find order item with product id
		/// </summary>
		/// <param name="productId">id of product</param>
		/// <returns></returns>
		public OrderItem FindOrderItemByProductId(int productId)
		{
			try
			{
				return state.Cart.Find((order => order.ProductID == productId));
			}
			catch (Exception ex)
			{
				throw new ArgumentOutOfRangeException(nameof(productId),$"Could not find orders with Id:{productId}");
			}
		}

		/// <summary>
		/// find order item  with order Id
		/// </summary>
		/// <param name="OrderId"></param>
		/// <returns></returns>
		public OrderItem FindOrderItemByOrderId(int OrderId)
		{
			return state.Cart.Find((order => order.OrderId == OrderId));
		}

		#endregion

		/// <summary>
		/// ad order item to cart, count total price
		/// </summary>
		/// <param name="productId"></param>
		/// <param name="price"></param>
		/// <param name="quantity"></param>
		public void AddOrderItem( int productId, int price, int quantity)
		{
			int id = state.LastProductId++;
			state.Cart.Add(new OrderItem() { OrderId = id, TotalPrice = price * quantity,  ProductID = productId, Quantity = quantity});
		}

		/// <summary>
		/// change hoe many pieces of that product you have  in cart
		/// </summary>
		/// <param name="productId"></param>
		/// <param name="newQuantity"></param>
		public void changeQuantity(int productId, int newQuantity)
		{

		OrderItem ord=	FindOrderItemByProductId(productId);
		ord.Quantity = newQuantity;
		}
	}
}