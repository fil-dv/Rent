using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class NavController : Controller
    {
        IAreaRepository _repoArea;       

        public NavController(IAreaRepository repository)
        {
            _repoArea = repository;
        }

        public PartialViewResult Menu(string region = null)
        {
            ViewBag.SelectedRegion = region;

            IEnumerable<string> regions = (_repoArea.Areas.Select(a => a.RentAreaAddressRegion).Distinct().OrderBy(x => x)).ToList();
            return PartialView(regions);
        }        
    }
}