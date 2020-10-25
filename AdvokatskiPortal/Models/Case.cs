﻿using System;
using System.Collections.Generic;

namespace CraftmanPortal.Models
{
    public class Case
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //TODO for mechanical esay to find veacle 
        public double GSirina { get; set; }
        public double GDuzina { get; set; }
        public DateTime DeadLineForAnswer { get; set; }
        public int CategoryId { get; set; }
        public Category Category{ get; set; }
        public int UserId { get; set; }
        public User User{ get; set; }
        public ICollection<Picture> Pictures { get; set; }
        public ICollection<CaseCraftman> CaseCraftmans{ get; set; }
        public ICollection<Contract> Contracts { get; set; }
        public ICollection<Notification> Notifications { get; set; }
    }
}
