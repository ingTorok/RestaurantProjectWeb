using System.Collections.Generic;

namespace OopRestaurant.Models
{
    public class Location
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsNonSmoking { get; set; }

        public virtual List<Table> Tables { get; set; }
    }
}