using Domain.Abstract;
using Domain.Entities;
using GoogleMaps.LocationServices;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Results;
using WebUI.Models.API;
//using System.Web.Mvc;

namespace WebUI.Controllers.API
{
    public class CoordController : ApiController
    {
        IAreaRepository _repoArea;

        public CoordController(IAreaRepository repository)
        {
            _repoArea = repository;
        }


        public void GetCoordByAddressForAll()
        {
            IEnumerable<Area> list = _repoArea.Areas.Where(r => r.Latitude == null || r.Longitude == null);
            string address = String.Empty;
            GoogleLocationService locationService;
            MapPoint point;
            //int counter = 0;

            foreach (Area item in list)
            {
                address = item.RentAreaAddressRegion + " обл., " + item.RentAreaAddressCity + ", " + item.RentAreaAddressStreet;
                locationService = new GoogleLocationService();
                point = locationService.GetLatLongFromAddress(address);
                if (item.Latitude == null)
                    item.Latitude = System.Convert.ToDecimal(point.Latitude);
                if (item.Longitude == null)
                    item.Longitude = System.Convert.ToDecimal(point.Longitude);
                //Console.WriteLine(String.Format("{0}) {1}\tширота - {2}\tдолгота - {3}", ++counter, address, item.Latitude, item.Longitude));
                Thread.Sleep(200);
            }

            try
            {
               // _repoArea.SaveChanges();
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException.Message);
            }
        }

        public JsonResult<List<NearlyAreaModel>> GetNearlyAreas(decimal? latit, decimal? longit)
        {
            const int areasCount = 3;

            List<DeltaModel> listWithDelta = new List<DeltaModel>();

            foreach (Area item in _repoArea.Areas)
            {
                if(latit != null && longit != null && item.Latitude!= null && item.Longitude !=null)
                {
                    DeltaModel dModel = new DeltaModel
                    {
                        AreaId = item.AreaID,
                        DeltaLat = item.Latitude - latit,
                        DeltaLong = item.Longitude - longit
                    };

                    listWithDelta.Add(dModel);
                }               
            }

            List<NearlyAreaModel> nearlyAreaList = new List<NearlyAreaModel>();

            for(int i = 0; i < areasCount; ++i)
            {
                decimal? minDeltaLat = listWithDelta.Min(d => d.DeltaLat);
                decimal? minDeltaLong = listWithDelta.Min(d => d.DeltaLong);

                DeltaModel deltaModel = new DeltaModel();

                if (minDeltaLat < minDeltaLong)
                {
                    deltaModel = listWithDelta.Find(d => d.DeltaLat == minDeltaLat);
                }
                else
                {
                    deltaModel = listWithDelta.Find(d => d.DeltaLong == minDeltaLong);
                }

                
                if (deltaModel != null)
                {
                    Area area = _repoArea.Areas.ToList().Find(a => a.AreaID == deltaModel.AreaId);

                    NearlyAreaModel nearlyAreaModel = new NearlyAreaModel
                    {
                        AreaId = area.AreaID,
                        Region = area.RentAreaAddressRegion,
                        City = area.RentAreaAddressCity,
                        Street = area.RentAreaAddressStreet,
                        Flat = "12345", ////////////////////////////
                        Floor = ""      ////////////////////////////
                    };

                    nearlyAreaList.Add(nearlyAreaModel);

                    listWithDelta.Remove(deltaModel);
                }  
            }
            return Json(nearlyAreaList);
        }
    }
}
