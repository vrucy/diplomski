using System.Collections.Generic;

namespace CraftmanPortal.Models.View
{
    public class postCraftman
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Place { get; set; }
        public string Street { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public IEnumerable<int> CategoriesId { get; set; }
        
    }
}
