using System;
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


            viewModel.lista = RDFHelper.GetAllObjectsOfSpecificCreator();
            viewModel.creators = RDFHelper.GetAllCreators(viewModel.lista);
            viewModel.coronaArticles = RDFHelper.GetAllCoronaArticles();
            return View(viewModel);
        }

        public ActionResult ShowChart()
        {
            viewModel.lista = RDFHelper.GetAllObjectsOfSpecificCreator();
            viewModel.creators = RDFHelper.GetAllCreators(viewModel.lista);
            viewModel.occurences = RDFHelper.GetNumberOfArticlesPerCreator(viewModel.lista);
            return PartialView("CreatorChart",viewModel);
        }
        public ActionResult ShowCoronaChart()
        {
            viewModel.coronaArticles = RDFHelper.GetAllCoronaArticles();
            viewModel.occurences = RDFHelper.GetNumberOfCoronaArticlesPerCreator(viewModel.coronaArticles);
            return PartialView("CoronaChart", viewModel);
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