using Domain.Abstract;
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


        public AdminController(IAreaRepository repository, IPhotoRepository repoPhoto)
        {
            _repoArea = repository;
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
    }
}