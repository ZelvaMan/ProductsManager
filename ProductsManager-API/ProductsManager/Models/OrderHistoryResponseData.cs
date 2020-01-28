using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsManager.Models
{
	public struct OrderHistoryResponseData
	{
		public int Last { get; private set; }
		public bool Canceled { get; private set; }
		public int StartingPosition { get; private set; }
		public List<Order> Orders { get; private set; }


		public OrderHistoryResponseData(int last, bool canceled, int startingPosition, List<Order> orders)
		{
			Last = last;
			Canceled = canceled;
			StartingPosition = startingPosition;
			Orders = orders;
		}
	}
}
