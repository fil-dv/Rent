using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models.CustomModels
{
    public class AreaWithPhoto
    {
        
        public AreaWithPhoto() { }
        public AreaWithPhoto(Area area, Photo photo) { }

        public Area Area { get; set; }
        public Photo Photo { get; set; }
    }
}