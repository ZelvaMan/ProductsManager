namespace ProductsManager.Models
{

	public class OrderItem
	{
		public int OrderId { get; set; }
		public int ProductID { get; set; }
		public int Quantity { get; set; }
		public int TotalPrice { get; set; }

	}
}