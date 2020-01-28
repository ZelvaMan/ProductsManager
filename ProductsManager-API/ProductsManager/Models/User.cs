namespace ProductsManager.Models
{
	public class User
	{
		public int UserId { get; private set; }
		public string UserName { get; private set; }

		public User(int userId, string userName)
		{
			UserId = userId;
			UserName = userName;
		}


	}
}