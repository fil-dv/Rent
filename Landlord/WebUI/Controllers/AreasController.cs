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
            List<AreaWithPhoto> areaPhotoList = new List<AreaWithPhoto>();

            foreach (var area in _repoArea.Areas.OrderBy(area => area.AreaID).Skip((page - 1) * pageSize).Take(pageSize))
            {
                AreaWithPhoto awp = new AreaWithPhoto();
                awp.Area = area;
                awp.Photo = _repoPhoto.Photos.First(ph => ph.AreaID == area.AreaID);
                areaPhotoList.Add(awp);
            }

            AreaListViewModel model = new AreaListViewModel
            {
                AreasWithPhoto = areaPhotoList,

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