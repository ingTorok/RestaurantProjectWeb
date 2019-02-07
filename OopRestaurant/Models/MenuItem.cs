using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OopRestaurant.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class MenuItem
    {
        public MenuItem()
        {
            AssignableCategories = new List<SelectListItem>();
        }

        //[Key] - ha van int es a neve Id akkor automatikusan o a PrimaryKey
        public int Id { get; set; }
        [Required]
        [StringLength(200)] //now we can Index the Name
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        [Required]
        public Category Category { get; set; }

        #region Only for the View
        [NotMapped] //we dont need for the database
        public List<SelectListItem> AssignableCategories { get; set; }

        [NotMapped] //we dont need for the database
        public int CategoryId { get; set; }
        #endregion Only for the View    
    }
}