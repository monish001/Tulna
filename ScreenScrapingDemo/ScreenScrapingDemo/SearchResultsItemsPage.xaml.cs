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
using WebsiteLibrary;

// The Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234233

namespace ScreenScrapingDemo
{
    /// <summary>
    /// A page that displays a collection of item previews.  In the Split Application this page
    /// is used to display and select one of the available groups.
    /// </summary>
    public sealed partial class SearchResultsItemsPage : ScreenScrapingDemo.Common.LayoutAwarePage
    {
        private string query = null;
        public SearchResultsItemsPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            // TODO: Assign a bindable collection of items to this.DefaultViewModel["Items"]
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
            {
                base.OnNavigatedTo(e);
                return;
            }
            query = e.Parameter as string;
            progressBar.Visibility = Windows.UI.Xaml.Visibility.Visible;
            List<Listing> items = new List<Listing>();
            try
            {
                string url = Flipkart.generateUrl(query);
                WebResponse response = await WebRequest.Create(url).GetResponseAsync();
                List<Listing> items1 = Flipkart.getTop10Listings(Utility.Helper.getHTMLString(response));

                url = Junglee.generateUrl(query);
                response = await WebRequest.Create(url).GetResponseAsync();
                List<Listing> items2 = Junglee.getTop10Listings(Utility.Helper.getHTMLString(response));

                items = Utility.Helper.mergeLists(items1, items2);
            }
            catch (Exception)
            {
                this.Frame.Navigate(typeof(ErrorPage), "Sorry! There is some problem with the app.");
            }
            
//            List<string> items = new List<string>() { "string1", "string2"};
/*            List<Listing> items = new List<Listing>() { 
                new Listing("Title: Steve Jobs: The Exclusive Biography",
                    "Author: Walter Isaacson, Walter Isaacson, Walter Isaacson, Walter Isaacson",
                    "Price:  Rs. 647456",
                    "http://img8.flixcart.com/image/book/7/4/8/steve-jobs-the-exclusive-biography-100x100-imad8u3phcmwwkrq.jpeg",
                    "http://www.flipkart.com/steve-jobs-1408703742/p/itmd34yfgtwfzryn/search-books-steve-jobs/1?ref=e7c53053-fa29-42a5-bd9f-b2412404ea08&pid=9781408703748"),
                new Listing("Title: Steve Jobs: The Man Who Thought Different", 
                    "Author: Karen Blumenthal",
                    "Price:  Rs. 279",
                    "http://img7.flixcart.com/image/book/0/6/6/steve-jobs-the-man-who-thought-different-100x100-imadat2ayphvf9jb.jpeg",
                    "http://www.flipkart.com/steve-jobs-1408832062/p/itmd5kyk9zwpweys/search-books-steve-jobs/2?ref=e7c53053-fa29-42a5-bd9f-b2412404ea08&pid=9781408832066")
            };*/
            progressBar.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            itemsViewSource.Source = items;
            base.OnNavigatedTo(e);
        }

        private void itemGridView_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            this.Frame.Navigate(typeof(BrowserPage), (e.ClickedItem as Listing).WebPageLink);
        }

        /// <summary>
        /// Invoked as an event handler to navigate backward in the navigation stack
        /// associated with this page's <see cref="Frame"/>.
        /// </summary>
        /// <param name="sender">Instance that triggered the event.</param>
        /// <param name="e">Event data describing the conditions that led to the
        /// event.</param>
        protected override void GoBack(object sender, RoutedEventArgs e)
        {
            itemsViewSource.Source = new List<Listing>();
            // Use the navigation frame to return to the previous page
            if (this.Frame != null && this.Frame.CanGoBack) this.Frame.GoBack();
        }
    }
}
