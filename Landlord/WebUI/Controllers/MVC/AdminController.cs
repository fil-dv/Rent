using Domain.Abstract;
using Domain.Entities;
using GoogleMaps.LocationServices;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
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
        const string _dirPath = @"..\Content\NewPhotos";

        IAreaRepository _repoArea;
        IPhotoRepository _repoPhoto;
        IPendingRepository _repoPending;

        public AdminController(IAreaRepository repoArea, IPhotoRepository repoPhoto, IPendingRepository repoPending)
        {
            _repoArea = repoArea;
            _repoPhoto = repoPhoto;
            _repoPending = repoPending;
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

        }

        [HttpPost]
        public ActionResult PhotoDelete(int photoID, int areaId)
        {
            Photo photo = _repoPhoto.Photos.Where(p => p.PhotoID == photoID).ToArray()[0];
            _repoPhoto.DeletePhoto(photo);

            return RedirectToAction("Edit", new { areaID = areaId });
        }

        [HttpPost]
        public ActionResult GetCoordByAdress(int areaId)
        {
            // Task.Factory.StartNew(() => {

            string address = String.Empty;
            GoogleLocationService locationService;
            MapPoint point;
            Area area = _repoArea.Areas.Where(a => a.AreaID == areaId).ToArray()[0];

            if (area != null)
            {
                address = area.RentAreaAddressRegion + " обл., " + area.RentAreaAddressCity + ", " + area.RentAreaAddressStreet;
                locationService = new GoogleLocationService();
                point = locationService.GetLatLongFromAddress(address);
                if (point != null)
                {
                    area.Latitude = System.Convert.ToDecimal(point.Latitude);
                    area.Longitude = System.Convert.ToDecimal(point.Longitude);
                }
            }

            try
            {
                _repoArea.SaveAreaChanges(area);
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
            //  });
            return RedirectToAction("Edit", new { areaID = areaId });
        }


        [HttpPost]
        public ActionResult UploadPhoto(int areaId)
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("../Content/Images/"), fileName);
                    file.SaveAs(path);

                    string[] arr = fileName.Split('.');
                    string pExt = "." + arr[arr.Length - 1];
                    string pName = String.Empty;
                    for (int i = 0; i < arr.Length - 1; ++i)
                    {
                        pName += arr[i];
                    }
                    Photo photo = new Photo { AreaID = areaId, PathToPhoto = @"..\Content\Images\", PhotoName = pName, PhotoExtension = pExt };
                    _repoPhoto.SavePhotoChanges(photo);
                }
            }
            return RedirectToAction("Edit", new { areaID = areaId });
        }

       

        public ActionResult AddNewPhotos()
        {
            foreach (Pending pending in _repoPending.Pendings)
            {
                Area area = _repoArea.Areas.Where(p => p.AreaID == pending.AreaID).ToList()[0];
                if (!Directory.Exists(Server.MapPath(_dirPath)))
                {
                    Directory.CreateDirectory(Server.MapPath(_dirPath));
                }
                List<string> listFile = Directory.GetFiles(Server.MapPath(_dirPath)).ToList();
                foreach (var item in listFile)
                {                    
                    FileInfo fi = new FileInfo(item);
                    FileSystemInfo fsi = new FileInfo(item);

                    if (fi.LastWriteTime >= pending.Start && fi.LastWriteTime <= pending.Stop)
                    {
                       
                        var fileName = Path.GetFileName(fi.Name);
                        var path = Path.Combine(Server.MapPath(@"..\Content\Images\"), fileName);

                        bool isOk = false;

                        string sourse = item;
                        string dest = Server.MapPath(@"..\Content\Images\");

                        do
                        {
                            if (!System.IO.File.Exists(path))
                            {
                                if (System.IO.File.Exists(sourse))
                                {
                                    if (System.IO.Directory.Exists(dest))
                                    {
                                        System.IO.File.Copy(sourse, path);
                                        isOk = true;
                                    }
                                }                                
                            }
                            else
                            {
                                fileName = ("_" + fileName);
                                path = Path.Combine(Server.MapPath(@"..\Content\Images\"), fileName);
                            }
                        }
                        while (!isOk);

                        string[] arr = fileName.Split('.');
                        string pExt = "." + arr[arr.Length - 1];
                        string pName = String.Empty;
                        for (int i = 0; i < arr.Length - 1; ++i)
                        {
                            pName += arr[i];
                        }

                        Photo photo = new Photo { AreaID = pending.AreaID, PathToPhoto = @"..\Content\Images\", PhotoName = pName, PhotoExtension = pExt };
                        _repoPhoto.SavePhotoChanges(photo);

                        System.IO.File.Delete(item);
                    }                    
                }
            }
            return RedirectToAction("Index");
        }

    }
}