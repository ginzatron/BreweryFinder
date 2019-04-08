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
        public DateTime HappyHour { get; set; }
        public DateTime OperatingHours { get; set; }
        public int Established { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
    }
}
