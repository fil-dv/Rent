using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebUI.Models.API
{
    [DataContract]
    public class NearlyAreaModel
    {
        [DataMember]
        public int AreaId { get; set; }
        [DataMember]
        public string Region { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string Street { get; set; }
        [DataMember]
        public string ContactaPhone1 { get; set; }
        [DataMember]
        public decimal? Latitude { get; set; }
        [DataMember]
        public decimal? Longitude { get; set; }
    }
}