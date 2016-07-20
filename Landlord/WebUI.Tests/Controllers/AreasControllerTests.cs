using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Domain.Abstract;
using Domain.Entities;
using System.Collections.Generic;
using WebUI.Controllers;
using System.Linq;
using WebUI.Models.CustomModels;

namespace WebUI.Tests.Controllers
{
    [TestClass]
    public class AreasControllerTests
    {
        [TestMethod]
        public void Can_Paginate()
        {
            Mock<IAreaRepository> mockArea = new Mock<IAreaRepository>();
            Mock<IPhotoRepository> mockPhoto = new Mock<IPhotoRepository>();
            mockArea.Setup(m => m.Areas).Returns(new List<Area>
            {
                new Area { AreaID = 1, ContactaName = "name_1" },
                new Area { AreaID = 2, ContactaName = "name_2" },
                new Area { AreaID = 3, ContactaName = "name_3" },
                new Area { AreaID = 4, ContactaName = "name_4" },
                new Area { AreaID = 5, ContactaName = "name_5" },
                new Area { AreaID = 6, ContactaName = "name_6" },
                new Area { AreaID = 7, ContactaName = "name_7" }
            });

            AreasController controller = new AreasController(mockArea.Object, mockPhoto.Object);
            controller.pageSize = 3;
            AreaListViewModel result = (AreaListViewModel)controller.List(null, 3).Model;
            List<AreaWithPhotos> areas = result.AreasWithPhotosList.ToList();
            

            Assert.IsTrue(areas.Count == 1);
            Assert.AreEqual(areas[0].Area.ContactaName, "name_7");
        }

        [TestMethod]
        public void Can_Filter_By_Region()
        {
            Mock<IAreaRepository> mockArea = new Mock<IAreaRepository>();
            Mock<IPhotoRepository> mockPhoto = new Mock<IPhotoRepository>();
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

            AreasController controller = new AreasController(mockArea.Object, mockPhoto.Object);
            controller.pageSize = 3;
            AreaListViewModel result_1 = (AreaListViewModel)controller.List("region_3", 1).Model;
            AreaListViewModel result_2 = (AreaListViewModel)controller.List("region_3", 2).Model;
            List<AreaWithPhotos> areas_1 = result_1.AreasWithPhotosList.ToList();
            List<AreaWithPhotos> areas_2 = result_2.AreasWithPhotosList.ToList();


            Assert.IsTrue(areas_1.Count == 3);
            Assert.IsTrue(areas_2.Count == 1);
            Assert.AreEqual(areas_1[1].Area.ContactaName, "name_5");
            Assert.AreEqual(areas_2[0].Area.ContactaName, "name_7");
        }

        [TestMethod]
        public void Can_Get_Correct_Count_By_Region()
        {
            Mock<IAreaRepository> mockArea = new Mock<IAreaRepository>();
            Mock<IPhotoRepository> mockPhoto = new Mock<IPhotoRepository>();
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

            AreasController controller = new AreasController(mockArea.Object, mockPhoto.Object);
            controller.pageSize = 3;
            int result_1 = ((AreaListViewModel)controller.List("region_1").Model).PageInfo.TotalItems;
            int result_2 = ((AreaListViewModel)controller.List("region_2").Model).PageInfo.TotalItems;
            int result_3 = ((AreaListViewModel)controller.List("region_3").Model).PageInfo.TotalItems;
            int result_all = ((AreaListViewModel)controller.List(null).Model).PageInfo.TotalItems;

            Assert.AreEqual(result_1, 1);
            Assert.AreEqual(result_2, 2);
            Assert.AreEqual(result_3, 4);
            Assert.AreEqual(result_all, 7);
        }
    }
}
