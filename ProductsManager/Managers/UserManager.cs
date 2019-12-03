using System.Collections.Generic;
using ProductsManager.Models;

namespace ProductsManager.Managers
{
	public class UserManager
	{
		private readonly ApplicationState state;

		public List<User> Users
		{
			get { return state.Users; }
		}

		public void AddUser(string userName)
		{
			int id = state.LastUserId++;
			state.Users.Add(new User(id,userName));
		}

		public void RemoveUser(int userId)
		{
			User us = state.Users.Find((user => user.UserId == userId));
			state.Users.Remove(us);
		}
	}
}