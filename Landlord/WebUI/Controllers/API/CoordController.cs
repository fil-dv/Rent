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
using System.Web.Http;

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
    }
}
