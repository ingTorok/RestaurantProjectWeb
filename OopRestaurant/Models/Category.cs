using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OopRestaurant.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Display(Name="Category")]
        public string Name { get; set; }

        public List<MenuItem> MenuItems { get; set; }
    }
}