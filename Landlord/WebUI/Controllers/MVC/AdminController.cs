using Domain.Abstract;
using Domain.Entities;
using GoogleMaps.LocationServices;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
            List<Photo> photos = _repoPhoto.Photos.Where(ph => ph.AreaID == area.AreaID).ToList();

            AreaWithPhotos model = new AreaWithPhotos
            {
                Area = area,
                Photos = photos
            };

            //ViewBag.Photos = photos;

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Area area)
        {
            if (ModelState.IsValid)
            {
                _repoArea.SaveAreaChanges(area);
                TempData["message"] = String.Format("Изменения данных объекта ID = \"{0}\" успешно применены. ", area.AreaID);
                return RedirectToAction("Index");
            }
            else
            {
                return View(area);
            }
        }

        public ActionResult GetCoordByAddressForAll()
        {
            Task.Factory.StartNew(() => {

                IEnumerable<Area> list = _repoArea.Areas.Where(r => r.Latitude == null || r.Longitude == null);
                string address = String.Empty;
                GoogleLocationService locationService;
                MapPoint point;
                int counter = 0;

                foreach (Area item in list)
                {
                    try
                    {
                        address = item.RentAreaAddressRegion + " обл., " + item.RentAreaAddressCity + ", " + item.RentAreaAddressStreet;
                        locationService = new GoogleLocationService();
                        point = locationService.GetLatLongFromAddress(address);
                        if (item.Latitude == null)
                            item.Latitude = System.Convert.ToDecimal(point.Latitude);
                        if (item.Longitude == null)
                            item.Longitude = System.Convert.ToDecimal(point.Longitude);
                        if (++counter > 30) break;
                        //Console.WriteLine(String.Format("{0}) {1}\tширота - {2}\tдолгота - {3}", ++counter, address, item.Latitude, item.Longitude));
                        Thread.Sleep(2000);
                    }
                    catch (NullReferenceException)
                    {
                        continue;
                    }
                }

                try
                {
                    _repoArea.SaveAllAreasChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}",
                                                    validationError.PropertyName,
                                                    validationError.ErrorMessage);
                        }
                    }
                }
            });
            

            TempData["message"] = String.Format("Получение координат объектов завершено.");
            return RedirectToAction("Index");

            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    Console.WriteLine(ex.InnerException.Message);
            //}
        }

        [HttpPost]
        public ActionResult PhotoDelete(int photoID, int areaId)
        {
            Photo photo = _repoPhoto.Photos.Where(p => p.PhotoID == photoID).ToArray()[0];
            _repoPhoto.DeletePhoto(photo);

            return RedirectToAction("Edit", new { areaID = areaId });
        } 
    }
}