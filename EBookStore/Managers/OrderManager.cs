using EBookStore.EBookStore.ORM;
using EBookStore.Helpers;
using EBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBookStore.Managers
{
    public class OrderManager
    {
        /** Order Part */
        public Order GetOnlyOneUnfinishOrder()
        {
            try
            {
                using (ContextModel contextModel = new ContextModel())
                {
                    var findUnfinishOrder = contextModel.Orders.Where(item => item.OrderStatus == 0);

                    int isOnlyOneUnfinishOrder = findUnfinishOrder.Count();
                    if (isOnlyOneUnfinishOrder != 1)
                        return null;

                    var UnfinishOrder = findUnfinishOrder.FirstOrDefault();
                    return UnfinishOrder;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("OrderManager.GetOnlyOneUnfinishOrder", ex);
                throw;
            }
        }

        public void CreateOrder(Guid userID, Guid paymentID)
        {
            try
            {
                using (ContextModel contextModel = new ContextModel())
                {
                    Order newOrder = new Order()
                    {
                        OrderID = Guid.NewGuid(),
                        UserID = userID,
                        PaymentID = paymentID,
                        OrderDate = DateTime.Now,
                        OrderStatus = 0,
                    };

                    contextModel.Orders.Add(newOrder);
                    contextModel.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("OrderManager.CreateOrder", ex);
                throw;
            }
        }


        /** OrderBook Part */
        public List<OrderBook> GetOnlyOneUnfinishOrderItsOrderBookList()
        {
            try
            {
                using (ContextModel contextModel = new ContextModel())
                {
                    List<OrderBook> list = new List<OrderBook>();

                    var UnfinishOrder = this.GetOnlyOneUnfinishOrder();
                    if (UnfinishOrder == null)
                        return new List<OrderBook>();

                    var findOrderBooks = contextModel.OrderBooks.Where(item => item.OrderID == UnfinishOrder.OrderID);

                    list = findOrderBooks.ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("OrderManager.GetOrderBookList", ex);
                throw;
            }
        }

        public void CreateOrderBook(Guid orderID, Guid bookID)
        {
            try
            {
                using (ContextModel contextModel = new ContextModel())
                {
                    OrderBook newOrderBook = new OrderBook()
                    {
                        OrderBookID = Guid.NewGuid(),
                        OrderID = orderID,
                        BookID = bookID,
                    };

                    contextModel.OrderBooks.Add(newOrderBook);
                    contextModel.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("OrderManager.CreateOrderBook", ex);
                throw;
            }
        }
    }
}