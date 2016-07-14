using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class AreasController : Controller
    {
        IAreaRepository _repoArea;
        IPhotoRepository _repoPhoto;

        public int pageSize = 12;

        public AreasController(IAreaRepository repository, IPhotoRepository repoPhoto)
        {
            _repoArea = repository;
            _repoPhoto = repoPhoto;
        }

        public ViewResult List(int page = 1)
        {
            //ViewBag["photo"] = _repoPhoto;
            return View(_repoArea.Areas
                .OrderBy(area => area.AreaID)
                .Skip((page - 1) * pageSize)
                .Take(pageSize));
        }
    }
}