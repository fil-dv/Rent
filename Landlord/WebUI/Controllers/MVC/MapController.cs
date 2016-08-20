using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class MapController : Controller
    {       
        IAreaRepository _repoArea;

        public MapController(IAreaRepository repository)
        {
            _repoArea = repository;
        }

        public ViewResult ShowOnMap(int areaID)
        {
            Area area = _repoArea.Areas.FirstOrDefault(a => a.AreaID == areaID);
            return View(area);
        }



    }
}