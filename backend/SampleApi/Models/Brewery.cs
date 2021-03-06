﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.Models
{
    public class Brewery
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan HappyHourFrom { get; set; }
        public TimeSpan HappyHourTo { get; set; }
        public int? Established { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int? Zip { get; set; }
        public string SiteURL { get; set; }
        public string Description { get; set; }
        public bool IsBar { get; set; }
        public bool IsBrewery { get; set; }
        public string imgSrc { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public IList<Beer> beersAvailable { get; set; }
    }
}
