﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models.CustomModels
{
    public class AreaListViewModel
    {
        public IEnumerable<AreaWithPhotos> AreasWithPhotosList { get; set; }
        public PagingInfo PageInfo { get; set; }
        public string CurrentRegion { get; set; }
    }
}