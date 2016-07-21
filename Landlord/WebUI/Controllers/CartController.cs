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

        public CartController(IAreaRepository repository)
        {
            _repoArea = repository;
        }

        public Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        public RedirectToRouteResult AddToCart(int areaId, string returnUrl)
        {
            Area area = _repoArea.Areas.FirstOrDefault(a => a.AreaID == areaId);
            if (area != null)
            {
                GetCart().AddItem(area);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Area ar, string returnUrl)
        {
            Area area = _repoArea.Areas.FirstOrDefault(a => a.AreaID == ar.AreaID);
            if (area != null)
            {
                GetCart().RemoveLine(area);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });
        }

    }
}