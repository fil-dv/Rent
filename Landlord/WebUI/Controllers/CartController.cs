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

    }
}