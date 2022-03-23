using EBookStore.EBookStore.ORM;
using EBookStore.Helpers;
using EBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBookStore.Managers
{
    public class BookManager
    {
        public List<Book> GetBookList()
        {
            try
            {
                using (ContextModel contextModel = new ContextModel())
                {
                    List<Book> retList = new List<Book>();
                    retList = contextModel.Books.Where(item => item.IsEnable).ToList();

                    return retList;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("BookManager.GetBookList", ex);
                throw;
            }
        }

        public List<Book> GetBookList(string searchText)
        {
            try
            {
                using (ContextModel contextModel = new ContextModel())
                {
                    List<Book> retList = new List<Book>();
                    var query = contextModel.Books.Where(item => item.IsEnable && item.BookName == searchText);
                    retList = query.ToList();

                    return retList;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("BookManager.GetBookList", ex);
                throw;
            }
        }

        public Book GetBook(Guid bookID)
        {
            try
            {
                using (ContextModel contextModel = new ContextModel())
                {
                    Book model = new Book();
                    model = contextModel.Books.Where(item => item.BookID == bookID).FirstOrDefault();

                    return model;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("BookManager.GetBook", ex);
                throw;
            }
        }

        public Book GetBook(string searchText)
        {
            try
            {
                using (ContextModel contextModel = new ContextModel())
                {
                    Book model = new Book();
                    model = contextModel.Books.Where(item => item.BookName == searchText).FirstOrDefault();

                    return model;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("BookManager.GetBook", ex);
                throw;
            }
        }

        // Temp createUserID
        public void CreateBook(Book model, Guid createUserID)
        {
            try
            {
                using (ContextModel contextModel = new ContextModel())
                {
                    model.BookID = Guid.NewGuid();
                    model.UserID = createUserID;
                    model.Date = DateTime.Now;
                    contextModel.Books.Add(model);
                    contextModel.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("BookManager.CreateBook", ex);
                throw;
            }
        }

        public BookModel BuildBookModel(Book obj)
        {
            return new BookModel()
            {
                BookID = obj.BookID,
                UserID = obj.UserID,
                CategoryName = obj.CategoryName,
                AuthorName = obj.AuthorName,
                BookName = obj.BookName,
                Description = obj.Description,
                Price = obj.Price,
                Image = obj.Image,
                IsEnable = obj.IsEnable,
                Date = obj.Date,
                EndDate = obj.EndDate,
            };
        }
    }
}