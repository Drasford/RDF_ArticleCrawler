using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace TimeMKCrawler.Core
{
    public class CrawlerController
    {

         public static List<Article> GetAllArticles()
        {
            List<Article> articles = new List<Article>();

            using(StreamReader sr = new StreamReader("C:\\Users\\Dame\\Desktop\\RDF\\RDF_ArticleCrawler-master\\TimeMKCrawler\\Data\\Articles.json"))
            {
                string json = sr.ReadToEnd();
                articles = JsonConvert.DeserializeObject<List<Article>>(json);
            }

            return articles;
        }


        

        public static async Task StartCrawlerAsync()
        {
            
            var url = "https://time.mk/week/2020/33";


            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var div_articles = htmlDocument.DocumentNode.Descendants("div")
                         .Where(node => node.GetAttributeValue("class", "")
                         .Equals("cluster")).ToList();

            List<Article> articles = new List<Article>();

            await Task.Run( () => articles = GetAllArticles());


            


            foreach (var div_article in div_articles)
            {

                var tmpCreator = div_article?.Descendants("h2")?.FirstOrDefault()?.InnerText;
                int index = tmpCreator.IndexOf("-");
                tmpCreator = tmpCreator.Substring(0, index);

                var article = new Article
                {
                    title = div_article?.Descendants("h1")?.FirstOrDefault()?.InnerText,
                    text = div_article?.Descendants("p")?.FirstOrDefault()?.InnerText,
                    link = "https://www.time.mk/" + div_article?.Descendants("a")?.FirstOrDefault()?.ChildAttributes("href").FirstOrDefault()?.Value,
                    dateCreated = div_article?.Descendants("span")?.FirstOrDefault()?.InnerText,
                    creator = "https://www.time.mk/" + div_article?.Descendants("h2")?.FirstOrDefault()?.Descendants("a")?.FirstOrDefault()?.ChildAttributes("href").FirstOrDefault()?.Value,
                };
                articles.Add(article);
            }




            JsonSerializer serializer = new JsonSerializer();

            using (StreamWriter sw = new StreamWriter("C:\\Users\\Dame\\Desktop\\RDF\\RDF_ArticleCrawler-master\\TimeMKCrawler\\Data\\Articles.json"))
            {
                using (JsonWriter jw = new JsonTextWriter(sw))
                {
                    serializer.Serialize(jw, articles);
                }
            }

        }

       
    }
}
