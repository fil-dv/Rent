using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Abstract;
using Moq;
using System.Collections.Generic;
using Domain.Entities;
using WebUI.Controllers.API;
using WebUI.Models.API;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using System.Web.Http.Results;
using System.Net;
using System.Linq;

namespace WebUI.Tests.Controllers.API
{
    [TestClass]
    public class CoordControllerTests
    {
        Mock<IAreaRepository> GetData()
        {
            Mock<IAreaRepository> mock = new Mock<IAreaRepository>();

            mock.Setup(m => m.Areas).Returns(new List<Area>
            {
                new Area { AreaID = 1,  Latitude = (decimal)1.111, Longitude = (decimal)1.111 },
                new Area { AreaID = 2,  Latitude = (decimal)2.222, Longitude = (decimal)2.222 },
                new Area { AreaID = 3,  Latitude = (decimal)3.333, Longitude = (decimal)3.333 },
                new Area { AreaID = 4,  Latitude = (decimal)4.444, Longitude = (decimal)4.444 },
                new Area { AreaID = 5,  Latitude = (decimal)5.555, Longitude = (decimal)5.555 },
                new Area { AreaID = 6,  Latitude = (decimal)6.666, Longitude = (decimal)6.666 },
                new Area { AreaID = 7,  Latitude = (decimal)7.777, Longitude = (decimal)7.777 },
                new Area { AreaID = 8,  Latitude = (decimal)8.888, Longitude = (decimal)8.888 },
                new Area { AreaID = 9,  Latitude = (decimal)9.999, Longitude = (decimal)9.999 }
            });
            return mock;
        }

        [TestMethod]
        public void Can_Get_Nearly_List()
        {
            Mock<IAreaRepository> mockArea = GetData();

            CoordController controller = new CoordController(mockArea.Object, null);

            string jsonString_1 = controller.GetNearlyAreas(1, 1);
            List<NearlyAreaModel> result1 = JsonConvert.DeserializeObject<List<NearlyAreaModel>>(jsonString_1);

            string jsonString_2 = controller.GetNearlyAreas(2, 2);
            List<NearlyAreaModel> result2 = JsonConvert.DeserializeObject<List<NearlyAreaModel>>(jsonString_2);

            string jsonString_3 = controller.GetNearlyAreas(9, 9);
            List<NearlyAreaModel> result3 = JsonConvert.DeserializeObject<List<NearlyAreaModel>>(jsonString_3);

            Assert.AreEqual(result1[0].AreaId, 1);
            Assert.AreEqual(result1[1].AreaId, 2);
            Assert.AreEqual(result1[2].AreaId, 3);

            Assert.AreEqual(result2[0].AreaId, 2);
            Assert.AreEqual(result2[1].AreaId, 1);
            Assert.AreEqual(result2[2].AreaId, 3);

            Assert.AreEqual(result3[0].AreaId, 8);
            Assert.AreEqual(result3[1].AreaId, 9);
            Assert.AreEqual(result3[2].AreaId, 7);
        }

        [TestMethod]
        public void Can_Update_Area()
        {
            NearlyAreaModel nAreaM = new NearlyAreaModel
                                        {
                                            AreaId = 7777777,
                                            ContactaPhone1 = "newContactaPhone1",
                                            Region = "newRentAreaAddressRegion",
                                            City = "newRentAreaAddressCity",
                                            Street = "newRentAreaAddressStreet",
                                            Latitude = 100,
                                            Longitude = 300
                                        };

            string jsonStr = JsonConvert.SerializeObject(nAreaM);

            CoordController controller = new CoordController(null, null);

            NearlyAreaModel result = controller.AddOrUpdateAreaForUnitTest(jsonStr);

            Assert.AreEqual(result.AreaId, 7777777);
            Assert.AreEqual(result.Latitude, 100);
            Assert.AreEqual(result.Street, "newRentAreaAddressStreet");
        }

        [TestMethod]
        public void Can_Get_ID_For_New_Area()
        {
            Mock<IAreaRepository> mockArea = GetData();

            NearlyAreaModel nAreaM = new NearlyAreaModel
            {
                ContactaPhone1 = "newContactaPhone1",
                Region = "newRentAreaAddressRegion",
                City = "newRentAreaAddressCity",
                Street = "newRentAreaAddressStreet",
                Latitude = 100,
                Longitude = 300
            };

            string jsonStr = JsonConvert.SerializeObject(nAreaM);

            CoordController controller = new CoordController(mockArea.Object, null);

            int result = controller.GetNewAreaId(jsonStr);

            Assert.AreEqual(result, 0);
        }

        [TestMethod]
        public void Can_Set_Start_Time()
        {
            Mock<IPendingRepository> mock = new Mock<IPendingRepository>();

            mock.Setup(m => m.Pendings).Returns(new List<Pending>
            {
                new Pending { PendingID = 1, AreaID = 1, Start = null, Stop = null },
                new Pending { PendingID = 2, AreaID = 2, Start = null, Stop = null },
                new Pending { PendingID = 3, AreaID = 3, Start = null, Stop = null }
            });

            DateTime dt = DateTime.Now;
            Pending pending = new Pending { AreaID = 4, Start = dt };
            string jsonStr = JsonConvert.SerializeObject(pending);

            CoordController controller = new CoordController(null, mock.Object);

            string result = controller.SetStartTime(jsonStr);
            int res = Convert.ToInt32(result);

            Assert.AreEqual(res, 0);
            Assert.AreEqual(pending.Start, dt);
        }

        [TestMethod]
        public void Can_Set_Stop_Time()
        {
            Mock<IPendingRepository> mock = new Mock<IPendingRepository>();

            DateTime startDate = DateTime.ParseExact("2016-09-01 11:40:52,531", "yyyy-MM-dd HH:mm:ss,fff",
                                       System.Globalization.CultureInfo.InvariantCulture);

            mock.Setup(m => m.Pendings).Returns(new List<Pending>
            {
                new Pending { PendingID = 1, AreaID = 1, Start = startDate, Stop = null },
                new Pending { PendingID = 2, AreaID = 2, Start = startDate, Stop = null },
                new Pending { PendingID = 3, AreaID = 3, Start = startDate, Stop = null }
            });

            DateTime stopDate = DateTime.Now;
            Pending pending = new Pending { PendingID = 3, AreaID = 3, Start = startDate, Stop = stopDate };
            string jsonStr = JsonConvert.SerializeObject(pending);

            CoordController controller = new CoordController(null, mock.Object);

            controller.SetStopTime(jsonStr);

            Assert.AreEqual(pending.PendingID, 3);
            Assert.AreEqual(pending.AreaID, 3);
            Assert.AreEqual(pending.Start, startDate);
            Assert.AreEqual(pending.Stop, stopDate);
        }

    }
}
