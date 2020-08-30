using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeMKVisualizer.Models;
using TimeMKVisualizer.Models.ViewModels;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Writing;

namespace TimeMKVisualizer.Helpers
{
    public static class RDFHelper
    {
        public static IGraph LoadTTL()
        {
            IGraph g = new Graph();

            TurtleParser turtleParser = new TurtleParser();

            UriLoader.Load(g, new Uri("https://www.dropbox.com/s/h53ocqp0vj1tm80/TimeMKArticles.ttl?dl=1"),turtleParser);

            return g;

        }

  

        public static string LinkCutter(string link)
        {
           var cut =  link.Replace("https://www.time.mk/s/", "");
            return cut;
        }


        public static List<string> GetNumberOfCoronaArticlesPerCreator(List<CoronaModel> coronaArticles)
        {
            List<string> listOfCoronaArticleOccuranecsPerCreator = new List<string>();
            var grouping = coronaArticles.GroupBy(i => i.CoronaCreator.CreatorName);
            foreach (var item in grouping)
            {
                listOfCoronaArticleOccuranecsPerCreator.Add(item.Count().ToString());
            }

            return listOfCoronaArticleOccuranecsPerCreator;
        }
       

        public static List<string> GetNumberOfArticlesPerCreator(List<RDFViewModel> creatorTriples)
        {

            List<string> listOfArticleOccuranecsPerCreator = new List<string>();
            var grouping = creatorTriples.GroupBy(i => i.Object);

            foreach (var item in grouping)
            {
                listOfArticleOccuranecsPerCreator.Add(item.Count().ToString());
            }
            return listOfArticleOccuranecsPerCreator;
        }

        public static List<CreatorModel> GetAllCreators(List<RDFViewModel> creatorTriples)
        {
            List<CreatorModel> creators = new List<CreatorModel>();
            foreach (var item in creatorTriples)
            {
                var temp = new CreatorModel
                {
                    CreatorName = LinkCutter(item.Object.ToString()),
                    ArticleLink = item.Subject.ToString()

                };
                creators.Add(temp);
            }
            return creators;
        }

        public static Boolean ArticleContainsCorona(string articleHeadline)
        {
            bool containsKovid = articleHeadline.IndexOf("Ковид", StringComparison.OrdinalIgnoreCase) >= 0;
            bool containsKorona = articleHeadline.IndexOf("корона", StringComparison.OrdinalIgnoreCase) >= 0;
            return containsKovid|| containsKorona;
        }

        public static List<CoronaModel> GetAllCoronaArticles()
        {
            IGraph g = LoadTTL();
            
            IUriNode articleHeadlineProperty = g.CreateUriNode(UriFactory.Create("https://schema.org/headline"));
            
            IEnumerable<Triple> triplesByHeadline = g.GetTriplesWithPredicate(articleHeadlineProperty);

            List<CoronaModel> coronaModelList = new List<CoronaModel>();

            List<RDFViewModel> triplesWithCreatorPredicate = GetAllObjectsOfSpecificCreator();
            List<CreatorModel> creators = GetAllCreators(triplesWithCreatorPredicate);

            foreach (var item in triplesByHeadline)
            {
                if (ArticleContainsCorona(item.Object.ToString()))
                {
                    var tmp = new CoronaModel
                    {
                        CoronaArticleLink = item.Subject.ToString(),
                        CoronaArticleText = item.Object.ToString()
                    };
                    coronaModelList.Add(tmp);
                }
               
            }

           // var matchList = creators.Select(x => x.ArticleLink).Intersect(coronaModelList.Select(y => y.CoronaArticleLink)).ToList();

            var FINAL =
                from c in creators
                from aranga in coronaModelList
                where c.ArticleLink == aranga.CoronaArticleLink
                select new CoronaModel{ CoronaArticleText = aranga.CoronaArticleText, CoronaArticleLink=aranga.CoronaArticleLink, CoronaCreator=c};

            var FINALFINAL = FINAL.ToList();
             

            return FINALFINAL;
           
        }

        public static List<RDFViewModel> GetAllObjectsOfSpecificCreator()
        {
            IGraph g = LoadTTL();
            IUriNode articleCreatorProperty = g.CreateUriNode(UriFactory.Create("https://schema.org/creator"));

            IEnumerable<Triple> triplesByCreator = g.GetTriplesWithPredicate(articleCreatorProperty);
            List<RDFViewModel> triplesWithCreatorPredicate = new List<RDFViewModel>();


            foreach (var triple in triplesByCreator)
            {
                var temp = new RDFViewModel
                {
                    Subject = triple.Subject.ToString(),
                    Predicate = triple.Predicate.ToString(),
                    Object = triple.Object.ToString(),

                };
                triplesWithCreatorPredicate.Add(temp);
            }
            return triplesWithCreatorPredicate;

        }

      
    }
}