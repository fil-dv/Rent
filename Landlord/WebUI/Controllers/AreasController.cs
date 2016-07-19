using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models.CustomModels;

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
            List<AreaWithPhotos> areaPhotoList = new List<AreaWithPhotos>();

            foreach (var area in _repoArea.Areas.OrderBy(area => area.AreaID).Skip((page - 1) * pageSize).Take(pageSize))
            {
                AreaWithPhotos awp = new AreaWithPhotos();
                awp.Area = area;
                var photos = _repoPhoto.Photos.Where(ph => ph.AreaID == area.AreaID).ToList();
                awp.Photos = photos;
                areaPhotoList.Add(awp);
            }

            AreaListViewModel model = new AreaListViewModel
            {
                AreasWithPhotosList = areaPhotoList,

                PageInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemPerPage = pageSize,
                    TotalItems = _repoArea.Areas.Count()
                }
            };

            return View(model);
        }
    }
}