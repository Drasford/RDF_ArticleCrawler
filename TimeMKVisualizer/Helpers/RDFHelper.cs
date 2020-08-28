using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

            UriLoader.Load(g, new Uri("https://www.dropbox.com/s/h53ocqp0vj1tm80/TimeMKArticles.ttl?dl=1"));

            return g;

        }

        public static List<RDFViewModel> GetSpecificCreator()
        {
            IGraph g = LoadTTL();

            IUriNode kanal = g.CreateUriNode(UriFactory.Create("https://www.time.mk/s/kanal5"));
            IUriNode articleCreatorProperty = g.CreateUriNode(UriFactory.Create("https://schema.org/creator"));


            IEnumerable<Triple> list = g.GetTriplesWithPredicateObject(articleCreatorProperty, kanal);

            List<RDFViewModel> creatorsList = new List<RDFViewModel>();

            foreach (var triple in list)
            {
                var temp = new RDFViewModel
                {
                    Subject = triple.Subject.ToString(),
                    Predicate = triple.Predicate.ToString(),
                    Object = triple.Object.ToString(),

                };
                creatorsList.Add(temp);
            }

            return creatorsList;
            
        }
    }
}