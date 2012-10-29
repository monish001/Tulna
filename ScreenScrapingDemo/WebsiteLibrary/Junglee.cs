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
using HtmlAgilityPack;

namespace WebsiteLibrary
{
    public class Junglee
    {

        public static string generateUrl(string text)
        {
            return "http://www.junglee.com/mn/search/junglee/ref=nav_sb_noss?url=search-alias%3Dstripbooks&p_87=1&field-keywords=" + text.Replace(" ", "+");
        }

        public static List<Listing> getTop10Listings(string htmlDocString)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(htmlDocString);

            List<Listing> list = new List<Listing>();
            IEnumerable<HtmlNode> nodelist = doc.DocumentNode.Descendants("div").ToArray();//.Where(div => div.GetAttributeValue("class", "")=="fk-srch-item fk-inf-scroll-item");
            if (nodelist != null)
                foreach (HtmlNode node in nodelist)//("//div[@class=\"fk-srch-item fk-inf-scroll-item\"]")
                {
                    if (node.GetAttributeValue("class", "") != "result product")
                        continue;
                    Listing aListing = new Listing(getTitle(node), getAuthor(node), getPrice(node), getImageUrl(node), getBookWebpageUrl(node));
                    //Console.WriteLine(aListing.ToString());
                    list.Add(aListing);
                }
 
            return list;
        }

        private static string getTitle(HtmlNode node)
        {
            IEnumerable<HtmlNode> nodeTitle = node.Descendants("a");//(".//a[@class=\"fk-srch-title-text fksd-bodytext\"]");
            foreach (HtmlNode aNode in nodeTitle)
            {
                if (aNode.GetAttributeValue("class", "") == "title")
                    return aNode.InnerText;
            }
            return "";
        }
        public static string getImageUrl(HtmlNode node)
        {
            IEnumerable<HtmlNode> listingNode = node.Descendants("div");// (".//div[@class=\"lastUnit rposition\"]").First().Element("a").Element("img");
            foreach (HtmlNode aNode in listingNode)
            {
                if (aNode.GetAttributeValue("class", "") == "image")
                {
                    return aNode.Element("a").Element("img").GetAttributeValue("src", "");
                }
            }
            return "";
        }
        public static string getPrice(HtmlNode node)
        {
            IEnumerable<HtmlNode> priceNode = node.Descendants("span");//(".//b[@class=\"fksd-bodytext price final-price\"]");
            foreach (HtmlNode aNode in priceNode)
            {
                if (aNode.GetAttributeValue("class", "") == "price")
                {
                    return aNode.InnerText;
                }
            }
            return "";
        }
        public static string getBookWebpageUrl(HtmlNode node)
        {
            IEnumerable<HtmlNode> nodeTitle = node.Descendants("a");//(".//a[@class=\"fk-srch-title-text fksd-bodytext\"]");
            foreach (HtmlNode aNode in nodeTitle)
            {
                if (aNode.GetAttributeValue("class", "") == "title")
                    return aNode.GetAttributeValue("href", "");
            }
            return "";
        }

        public static string getAuthor(HtmlNode node)
        {
            IEnumerable<HtmlNode> nodeAuthor = node.Descendants("span");// (".//span[@class=\"fk-item-authorinfo-text fksd-smalltext\"]");
            string author = "";
            foreach (HtmlNode aNode in nodeAuthor)
            {
                if (aNode.GetAttributeValue("class", "") == "ptBrand")
                    return aNode.InnerText.Substring(3);
                }
            return author;
        }
    }
}
