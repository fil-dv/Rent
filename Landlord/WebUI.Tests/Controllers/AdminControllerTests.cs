using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Abstract;
using Moq;
using Domain.Entities;
using System.Collections.Generic;
using WebUI.Controllers;
using WebUI.Models.CustomModels;
using System.Linq;

namespace WebUI.Tests.Controllers
{
    [TestClass]
    public class AdminControllerTests
    {
        [TestMethod]
        public void Can_Index_Return_All_Areas()
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

            AdminController controller = new AdminController(mockArea.Object, mockPhoto.Object);
            List<AreaWithPhotos> result = ((IEnumerable<AreaWithPhotos>)controller.Index().ViewData.Model).ToList();
           


            Assert.IsTrue(result.Count == 7);
            Assert.AreEqual(result[0].Area.ContactaName, "name_1");
            Assert.AreEqual(result[1].Area.ContactaName, "name_2");
            Assert.AreEqual(result[6].Area.ContactaName, "name_7");
        }
    }
}
