using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using TimeMKCrawler.Core;
using TimeMKCrawler.RDF;
using System.Threading.Tasks;


namespace TimeMKCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
           
            int pageNumber = 32;
            
            CrawlerController.StartCrawlerAsync();
            //RdfMapper.InitMapper();
          
            Console.ReadLine();
        }

    }
}
