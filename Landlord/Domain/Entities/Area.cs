using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{

    /// <summary>
    /// DB entitie
    /// </summary>

    public class Area
    {
        public int AreaID { get; set; }
        public string OwnerName { get; set; }
        public string ContactaName { get; set; }
        public string ContactaPhone1 { get; set; }
        public string ContactaPhone2 { get; set; }
        public string ContactaPhone3 { get; set; }
        public string LegalAddress { get; set; }
        public string RentAreaAddress { get; set; }
        public decimal SquareArea { get; set; }        
        public decimal MonthPrice { get; set; }
        public bool IsBooking { get; set; }
        public bool IsAvailable { get; set; }
        public int Rating { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }        
    }
}
