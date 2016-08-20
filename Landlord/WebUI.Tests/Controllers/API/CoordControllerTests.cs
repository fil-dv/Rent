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

            JsonResult<List<NearlyAreaModel>> result1 = controller.GetNearlyAreas(1, 1);
            JsonResult<List<NearlyAreaModel>> result2 = controller.GetNearlyAreas(2, 2);
            JsonResult<List<NearlyAreaModel>> result3 = controller.GetNearlyAreas(9, 9);

            Assert.AreEqual(result1.Content[0].AreaId, 1);
            Assert.AreEqual(result1.Content[1].AreaId, 2);
            Assert.AreEqual(result1.Content[2].AreaId, 3);

            Assert.AreEqual(result2.Content[0].AreaId, 2);
            Assert.AreEqual(result2.Content[1].AreaId, 1);
            Assert.AreEqual(result2.Content[2].AreaId, 3);

            Assert.AreEqual(result3.Content[0].AreaId, 8);
            Assert.AreEqual(result3.Content[1].AreaId, 9);
            Assert.AreEqual(result3.Content[2].AreaId, 7);

        }
    }
}
