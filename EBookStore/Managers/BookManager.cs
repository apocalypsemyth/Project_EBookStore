using EBookStore.Helpers;
using EBookStore.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EBookStore.Managers
{
    public class BookManager
    {
        public List<BookModel> GetBookWithLabelList(string searchText)
        {
            string whereCondition = "";
            string joinCondition = "";
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                whereCondition = "WHERE BookName LIKE @BookName";
                joinCondition = "JOIN Labels ON Books.LabelID = Labels.LabelID";
            }

            string connectString = ConfigHelper.GetConnectionString();
            string commandText =
                $@" 
                    SELECT *
                    FROM Books 
                    {joinCondition}
                    {whereCondition}
                    ORDER BY Date DESC 
                ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectString))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        if (!string.IsNullOrWhiteSpace(searchText))
                        {
                            command.Parameters.AddWithValue("@BookName", searchText);
                        }

                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        List<BookModel> retList = new List<BookModel>();    // 將資料庫內容轉為自定義型別清單
                        while (reader.Read())
                        {
                            BookModel info = this.BuildBookModel(reader);
                            retList.Add(info);
                        }

                        return retList;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("BookManager.GetBookWithLabelList", ex);
                throw;
            }
        }

        public List<BookModel> GetAdminBookWithLabelList()
        {
            string joinCondition = "JOIN Labels ON Books.LabelID = Labels.LabelID";

            string connectString = ConfigHelper.GetConnectionString();
            string commandText =
                $@" 
                    SELECT *
                    FROM Books 
                    {joinCondition}
                    ORDER BY Date DESC 
                ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectString))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        List<BookModel> retList = new List<BookModel>();    // 將資料庫內容轉為自定義型別清單
                        while (reader.Read())
                        {
                            BookModel info = this.BuildBookModel(reader);
                            retList.Add(info);
                        }

                        return retList;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("BookManager.GetAdminBookWithLabelList", ex);
                throw;
            }
        }

        public BookModel GetBookWithLabel(Guid bookID)
        {
            string joinCondition = "JOIN Labels ON Books.LabelID = Labels.LabelID";

            string connectString = ConfigHelper.GetConnectionString();
            string commandText =
                $@" 
                    SELECT *
                    FROM Books
                    {joinCondition}
                    WHERE BookID = @BookID 
                ";

            try
            {
                using (SqlConnection connect = new SqlConnection(connectString))
                {
                    using (SqlCommand command = new SqlCommand(commandText, connect))
                    {
                        command.Parameters.AddWithValue("@BookID", bookID);

                        connect.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        BookModel model = new BookModel();
                        if (reader.Read())
                        {
                            model = this.BuildBookModel(reader);
                        }

                        return model;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("BookManager.GetBook", ex);
                throw;
            }
        }

        private BookModel BuildBookModel(SqlDataReader reader)
        {
            return new BookModel()
            {
                BookID = (Guid)reader["BookID"],
                CategoryName = reader["CategoryName"] as string,
                AuthorName = reader["AuthorName"] as string,
                BookName = reader["BookName"] as string,
                Description = reader["Description"] as string,
                Image = reader["Image"] as string,
                IsEnable = (bool)reader["IsEnable"],
                Date = (DateTime)reader["Date"],
                EndDate = reader["EndDate"] as DateTime?,
            };
        }

        public void CreateBook(BookModel model, string createUserID, Guid labelID)
        {
            string connectString = ConfigHelper.GetConnectionString();
            string commandText =
               @"   
                    INSERT INTO Books
                        (BookID, UserID, LabelID, CategoryName, AuthorName, BookName, Description, Image, IsEnable, Date)
                    VALUES
                        (@BookID, @UserID, @LabelID, @CategoryName, @AuthorName, @BookName, @Description, @Image, @IsEnable, @Date) 
               ";

            try
            {
                using (SqlConnection connect = new SqlConnection(connectString))
                {
                    using (SqlCommand command = new SqlCommand(commandText, connect))
                    {
                        model.BookID = Guid.NewGuid();

                        command.Parameters.AddWithValue("@BookID", model.BookID);
                        command.Parameters.AddWithValue("@UserID", createUserID);
                        command.Parameters.AddWithValue("@LabelID", labelID);
                        command.Parameters.AddWithValue("@CategoryName", model.CategoryName);
                        command.Parameters.AddWithValue("@AuthorName", model.AuthorName);
                        command.Parameters.AddWithValue("@BookName", model.BookName);
                        command.Parameters.AddWithValue("@Description", model.Description);
                        command.Parameters.AddWithValue("@Image", model.Image);
                        command.Parameters.AddWithValue("@IsEnable", model.IsEnable);
                        command.Parameters.AddWithValue("@Date", DateTime.Now);
                        
                        connect.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("BookManager.CreateBook", ex);
                throw;
            }
        }
    }
}