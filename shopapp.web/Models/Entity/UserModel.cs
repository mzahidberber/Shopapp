﻿using Microsoft.AspNetCore.Identity;

namespace shopapp.web.Models.Entity
{
	public class UserModel:IdentityUser
	{
		public UserModel()
		{
			this.Orders = new List<OrderModel>();
		}
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public List<OrderModel> Orders { get; set; }
	}
}
