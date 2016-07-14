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
    public class AreasControllerTests
    {
        [TestMethod]
        public void CanPaginate()
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
            IEnumerable<Area> result = (IEnumerable<Area>)controller.List(3).Model;
            List<Area> areas = result.ToList();

            Assert.IsTrue(areas.Count == 1);
            Assert.AreEqual(areas[0].ContactaName, "name_7");
        }
    }
}
