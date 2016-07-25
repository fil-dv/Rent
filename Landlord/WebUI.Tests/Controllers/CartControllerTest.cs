using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Abstract;
using Moq;
using Domain.Entities;
using System.Collections.Generic;
using WebUI.Controllers;
using System.Linq;
using System.Web.Mvc;
using WebUI.Models.CustomModels;

namespace WebUI.Tests.Controllers
{
    [TestClass]
    public class CartControllerTest
    {
        Mock<IAreaRepository> GetData()
        {
            Mock<IAreaRepository> mockArea = new Mock<IAreaRepository>();
            
            mockArea.Setup(m => m.Areas).Returns(new List<Area>
            {
                new Area {AreaID = 1, MonthPrice = 100},
                new Area {AreaID = 2, MonthPrice = 200},
                new Area {AreaID = 3, MonthPrice = 300},
                new Area {AreaID = 4, MonthPrice = 400},
                new Area {AreaID = 5, MonthPrice = 500},
                new Area {AreaID = 6, MonthPrice = 600},
                new Area {AreaID = 7, MonthPrice = 700},
                new Area {AreaID = 8, MonthPrice = 800},
                new Area {AreaID = 9, MonthPrice = 900}
            });
            return mockArea;
        }

        [TestMethod]
        public void Can_Add_To_Cart_Use_ModelBinder()
        {
            //arrange
            Mock<IAreaRepository> mockArea = GetData();
            Mock<IPhotoRepository> mockPhoto = new Mock<IPhotoRepository>();
            CartController controller = new CartController(mockArea.Object);
            Cart cart = new Cart();

            //act
            controller.AddToCart(cart, 1, null);

            //assert
            Assert.AreEqual(cart.Lines.Count(), 1);
            Assert.IsTrue(cart.Lines.ToList()[0].Area.AreaID == 1);
        }

        [TestMethod]
        public void Can_Build_Correct_Url_After_Add_To_Cart()
        {
            //arrange
            Mock<IAreaRepository> mock = GetData();
            CartController controller = new CartController(mock.Object);
            Cart cart = new Cart();

            //act
            RedirectToRouteResult result = controller.AddToCart(cart, 1, "myUrl");

            //assert
            Assert.AreEqual(result.RouteValues["Action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], "myUrl");
        }

        [TestMethod]
        public void Can_Method_Index_Build_Right_Url()
        {
            //arrange
            CartController controller = new CartController(null);
            Cart cart = new Cart();

            //act
            CartViewModel result = (CartViewModel)controller.Index(cart, "myUrl").ViewData.Model;

            //assert
            Assert.AreSame(result.Cart, cart);
            Assert.AreEqual(result.ReturnUrl, "myUrl");
        }
    }
}
