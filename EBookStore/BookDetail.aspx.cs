using EBookStore.Managers;
using EBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EBookStore
{
    public partial class BookDetail : System.Web.UI.Page
    {
        private BookManager _bookMgr = new BookManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            string bookIDText = this.Request.QueryString["ID"];

            // 如果沒有帶 id ，跳回列表頁
            if (string.IsNullOrWhiteSpace(bookIDText))
                this.BackToListPage();

            Guid bookID;
            if (!Guid.TryParse(bookIDText, out bookID))
                this.BackToListPage();

            // 查資料
            BookModel model = this._bookMgr.GetBookWithLabel(bookID);
            if (model == null)
                this.BackToListPage();

            // 不開放前台顯示
            if (!model.IsEnable)
                this.BackToListPage();

            // 顯示資料
            this.ShowDetail(model);
        }

        private void ShowDetail(BookModel model)
        {
            this.ltlCategoryName.Text = model.CategoryName;
            this.ltlBookName.Text = model.BookName;
            this.ltlAuthorName.Text = model.AuthorName;
            this.ltlDescription.Text = model.Description;
            this.imgImage.Src = model.Image;
            this.ltlDate.Text = model.Date.ToString("yyyy-MM-dd");

            DateTime? endDate = model.EndDate;
            if (endDate != null)
                this.ltlEndDate.Text += endDate?.ToString("yyyy-MM-dd");
            else
                this.ltlEndDate.Visible = false;
        }

        private void BackToListPage()
        {
            this.Response.Redirect("BookList.aspx", true);
        }
    }
}