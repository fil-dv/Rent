using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Abstract;
using Moq;
using System.Collections.Generic;
using Domain.Entities;
using WebUI.Controllers.API;

namespace WebUI.Tests.Controllers.API
{
    [TestClass]
    public class CoordControllerTests
    {
        Mock<IAreaRepository> GetData()
        {
            Mock<IAreaRepository> mockArea = new Mock<IAreaRepository>();

            mockArea.Setup(m => m.Areas).Returns(new List<Area>
            {
                new Area
                {
                    AreaID = 1,
                    RentAreaAddressRegion = "region_1",
                    RentAreaAddressCity = "city_1",
                    RentAreaAddressStreet = "street_1",
                    Latitude = null,
                    Longitude = null
                }              
            });
            return mockArea;
        }

        [TestMethod]
        public void Can_Build_Correct_Address()
        {
            //Mock<IAreaRepository> mockArea = GetData();
            //CoordController controller = new CoordController(mockArea.Object);

            //controller.GetCoordByAddressForAll();
            //IEnumerable<Area> list = mockArea.Where(r => r.Latitude == null || r.Longitude == null);
            //string address = mockArea[0].RentAreaAddressRegion + " обл., " + item.RentAreaAddressCity + ", " + item.RentAreaAddressStreet; 


        }
    }
}
