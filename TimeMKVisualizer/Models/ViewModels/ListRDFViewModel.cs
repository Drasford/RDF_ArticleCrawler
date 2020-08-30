using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeMKVisualizer.Models.ViewModels
{
    public class ListRDFViewModel
    {
        public List<RDFViewModel> lista = new List<RDFViewModel>();
        public List<CreatorModel> creators = new List<CreatorModel>();
        public List<CoronaModel> coronaArticles = new List<CoronaModel>(); 
        public List<string> occurences = new List<string>();
    }
}