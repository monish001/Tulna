using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using HtmlAgilityPack;namespace WebsiteLibrary
{
    public static class Flipkart
    {
        public static List<Listing> getTop10Listings(string htmlDocString)
        {

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(htmlDocString);

            List<Listing> list = new List<Listing>();
            IEnumerable<HtmlNode> nodelist = doc.DocumentNode.Descendants("div").ToArray();//.Where(div => div.GetAttributeValue("class", "")=="fk-srch-item fk-inf-scroll-item");
            if (nodelist != null)
                foreach (HtmlNode node in nodelist)//("//div[@class=\"fk-srch-item fk-inf-scroll-item\"]")
                {
                    if (node.GetAttributeValue("class", "") != "fk-srch-item fk-inf-scroll-item")
                        continue;
                    Listing aListing = new Listing(getTitle(node), getAuthor(node), getPrice(node), getImageUrl(node), getBookWebpageUrl(node));
                    //Console.WriteLine(aListing.ToString());
                    list.Add(aListing);
                }
            return list;
        }
        public static string generateUrl(string text)
        {
            return "http://www.flipkart.com/search/a/books?query=" + text.Replace(" ", "+");
        }
        private static string getTitle(HtmlNode node)
        {
            IEnumerable<HtmlNode> nodeTitle = node.Descendants("a");//(".//a[@class=\"fk-srch-title-text fksd-bodytext\"]");
            foreach (HtmlNode aNode in nodeTitle)
            {
                if (aNode.GetAttributeValue("class", "") == "fk-srch-title-text fksd-bodytext")
                    return aNode.InnerText;
            }
            return "";
        }
        public static string getImageUrl(HtmlNode node)
        {
            IEnumerable<HtmlNode> listingNode = node.Descendants("div");// (".//div[@class=\"lastUnit rposition\"]").First().Element("a").Element("img");
            foreach (HtmlNode aNode in listingNode)
            {
                if (aNode.GetAttributeValue("class", "") == "lastUnit rposition")
                {
                    return aNode.Element("a").Element("img").GetAttributeValue("data-src", "");
                }
            }
            return "";
        }
        public static string getPrice(HtmlNode node)
        {
            IEnumerable<HtmlNode> priceNode = node.Descendants("b");//(".//b[@class=\"fksd-bodytext price final-price\"]");
            foreach (HtmlNode aNode in priceNode)
            {
                if (aNode.GetAttributeValue("class", "") == "fksd-bodytext price final-price")
                {
                    return aNode.InnerText;
                }
            }
            return "";
        }
        public static string getBookWebpageUrl(HtmlNode node)
        {
            IEnumerable<HtmlNode> nodeTitle = node.Descendants("a");// (".//a[@class=\"fk-srch-title-text fksd-bodytext\"]");
            foreach (HtmlNode aNode in nodeTitle)
                if (aNode.GetAttributeValue("class", "") == "fk-srch-title-text fksd-bodytext")
                {
                    Uri baseUri = new Uri("http://www.flipkart.com", UriKind.Absolute);
                    Uri webpage = new Uri(baseUri, aNode.GetAttributeValue("href", ""));
                    return webpage.ToString();

                }
            return "";
        }

        public static string getAuthor(HtmlNode node)
        {
            IEnumerable<HtmlNode> nodeAuthor = node.Descendants("span");// (".//span[@class=\"fk-item-authorinfo-text fksd-smalltext\"]");
            string author = "";
            foreach (HtmlNode aNode in nodeAuthor)
            {
                if (aNode.GetAttributeValue("class", "") == "fk-item-authorinfo-text fksd-smalltext" && aNode.Elements("a") != null)
                    foreach (HtmlNode authorNode in aNode.Elements("a"))
                        author += authorNode.InnerText + ".\t";
            }
            return author;
        }
    }
}
