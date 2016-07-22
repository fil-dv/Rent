﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Infrastructure
{
    public class CartModelBinder : IModelBinder
    {
        const string sessionKey = "cart";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Cart cart = null;

            if (controllerContext.HttpContext.Session[sessionKey] != null)
            {
                cart = (Cart)controllerContext.HttpContext.Session[sessionKey];
            }

            if (cart == null)
            {
                cart = new Cart();
                controllerContext.HttpContext.Session[sessionKey] = cart;
            }
            return cart;
        }
    }
}