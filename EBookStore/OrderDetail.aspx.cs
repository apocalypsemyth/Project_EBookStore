using EBookStore.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EBookStore
{
    public partial class OrderDetail : System.Web.UI.Page
    {
        private BookManager _bookMgr = new BookManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            var list = this._bookMgr.GetBookList();

            if (list.Count == 0)
            {
                this.rptOrderBookList.Visible = false;
                this.plcOrderBookEmpty.Visible = true;
            }
            else
            {
                this.rptOrderBookList.Visible = true;
                this.plcOrderBookEmpty.Visible = false;

                this.rptOrderBookList.DataSource = list;
                this.rptOrderBookList.DataBind();
            }
        }
    }
}