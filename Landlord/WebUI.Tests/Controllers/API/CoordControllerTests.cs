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

            CoordController controller = new CoordController(mockArea.Object);

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
        public void Can_Add_New_Area()
        {
            Mock<IAreaRepository> mock = new Mock<IAreaRepository>();

            mock.Setup(m => m.Areas).Returns(new List<Area>
            {
                new Area
                {
                    AreaID = 7777777,
                    AreaTypeID = 1,
                    AreaDescription = "newArea",
                    OwnerName = "newOwnerName",
                    ContactaName = "newContactaName",
                    ContactaPhone1 = "newContactaPhone1",
                    LegalAddressRegion = "newLegalAddressRegion",
                    LegalAddressCity = "newLegalAddressCity",
                    LegalAddressStreet = "newLegalAddressStreet",
                    RentAreaAddressRegion = "newRentAreaAddressRegion",
                    RentAreaAddressCity = "newRentAreaAddressCity",
                    RentAreaAddressStreet = "newRentAreaAddressStreet",
                    SquareArea = 333,
                    MonthPrice = 777,
                    IsAvailable = true,
                    Rating = 0,
                    Latitude = 100,
                    Longitude = 300
                }
            });

            CoordController controller = new CoordController(mock.Object);
            Area area = (Area)(mock.Object.Areas.Where(m => m.AreaID == 7777777).ToList()[0]);
            
            string jsonStr = JsonConvert.SerializeObject(area);

            Area result = controller.AddOrUpdateAreaForUnitTest(jsonStr);

            Assert.AreEqual(result.AreaID, 7777777);
            Assert.AreEqual(result.Latitude, 100);
            Assert.AreEqual(result.RentAreaAddressStreet, "newRentAreaAddressStreet");
        }
    }
}
