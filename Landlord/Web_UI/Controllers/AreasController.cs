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

        public AreasController(IAreaRepository repository, IPhotoRepository repoPhoto)
        {
            _repoArea = repository;
            _repoPhoto = repoPhoto;
        }

        public ViewResult List()
        {
            //ViewBag["photo"] = _repoPhoto;
            return View(_repoArea.Areas);
        }
    }
}