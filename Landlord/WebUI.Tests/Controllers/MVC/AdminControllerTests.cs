using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Abstract;
using Moq;
using Domain.Entities;
using System.Collections.Generic;
using WebUI.Controllers;
using WebUI.Models.CustomModels;
using System.Linq;
using System.Web.Http;

namespace WebUI.Tests.Controllers.MVC
{
    [TestClass]
    public class AdminControllerTests
    {
        Mock<IAreaRepository> GetData()
        {
            Mock<IAreaRepository> mockArea = new Mock<IAreaRepository>();

            mockArea.Setup(m => m.Areas).Returns(new List<Area>
            {
                new Area {AreaID = 1, MonthPrice = 100, ContactaName = "name_1"},
                new Area {AreaID = 2, MonthPrice = 200, ContactaName = "name_2"},
                new Area {AreaID = 3, MonthPrice = 300, ContactaName = "name_3"},
                new Area {AreaID = 4, MonthPrice = 400, ContactaName = "name_4"},
                new Area {AreaID = 5, MonthPrice = 500, ContactaName = "name_5"},
                new Area {AreaID = 6, MonthPrice = 600, ContactaName = "name_6"},
                new Area {AreaID = 7, MonthPrice = 700, ContactaName = "name_7"}                
            });
            return mockArea;
        }

        [TestMethod]
        public void Can_Index_Return_All_Areas()
        {
            Mock<IAreaRepository> mockArea = GetData();
            Mock<IPhotoRepository> mockPhoto = new Mock<IPhotoRepository>();
            
            AdminController controller = new AdminController(mockArea.Object, mockPhoto.Object, null);
            List<AreaWithPhotos> result = ((IEnumerable<AreaWithPhotos>)controller.Index().ViewData.Model).ToList();

            Assert.IsTrue(result.Count == 7);
            Assert.AreEqual(result[0].Area.ContactaName, "name_1");
            Assert.AreEqual(result[1].Area.ContactaName, "name_2");
            Assert.AreEqual(result[6].Area.ContactaName, "name_7");
        }

        [TestMethod]
        public void Can_Edit()
        {
            Mock<IAreaRepository> mock = GetData();
            Mock<IPhotoRepository> mockPhoto = new Mock<IPhotoRepository>();
            mockPhoto.Setup(m => m.Photos).Returns(new List<Photo>
            {
                new Photo {AreaID = 1 }
            });
            AdminController controller = new AdminController(mock.Object, mockPhoto.Object, null);

            AreaWithPhotos area1 = controller.Edit(1).ViewData.Model as AreaWithPhotos;
            AreaWithPhotos area2 = controller.Edit(2).ViewData.Model as AreaWithPhotos;
            AreaWithPhotos area3 = controller.Edit(3).ViewData.Model as AreaWithPhotos;

            Assert.AreEqual(area1.Area.AreaID, 1);
            Assert.AreEqual(area2.Area.AreaID, 2);
            Assert.AreEqual(area3.Area.AreaID, 3);
        }
    }
}
