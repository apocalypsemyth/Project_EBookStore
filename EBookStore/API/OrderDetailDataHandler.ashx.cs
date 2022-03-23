using EBookStore.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBookStore.API
{
    /// <summary>
    /// Summary description for OrderDetailDataHandler
    /// </summary>
    public class OrderDetailDataHandler : IHttpHandler
    {
        private OrderManager _orderMgr = new OrderManager();
        private PaymentManager _paymentMgr = new PaymentManager();

        public void ProcessRequest(HttpContext context)
        {
            if (string.Compare("GET", context.Request.HttpMethod, true) == 0)
            {
                var orderBookList = this._orderMgr.GetOnlyOneUnfinishOrderItsOrderBookList();
                string orderBookAmount = orderBookList.Count().ToString();

                context.Response.ContentType = "text/plain";
                context.Response.Write(orderBookAmount);
                return;
            }

            if (string.Compare("POST", context.Request.HttpMethod, true) == 0 && string.Compare("CREATE", context.Request.QueryString["Action"], true) == 0)
            {
                string bookIDStr = context.Request.Form["bookID"];
                bool isValidBookID = Guid.TryParse(bookIDStr, out Guid bookID);

                if (!isValidBookID)
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("NULL");
                    return;
                }

                // Temp UserID
                string userIDStr = "11f49178-69bc-4057-ad06-7cbb74b4e38d";
                bool isValidUserID = Guid.TryParse(userIDStr, out Guid userID);

                var payment = this._paymentMgr.GetPayment();
                Guid paymentID = payment.PaymentID;

                string orderBookAmount = "";
                var hasOrder = this._orderMgr.GetOnlyOneUnfinishOrder();
                if (hasOrder == null)
                {
                    this._orderMgr.CreateOrder(userID, paymentID);
                    var order = this._orderMgr.GetOnlyOneUnfinishOrder();
                    this._orderMgr.CreateOrderBook(order.OrderID, bookID);
                    var orderBookList = this._orderMgr.GetOnlyOneUnfinishOrderItsOrderBookList();
                    orderBookAmount = orderBookList.Count().ToString();
                }
                else
                {
                    var orderBook = this._orderMgr.GetOnlyOneUnfinishOrderItsOrderBookList();
                    bool isCurrentBookInOrder = orderBook.Where(item => item.BookID == bookID).Any();

                    if (isCurrentBookInOrder)
                    {
                        context.Response.ContentType = "text/plain";
                        context.Response.Write("NULL");
                        return;
                    }

                    this._orderMgr.CreateOrderBook(hasOrder.OrderID, bookID);
                    var orderBookList = this._orderMgr.GetOnlyOneUnfinishOrderItsOrderBookList();
                    orderBookAmount = orderBookList.Count().ToString();
                }

                context.Response.ContentType = "text/plain";
                context.Response.Write(orderBookAmount);
                return;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}