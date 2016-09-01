using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models.API
{
    public class NearlyAreaModel
    {
        public int AreaId { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ContactaPhone1 { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }
}