using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OopRestaurant.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class MenuItem
    {
        //[Key] - ha van int es a neve Id akkor automatikusan o a PrimaryKey
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
    }
}