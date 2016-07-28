using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models.CustomModels;

namespace WebUI.Controllers
{
    public class AdminController : Controller
    {
        IAreaRepository _repoArea;
        IPhotoRepository _repoPhoto;


        public AdminController(IAreaRepository repoArea, IPhotoRepository repoPhoto)
        {
            _repoArea = repoArea;
            _repoPhoto = repoPhoto;
        }


        public ViewResult Index()
        {
            List<AreaWithPhotos> model = new List<AreaWithPhotos>();

            foreach (var area in _repoArea.Areas)
            {
                AreaWithPhotos awp = new AreaWithPhotos();
                awp.Area = area;
                var photos = _repoPhoto.Photos.Where(ph => ph.AreaID == area.AreaID).ToList();
                awp.Photos = photos;
                model.Add(awp);
            }

            return View(model);
        }

        public ViewResult Edit(int areaID)
        {
            Area area = _repoArea.Areas.FirstOrDefault(b => b.AreaID == areaID);
            return View(area);
        }

        [HttpPost]
        public ActionResult Edit(Area area)
        {
            if (ModelState.IsValid)
            {
                _repoArea.SaveArea(area);
                TempData["message"] = String.Format("Изменения данных объекта ID = \"{0}\" успешно применены. ", area.AreaID);
                return RedirectToAction("Index");
            }
            else
            {
                return View(area);
            }

        }
    }
}