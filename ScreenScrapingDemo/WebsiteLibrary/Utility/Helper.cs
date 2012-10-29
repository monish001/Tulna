using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using WebsiteLibrary;

namespace ScreenScrapingDemo.Utility
{

    public static class Helper
    {
        public static string getHTMLString(WebResponse response)
        {
            StringBuilder sb = new StringBuilder();
            byte[] buf = new byte[8192];
            Stream resStream = response.GetResponseStream();
            string tempString = null;
            int count = 0;
            do
            {
                // fill the buffer with data
                count = resStream.Read(buf, 0, buf.Length);
                // make sure we read some data
                if (count != 0)
                {
                    // translate from bytes to ASCII text
                    tempString = Encoding.UTF8.GetString(buf, 0, count);
                    // continue building the string
                    sb.Append(tempString);
                }
            }
            while (count > 0); // any more data to read?
            return sb.ToString();
        }

        public static List<Listing> mergeLists(List<Listing> items1, List<Listing> items2)
        {
            List<Listing> final = new List<Listing>();
            int count1 = items1.Count, count2 = items2.Count;
            int min = (count1<count2?count1:count2);
            for(int i=0; i<min; i++){
                final.Add(items1[i]);
                final.Add(items2[i]);
            }
            if (count1 > count2)
                for (int i = min; i < count1; i++)
                    final.Add(items1[i]);
            else
                for (int i = min; i < count2; i++)
                    final.Add(items2[i]);
            return final;
        }
    }
}
