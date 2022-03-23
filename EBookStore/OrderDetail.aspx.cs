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
        private PaymentManager _paymentMgr = new PaymentManager();
        private OrderManager _orderMgr = new OrderManager();
        private BookManager _bookMgr = new BookManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            var paymentList = this._paymentMgr.GetPaymentList();

            var orderBookList = this._orderMgr.GetOnlyOneUnfinishOrderItsOrderBookList();
            var bookList = this._bookMgr.GetBookList();
            var findBookIDList = orderBookList.Select(item => item.BookID);
            var findBookList = findBookIDList.Select(item => bookList.Where(item2 => item2.BookID == item).FirstOrDefault());
            var resultBookList = findBookList.ToList();

            if (resultBookList.Count == 0)
            {
                this.ddlPaymentList.Visible = false;

                this.rptOrderBookList.Visible = false;
                this.plcOrderBookEmpty.Visible = true;
            }
            else
            {
                this.ddlPaymentList.DataTextField = "PaymentName";
                this.ddlPaymentList.DataValueField = "PaymentName";
                this.ddlPaymentList.DataSource = paymentList;
                this.ddlPaymentList.DataBind();

                this.rptOrderBookList.Visible = true;
                this.plcOrderBookEmpty.Visible = false;
                this.rptOrderBookList.DataSource = resultBookList;
                this.rptOrderBookList.DataBind();
            }
        }
    }
}