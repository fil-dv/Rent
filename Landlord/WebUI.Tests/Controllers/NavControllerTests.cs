using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Domain.Abstract;
using Domain.Entities;
using System.Collections.Generic;
using WebUI.Controllers;
using System.Linq;

namespace WebUI.Tests.Controllers
{
    [TestClass]
    public class NavControllerTests
    {
        [TestMethod]
        public void Can_Create_Regions_List()
        {
            //организация

            Mock<IAreaRepository> mockArea = new Mock<IAreaRepository>();
            
            mockArea.Setup(m => m.Areas).Returns(new List<Area>
            {
                new Area { AreaID = 1, RentAreaAddressRegion = "region_1", ContactaName = "name_1" },
                new Area { AreaID = 2, RentAreaAddressRegion = "region_2", ContactaName = "name_2" },
                new Area { AreaID = 3, RentAreaAddressRegion = "region_2", ContactaName = "name_3" },
                new Area { AreaID = 4, RentAreaAddressRegion = "region_3", ContactaName = "name_4" },
                new Area { AreaID = 5, RentAreaAddressRegion = "region_3", ContactaName = "name_5" },
                new Area { AreaID = 6, RentAreaAddressRegion = "region_3", ContactaName = "name_6" },
                new Area { AreaID = 7, RentAreaAddressRegion = "region_3", ContactaName = "name_7" }
            });

            NavController controller = new NavController(mockArea.Object);

            // действие

            List<string> result = ((IEnumerable<string>)controller.Menu().Model).ToList();

            // утверждения

            Assert.IsTrue(result[0] == "region_1");
            Assert.IsTrue(result[1] == "region_2");
            Assert.IsTrue(result[2] == "region_3");
        }

        [TestMethod]
        public void Can_Indicate_Selected_Region()
        {
            //организация

            Mock<IAreaRepository> mockArea = new Mock<IAreaRepository>();

            mockArea.Setup(m => m.Areas).Returns(new List<Area>
            {
                new Area { AreaID = 1, RentAreaAddressRegion = "region_1", ContactaName = "name_1" },
                new Area { AreaID = 2, RentAreaAddressRegion = "region_2", ContactaName = "name_2" },
                new Area { AreaID = 3, RentAreaAddressRegion = "region_2", ContactaName = "name_3" },
                new Area { AreaID = 4, RentAreaAddressRegion = "region_3", ContactaName = "name_4" },
                new Area { AreaID = 5, RentAreaAddressRegion = "region_3", ContactaName = "name_5" },
                new Area { AreaID = 6, RentAreaAddressRegion = "region_3", ContactaName = "name_6" },
                new Area { AreaID = 7, RentAreaAddressRegion = "region_3", ContactaName = "name_7" }
            });

            NavController controller = new NavController(mockArea.Object);
            string selectedRegion = "region_3";

            // действие

            string result = controller.Menu(selectedRegion).ViewBag.SelectedRegion;

            // утверждения

            Assert.AreEqual(result, selectedRegion);            
        }
    }
}
