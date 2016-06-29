using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class AreasController : Controller
    {
        IAreaRepository _repository;

        public AreasController(IAreaRepository repository)
        {
            _repository = repository;
        }

        public ViewResult List()
        {
            return View(_repository.Areas);
        }
    }
}