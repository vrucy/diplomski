﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CraftmanPortal.Models
{
    public class User
    {
        public int Id { get; set; }
        public ApplicationUser Idenity { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Place { get; set; }
        public string Street { get; set; }
        public ICollection<Case>Cases { get; set; }

    }
}
