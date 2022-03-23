using EBookStore.EBookStore.ORM;
using EBookStore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBookStore.Managers
{
    public class PaymentManager
    {
        public Payment GetPayment()
        {
            try
            {
                using (ContextModel contextModel = new ContextModel())
                {
                    Payment firstPayment = new Payment();
                    firstPayment = contextModel.Payments.FirstOrDefault();

                    return firstPayment;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("PaymentManager.GetPayment", ex);
                throw;
            }
        }

        public List<Payment> GetPaymentList()
        {
            try
            {
                using (ContextModel contextModel = new ContextModel())
                {
                    List<Payment> paymentList = new List<Payment>();
                    var findPaymentList = contextModel.Payments.Select(item => item);
                    paymentList = findPaymentList.ToList();

                    return paymentList;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("PaymentManager.GetPaymentList", ex);
                throw;
            }
        }

        public void CreatePayment(string payment)
        {
            try
            {
                using (ContextModel contextModel = new ContextModel())
                {
                    Payment newPayment = new Payment()
                    {
                        PaymentID = Guid.NewGuid(),
                        PaymentName = payment,
                        PaymentDate = DateTime.Now,
                    };

                    contextModel.Payments.Add(newPayment);
                    contextModel.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("PaymentManager.CreatePayment", ex);
                throw;
            }
        }
    }
}