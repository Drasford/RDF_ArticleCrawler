﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeMKVisualizer.Helpers;
using TimeMKVisualizer.Models.ViewModels;

namespace TimeMKVisualizer.Controllers
{
    public class HomeController : Controller
    {
        ListRDFViewModel viewModel = new ListRDFViewModel();

        public ActionResult Index()
        {
            viewModel.lista = RDFHelper.GetSpecificCreator();
            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}