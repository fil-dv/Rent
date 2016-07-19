using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Photo
    {
        public int PhotoID { get; set; }
        public int AreaID { get; set; }
        public string PathToPhoto { get; set; }
        public string PhotoName { get; set; }
        public string PhotoExtension { get; set; }
        public decimal? Latitude  { get; set; }
        public decimal? Longitude  { get; set; }
    }
}
