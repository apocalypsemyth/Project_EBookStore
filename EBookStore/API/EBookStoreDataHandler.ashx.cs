using EBookStore.EBookStore.ORM;
using EBookStore.Managers;
using EBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBookStore.API
{
    /// <summary>
    /// Summary description for EBookStoreDataHandler
    /// </summary>
    public class EBookStoreDataHandler : IHttpHandler
    {
        private BookManager _bookMgr = new BookManager();

        public void ProcessRequest(HttpContext context)
        {
            if (string.Compare("GET", context.Request.HttpMethod, true) == 0 && !string.IsNullOrWhiteSpace(context.Request.QueryString["Name"]))
            {
                string name = context.Request.QueryString["Name"];
                Book obj = _bookMgr.GetBook(name);
                BookModel obj2 = BuildBookModel(obj);
                string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(obj2);

                context.Response.ContentType = "application/json";
                context.Response.Write(jsonText);
                return;
            }
        }

        private BookModel BuildBookModel(Book obj)
        {
            return new BookModel() {
                BookID = obj.BookID,
                UserID = obj.UserID,
                CategoryName = obj.CategoryName,
                AuthorName = obj.AuthorName,
                BookName = obj.BookName,
                Description = obj.Description,
                Image = obj.Image,
                IsEnable = obj.IsEnable,
                Date = obj.Date,
                EndDate = obj.EndDate,
            };
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