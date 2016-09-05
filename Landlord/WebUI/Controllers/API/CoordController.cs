using Domain.Abstract;
using Domain.Entities;
using GoogleMaps.LocationServices;
using Newtonsoft.Json;
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


namespace WebUI.Controllers.API
{
    public class CoordController : ApiController
    {
        IAreaRepository _repoArea;
        IPendingRepository _repoPending;

        // public CoordController() { }

        public CoordController(IAreaRepository repoArea, IPendingRepository repoPending)
        {
            _repoArea = repoArea;
            _repoPending = repoPending;
        }


        [HttpGet]
        public string Test(int latit, int longit)
        {
            //int res = x + y;
            List<string> list = new List<string> { "one", "two", "three" };
            string jsonStr = JsonConvert.SerializeObject(list);
            return jsonStr;
        }

        void InitRepo()
        {

        }

        [HttpGet]
        public string GetNearlyAreas(decimal? latit, decimal? longit)
        {
            const int areasCount = 3;

            InitRepo();
            

            List<DeltaModel> listWithDelta = new List<DeltaModel>();

            foreach (Area item in _repoArea.Areas)
            {
                if (latit != null && longit != null && item.Latitude != null && item.Longitude != null)
                {
                    DeltaModel dModel = new DeltaModel
                    {
                        AreaId = item.AreaID,
                        DeltaLat = Math.Abs((decimal)(item.Latitude - latit)),
                        DeltaLong = Math.Abs((decimal)(item.Longitude - longit))
                    };

                    listWithDelta.Add(dModel);
                }
            }

            List<NearlyAreaModel> nearlyAreaList = new List<NearlyAreaModel>();

            for (int i = 0; i < areasCount; ++i)
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
                        ContactaPhone1 = area.ContactaPhone1,
                        Latitude = area.Latitude,
                        Longitude = area.Longitude
                    };

                    nearlyAreaList.Add(nearlyAreaModel);

                    listWithDelta.Remove(deltaModel);
                }
            }
            string jsonStr = JsonConvert.SerializeObject(nearlyAreaList);
            return jsonStr;
        }

        [HttpPost]
        public void AddOrUpdateArea(string jsonString)
        {
            NearlyAreaModel nAreaModel = JsonConvert.DeserializeObject<NearlyAreaModel>(jsonString);
            Area area = GetAreaByNearlyModel(nAreaModel);
            _repoArea.SaveAreaChanges(area);
        }

        private Area GetAreaByNearlyModel(NearlyAreaModel nAreaM)
        {
            Area area = new Area
            {
                AreaID = nAreaM.AreaId,
                AreaTypeID  = 1,
                AreaDescription = "",
                OwnerName = "",
                ContactaName = "", 
                ContactaPhone1 =  nAreaM.ContactaPhone1,
                ContactaPhone2 = "",
                ContactaPhone3 = "",
                LegalAddressRegion = "",
                LegalAddressCity = "",
                LegalAddressStreet = "",
                RentAreaAddressRegion = nAreaM.Region,
                RentAreaAddressCity = nAreaM.City, 
                RentAreaAddressStreet =nAreaM.Street, 
                SquareArea = 0,
                MonthPrice = 0,
                IsAvailable = false, 
                Rating = 0, 
                Latitude = nAreaM.Latitude, 
                Longitude = nAreaM.Longitude
            };
            _repoArea.SaveAreaChanges(area);
            return area;
        }

        [HttpPost]
        public NearlyAreaModel AddOrUpdateAreaForUnitTest(string jsonString)
        {
            NearlyAreaModel nAreaModel = JsonConvert.DeserializeObject<NearlyAreaModel>(jsonString);
            return nAreaModel;
        }

        public int GetNewAreaId(string jsonString)
        {
            NearlyAreaModel nAreaModel = JsonConvert.DeserializeObject<NearlyAreaModel>(jsonString);
            Area area = GetAreaByNearlyModel(nAreaModel);
            return area.AreaID;
        }

        [HttpPost]
        public string SetStartTime(string jsonString)
        {
            Pending[] isInProgress = _repoPending.Pendings.Where(p => p.Stop == null).ToArray();
            if (isInProgress.Length > 1)
            {
                return null;
            }
            Pending pending = JsonConvert.DeserializeObject<Pending>(jsonString);
            _repoPending.AddOrUpdatePending(pending);
            return pending.PendingID.ToString();
        }

        [HttpPost]
        public void SetStopTime(string jsonString)
        {
            Pending pending = JsonConvert.DeserializeObject<Pending>(jsonString);
            _repoPending.AddOrUpdatePending(pending);
        }


      


    }
}
