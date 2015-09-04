using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SqlServerWebAdmin.Toolbars
{
    public partial class helplogouttoolbar : System.Web.UI.UserControl
    {

        private string helpTopic = "";

        public string HelpTopic
        {
            get
            {
                return helpTopic;
            }
            set
            {
                helpTopic = value;
            }
        }


        protected void Page_Load(object sender, System.EventArgs e)
        {
            // Put user code to initialize the page here
            HelpImageHyperLink.NavigateUrl = "../Help/" + helpTopic + ".aspx";
        }
    }
}