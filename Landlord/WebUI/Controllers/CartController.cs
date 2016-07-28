using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models.CustomModels;

namespace WebUI.Controllers
{
    public class CartController : Controller
    {
        IAreaRepository _repoArea;
        IOrderProcessor _orderProcessor;

        public CartController(IAreaRepository repository, IOrderProcessor orderProcessor)
        {
            _repoArea = repository;
            _orderProcessor = orderProcessor;
        }
        
        public RedirectToRouteResult AddToCart(Cart cart, int areaId, string returnUrl)
        {
            Area area = _repoArea.Areas.FirstOrDefault(a => a.AreaID == areaId);
            if (area != null)
            {
                cart.AddItem(area);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, Area ar, string returnUrl)
        {
            Area area = _repoArea.Areas.FirstOrDefault(a => a.AreaID == ar.AreaID);
            if (area != null)
            {
                cart.RemoveLine(area);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ActionResult CheckOut()
        {
            bool isLoggedIn = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            string userName = System.Web.HttpContext.Current.User.Identity.Name;
            if (!isLoggedIn)
            {
                //RedirectToAction("Login", "AccountController");
                return RedirectToAction("Login", "Account");
            }
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult CheckOut(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Извините, корзина пуста.");
            }

            if (ModelState.IsValid)
            {
                _orderProcessor.ProcessorOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(new ShippingDetails());
            }
        }
    }
}