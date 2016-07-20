﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models.CustomModels
{
    public class AreaWithPhotos
    {
        
        public AreaWithPhotos() { }
       // public AreaWithPhotos(Area area, Photo photo) { }

        public Area Area { get; set; }
        public IEnumerable<Photo> Photos { get; set; }
    }
}