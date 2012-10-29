using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteLibrary
{
    public class Listing : IListing
    {
        private String _bookName;
        public String Title
        {
            get { return _bookName; }
            set { _bookName = value; }
        }

        private String _authorName;
        public String Creditor
        {
            get { return _authorName; }
            set { _authorName = value; }
        }

        private String _webPageLink;
        public String WebPageLink
        {
            get { return _webPageLink; }
            set { _webPageLink = value; }
        }

        private String _price;
        public String Price
        {
            get { return _price; }
            set { _price = value; }
        }

        private String _imageLink;
        public String ImageLink
        {
            get { return _imageLink; }
            set { _imageLink = value; }
        }

        //Construtor: 
        public Listing(string getTitle, string getAuthor, string getPrice, string getImageUrl, string getBookWebpageUrl)
        {
            Title = getTitle;
            Creditor = getAuthor;
            Price = getPrice;
            WebPageLink = getBookWebpageUrl;
            ImageLink = getImageUrl;
        }
        public override string ToString()
        {
            return "Title: " + Title +
                    "\nAuthor: " + Creditor +
                    "\nPrice: " + Price +
                    "\nImage URL: " + ImageLink +
                    "\nWeb Page URL: " + WebPageLink + "\n";
        }
    }
}
