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

        [Display(Name = "AreaID")]
        //[HiddenInput(DisplayValue = false)]
        [Editable(false)]
        public int AreaTypeID { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание:")]
        public string AreaDescription { get; set; }

        [Display(Name = "Владелец:")]
        public string OwnerName { get; set; }
        [Display(Name = "Контактное лицо:")]
        public string ContactaName { get; set; }
        [Display(Name = "Контактный телефон №1:")]
        public string ContactaPhone1 { get; set; }
        [Display(Name = "Контактный телефон №2:")]
        public string ContactaPhone2 { get; set; }
        [Display(Name = "Контактный телефон №3:")]
        public string ContactaPhone3 { get; set; }
        [Display(Name = "Юридический адрес владельца(область)")]
        public string LegalAddressRegion { get; set; }
        [Display(Name = "Юридический адрес владельца(город)")]
        public string LegalAddressCity { get; set; }
        [Display(Name = "Юридический адрес владельца(улица, дом, [квартира])")]
        public string LegalAddressStreet { get; set; }
        [Display(Name = "Область")]
        public string RentAreaAddressRegion { get; set; }
        [Display(Name = "Город")]
        public string RentAreaAddressCity { get; set; }
        [Display(Name = "Улица, дом, [квартира]")]
        public string RentAreaAddressStreet { get; set; }
        [Display(Name = "Площадь, м2")]
        public decimal SquareArea { get; set; }
        [Display(Name = "Стоимость аренды в месяц")]
        public decimal MonthPrice { get; set; }
        [Display(Name = "Доступность в настоящее время")]
        public bool IsAvailable { get; set; }
        [Display(Name = "Популярность")]
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
