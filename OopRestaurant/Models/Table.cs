using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OopRestaurant.Models
{
    public class Table
    {
        public Table()
        {
            AssignableLocations = new List<SelectListItem>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        //Lazy loading
        public virtual Location Location { get; set; }

        #region parts of the View

        [NotMapped]
        public int LocationId { get; set; }

        [NotMapped]
        public List<SelectListItem> AssignableLocations { get; set; }

        #endregion parts of the View
    }
}