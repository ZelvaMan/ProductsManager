using System;
using System.Collections.Generic;
using System.Linq;
using ProductsManager.Models;

namespace ProductsManager.Managers
{
	public class ProductManager
	{
		private readonly ApplicationState state;

		public ProductManager(ApplicationState state)
		{
			this.state = state;
			Load("");
		}

		/// <summary>
		/// return products stored at appstate
		/// </summary>
		public List<Product> Products
		{
			get
			{
				return state.Products;
			}
		}

		/// <summary>
		/// load Products from JSON file
		/// </summary>
		/// <param name="path"></param>
		public void Load(string path)
		{
			//load products from JSON file
			if (state.Products.Count == 0)
			{

				//delete this
				for (int i = 1; i < 25; i++)
				{
					AddProduct("product" + i.ToString("D2"), i * 25, i * 2);
				}
				//delete this
			}
			Order();
		}

		/// <summary>
		/// order products in <see cref="Products"/> 
		/// </summary>
		public void Order()
		{
			state.Products = state.Products.OrderBy((product => product.ID)).ToList();
		}

		/// <summary>
		///add product to <see cref="Products"/>
		/// /// </summary>
		public void AddProduct(string name, int price, int inStock)
		{
			int id = state.LastProductId++;
			state.Products.Add(new Product() { ID = id, InStock = inStock, Name = name, Price = price });
			Order();
		}

		/// <summary>
		/// remove product 
		/// </summary>
		/// <param name="productId">Id of product</param>
		public void RemoveProduct(int productId)
		{
			state.Products.Remove(FindProductById(productId));
		}

		public Product FindProductById(int productId)
		{
			try
			{
				return state.Products.Find((product => product.ID == productId));
			}
			catch (Exception ex)
			{
				throw new ArgumentOutOfRangeException(nameof(productId), $"Could not find product with Id:{ productId }");
			}
		}

		public void ChangeStock(int productId, int quantity)
		{
			var pr = FindProductById(productId);
			pr.InStock += quantity;
			if (pr.InStock <= 0)
			{
				pr.InStock = 0;
				SetAvailability(Availability.UnAvailable, productId);
			}
			else
				SetAvailability(Availability.Avalible, productId);
		}

		public void SetAvailability(Availability productAvailability, int productId)
		{
			FindProductById(productId).ProductAvailability = productAvailability;
		}
	}
}