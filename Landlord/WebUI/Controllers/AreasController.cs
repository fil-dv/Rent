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

        public ViewResult List(string region, int page = 1)
        {
            List<AreaWithPhotos> areaPhotoList = new List<AreaWithPhotos>();

            foreach (var area in _repoArea.Areas.Where(r => region == null || r.RentAreaAddressRegion == region).OrderBy(area => area.AreaID).Skip((page - 1) * pageSize).Take(pageSize))
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
                },

                CurrentRegion = region

            };

            return View(model);
        }
    }
}