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
using System.Text;
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


        public CoordController(IAreaRepository repoArea, IPendingRepository repoPending)
        {
            _repoArea = repoArea;
            _repoPending = repoPending;
        }


        //[HttpGet]
        //public string Test(string id)
        //{
        //    //int res = x + y;
        //    List<string> list = new List<string> { "one", "two", "three" };
        //    string jsonStr = JsonConvert.SerializeObject(list);
        //    return jsonStr;
        //}

            
        /*http://www.geo.somee.com/api/Coord/GetNearlyAreas/50.52937692/30.79652152*/

        [HttpGet]
        public string GetNearlyAreas(decimal? latit, decimal? longit)
        {
            try
            {
               
                const int areasCount = 3;

           
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


                StringBuilder ListBuilder = new StringBuilder();

                foreach (var item in nearlyAreaList)
                {
                    ListBuilder.Append(item.AreaId);
                    ListBuilder.Append(":");
                    ListBuilder.Append(item.ContactaPhone1);
                    ListBuilder.Append(":");
                    ListBuilder.Append(item.Region);
                    ListBuilder.Append(":");
                    ListBuilder.Append(item.City);
                    ListBuilder.Append(":");
                    ListBuilder.Append(item.Street);
                    ListBuilder.Append("#");
                }

                return ListBuilder.ToString();
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }

        [HttpGet]
        public string AddOrUpdateArea(string areaStr, decimal? latit, decimal? longit)
        {
            try
            {
                string[] arr = areaStr.Split('!'); //0!Киевская!Бровары!ул. Воссоединения д. 11, кв. 148!0501909882/50.5090258/30.7827258/

                int AreaId = Convert.ToInt32(arr[0]);
                string Region = arr[1];
                string City = arr[2];
                string Street = arr[3];
                string ContactaPhone1 = arr[4];
                decimal? Latitude = latit; //null; //(decimal?)Convert.ToDecimal(arr[5]);
                decimal? Longitude = longit; //(decimal?)Convert.ToDecimal(arr[6]);


                //NearlyAreaModel nAreaModel = JsonConvert.DeserializeObject<NearlyAreaModel>(areaStr);
                
                NearlyAreaModel nAreaModel = new NearlyAreaModel
                {
                    AreaId = Convert.ToInt32(arr[0]),
                    Region = arr[1],
                    City = arr[2],
                    Street = arr[3],
                    ContactaPhone1 = arr[4],
                    Latitude = latit,  // (decimal?)Convert.ToDecimal(arr[5]),
                    Longitude = longit //(decimal?)Convert.ToDecimal(arr[6]),
                };
                Area area = GetAreaByNearlyModel(nAreaModel);
                _repoArea.SaveAreaChanges(area);
                return Convert.ToString(area.AreaID);
            }
            catch (Exception)
            {
                return "false";
            } 
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

        [HttpGet]
        public string SetTime(string id) // id - ID of Area for Start time.  id - ID of Pending for Stop time 
        {
            try
            {
                string idStr = id.Substring(0, id.Length - 1);
                string Signstr = id.Substring(id.Length - 1);

                int myId = Convert.ToInt32(idStr);

                Pending[] isInProgress = _repoPending.Pendings.Where(p => p.Stop == null).ToArray();
                
                if (Signstr == "n")
                {
                    if (isInProgress.Length > 0)
                    {
                        return "busy";
                    }
                    Pending pending = new Pending { PendingID = 0, AreaID = myId, Start = DateTime.Now };
                    _repoPending.AddOrUpdatePending(pending);
                    return pending.PendingID.ToString();
                }
                else if (Signstr == "k")
                {
                    Pending pending = _repoPending.Pendings.Where(p => p.PendingID == myId).First();
                    pending.Stop = DateTime.Now;
                    _repoPending.AddOrUpdatePending(pending);
                    return "ok";
                }
                else
                {
                    return "wrong key";
                }
            }
            catch (Exception)
            {
                return "false";
            }
        }

        [HttpPost]
        public void SetStopTime(string jsonString)
        {
            Pending pending = JsonConvert.DeserializeObject<Pending>(jsonString);
            _repoPending.AddOrUpdatePending(pending);
        }


      


    }
}
