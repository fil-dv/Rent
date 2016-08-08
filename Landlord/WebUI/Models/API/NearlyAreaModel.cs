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
        public string Floor { get; set; }
        public string Flat { get; set; }
    }
}