using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.Models
{
    public class Beer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Style_id { get; set; }
        public string Style { get; set; }
        public string Description { get; set; }
        public decimal Abv { get; set; }
        public string ImgSrc { get; set; }

        public IList<Brewery> Locations { get; set; }
    }
}
