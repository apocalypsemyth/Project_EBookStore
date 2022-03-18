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
        public string SearchText { get; set; }

        private BookManager _bookMgr = new BookManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = this.txtSearch.Text;
            var list = this._bookMgr.GetBookWithLabelList(searchText);
        }
    }
}