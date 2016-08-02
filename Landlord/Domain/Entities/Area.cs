using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain.Entities
{

    /// <summary>
    /// DB entitie
    /// </summary>

    public class Area 
    {
        [Display(Name = "ID")]
        //[HiddenInput (DisplayValue = false)]
        [Editable(false)]
        public int AreaID { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int AreaTypeID { get; set; }

        public string AreaDescription { get; set; }
        public string OwnerName { get; set; }
        public string ContactaName { get; set; }
        public string ContactaPhone1 { get; set; }
        public string ContactaPhone2 { get; set; }
        public string ContactaPhone3 { get; set; }
        public string LegalAddressRegion { get; set; }

        public string LegalAddressCity { get; set; }

        public string LegalAddressStreet { get; set; }

        [Display(Name = "Область")]
        public string RentAreaAddressRegion { get; set; }

        [Display(Name = "Город")]
        public string RentAreaAddressCity { get; set; }

        [Display(Name = "Улица, дом")]
        public string RentAreaAddressStreet { get; set; }

        public decimal SquareArea { get; set; }

        public decimal MonthPrice { get; set; }

        public bool IsAvailable { get; set; }
        
        public int Rating { get; set; }

        
        [DisplayFormat(DataFormatString = "{0:n10}", ApplyFormatInEditMode = true)]
        [Display(Name = "Широта")]
        [Editable(false)]
        public decimal? Latitude { get; set; }

        [DisplayFormat(DataFormatString = "{0:n10}", ApplyFormatInEditMode = true)]
        [Display(Name = "Долгота")]
        [Editable(false)]
        public decimal? Longitude { get; set; }        
    }
}
