using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IndividueleOpdracht
{
    public partial class advertentieUserControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private Product product; 
        public Product Product
        { 
            get { return product;}
            set
            {
                product = value;
                advertisementTitle.Text = product.Titel;
                content.Text = product.AdvertentieTekst;
                
            } 
        }
    }
}