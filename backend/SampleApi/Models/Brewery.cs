using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.Models
{
    public class Brewery
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime HappyHourFrom { get; set; }
        public DateTime  HappyHourTo { get; set; }
        public int Established { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public string SiteURL { get; set; }
        public string Description { get; set; }
        public bool IsBar { get; set; }
        public bool IsBrewery { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        IList<Beer> beersAvailable { get; set; }
    }
}
