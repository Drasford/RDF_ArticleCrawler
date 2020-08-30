using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeMKVisualizer.Models
{
    public class CoronaModel
    {
        public string CoronaArticleText { get; set; }

        public string CoronaArticleLink { get; set; }

        public CreatorModel CoronaCreator = new CreatorModel();
    }
}