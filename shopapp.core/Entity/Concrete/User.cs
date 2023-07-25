﻿using Microsoft.AspNetCore.Identity;
using shopapp.core.Entity.Abstract;

namespace shopapp.core.Entity.Concrete
{
    public class User:IdentityUser,IEntity
    {
        public User()
        {
            this.Orders = new List<Order>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Order> Orders { get; set; }

    }
}