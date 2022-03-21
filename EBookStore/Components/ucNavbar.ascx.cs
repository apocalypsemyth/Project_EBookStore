using EBookStore.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EBookStore.Components
{
    public partial class ucNavbar : System.Web.UI.UserControl
    {
        public delegate void BtnSearch(object sender, string SearchText);
        public event BtnSearch BtnSearchClick = null;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = this.txtSearch.Text;

            if (!string.IsNullOrWhiteSpace(searchText) && this.BtnSearchClick != null)
                this.BtnSearchClick(this, searchText);

            this.txtSearch.Text = "";
        }
    }
}