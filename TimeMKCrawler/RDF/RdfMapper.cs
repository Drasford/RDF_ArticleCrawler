using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeMKCrawler.Core;
using VDS.RDF;
using VDS.RDF.Writing;

namespace TimeMKCrawler.RDF
{
    public class RdfMapper
    {

        public static void InitMapper()
        {

            Console.OutputEncoding = Encoding.UTF8;

            List<Article> articles = CrawlerController.GetAllArticles();


            IGraph g = new Graph();

            foreach (var article in articles)
            {

                //Encoding encode = Encoding.GetEncoding(1253);

                //byte[] bytesForTitle = Encoding.UTF32.GetBytes(article.title);

                //var test = encode.GetString(bytesForTitle);

              


                    //Resources
                    IUriNode articleLinkToArticleResource = g.CreateUriNode(UriFactory.Create(article.link));
                    IUriNode articleCreatorResource = g.CreateUriNode(UriFactory.Create(article.creator));
                    IUriNode articleResource = g.CreateUriNode(UriFactory.Create("https://schema.org/Article"));
                    //Literals
                    ILiteralNode articleDateCreatedLiteral = g.CreateLiteralNode(article.dateCreated);
                    ILiteralNode articleTextLiteral = g.CreateLiteralNode(article.text);
                    ILiteralNode articleTitleLiteral = g.CreateLiteralNode(article.title);
                    //Properties

                    IUriNode rdfType = g.CreateUriNode("rdf:type");
                    IUriNode articleCreatorProperty = g.CreateUriNode(UriFactory.Create("https://schema.org/creator"));
                    IUriNode articleHeadlineProperty = g.CreateUriNode(UriFactory.Create("https://schema.org/headline"));
                    IUriNode articleBodyProperty = g.CreateUriNode(UriFactory.Create("https://schema.org/articleBody"));
                    IUriNode articleDateCreatedProperty = g.CreateUriNode(UriFactory.Create("https://schema.org/dateCreated"));





                    g.Assert(new Triple(articleLinkToArticleResource, rdfType, articleResource));
                    g.Assert(new Triple(articleLinkToArticleResource, articleCreatorProperty, articleCreatorResource));
                    g.Assert(new Triple(articleLinkToArticleResource, articleDateCreatedProperty, articleDateCreatedLiteral));
                    g.Assert(new Triple(articleLinkToArticleResource, articleHeadlineProperty, articleTitleLiteral));
                    g.Assert(new Triple(articleLinkToArticleResource, articleBodyProperty, articleTextLiteral));



                } 

                foreach (Triple t in g.Triples)
                {
        
                    Console.WriteLine(t.ToString());
                }

            

                TurtleWriter ttlWriter = new TurtleWriter();
                ttlWriter.Save(g, "C:\\Users\\Dame\\source\\repos\\TimeMKCrawler\\TimeMKCrawler\\Data\\TimeMKArticles.ttl");
                Console.ReadLine();

            }


        }
    }
