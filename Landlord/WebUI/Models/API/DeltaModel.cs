using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models.API
{
    public class DeltaModel
    {
        public int AreaId { get; set; }
        public decimal? DeltaLat { get; set; }
        public decimal? DeltaLong { get; set; }
    }
}