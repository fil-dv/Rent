using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Entities;
using System.Linq;
using System.Collections.Generic;

namespace WebUI.Tests.Domain
{
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void Can_Add_New_Item()
        {
            //организация
            Area area_1 = new Area { AreaID = 1, ContactaName = "name_1" };
            Area area_2 = new Area { AreaID = 2, ContactaName = "name_2" };
            Area area_3 = new Area { AreaID = 3, ContactaName = "name_3" };

            Cart cart = new Cart();

            //действия
            cart.AddItem(area_1);
            cart.AddItem(area_2);
            cart.AddItem(area_3);

            List<CartLine> result = cart.Lines.ToList();

            //утверждения
            Assert.AreEqual(result.Count(), 3);
            Assert.AreEqual(result[0].Area, area_1);
            Assert.AreEqual(result[1].Area, area_2);
            Assert.AreEqual(result[2].Area, area_3);
        }

        [TestMethod]
        public void Can_Add_Existing_Item()
        {
            //организация
            Area area_1 = new Area { AreaID = 1, ContactaName = "name_1" };
            
            Cart cart = new Cart();

            //действия
            bool result_1 = cart.AddItem(area_1);
            bool result_2 = cart.AddItem(area_1);            

            //утверждения
            Assert.AreEqual(result_1, true);
            Assert.AreEqual(result_2, false);
        }

        [TestMethod]
        public void Can_Remove_Item()
        {
            //организация
            Area area_1 = new Area { AreaID = 1, ContactaName = "name_1" };
            Area area_2 = new Area { AreaID = 2, ContactaName = "name_2" };
            Area area_3 = new Area { AreaID = 3, ContactaName = "name_3" };

            Cart cart = new Cart();

            //действия
            cart.AddItem(area_1);
            cart.AddItem(area_1);
            cart.AddItem(area_2);
            cart.AddItem(area_2);
            cart.AddItem(area_3);
            cart.AddItem(area_3);

            cart.RemoveLine(area_3);

            List<CartLine> result = cart.Lines.ToList();

            //утверждения
            Assert.AreEqual(result.Where(a=>a.Area == area_3).Count(), 0);
            Assert.AreEqual(result.Count(), 2);
        }

    }
}
