using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteLibrary
{
    interface IListing
    {
        /*        public Int64 uniqueId
                {
                    get;            set;
                }*/

        String Title
        {
            get;
            set;
        }

        String Creditor
        {
            get;
            set;
        }

        String Price
        {
            get;
            set;
        }

        String WebPageLink
        {
            get;
            set;
        }

        String ImageLink
        {
            get;
            set;
        }
    }
}
