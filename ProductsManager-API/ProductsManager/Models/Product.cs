using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsManager.Models
{
	public class Product
	{
		public int ID { get;  set; }
		public string Name { get;  set; }
		public int Price { get; set; }
		public int InStock { get; set; }
		public Availability ProductAvailability { get; set; } = Availability.Avalible;
	}
}
