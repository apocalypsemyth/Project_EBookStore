using EBookStore.EBookStore.ORM;
using EBookStore.Managers;
using EBookStore.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EBookStore.BackAdmin
{
    public partial class BookDetail : System.Web.UI.Page
    {
        private bool _isEditMode = false;
        private BookManager _bookMgr = new BookManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            // 做編輯模式或新增模式的判斷
            if (!string.IsNullOrWhiteSpace(this.Request.QueryString["ID"]))
                this._isEditMode = true;
            else
                this._isEditMode = false;

            if (this._isEditMode)
                this.InitEditMode();
            else
                this.InitCreateMode();
        }

        /// <summary> 新增模式初始化 </summary>
        private void InitCreateMode()
        {
            this.imgImage.Visible = false;
            this.lblMsg.Text = "請新增資料";
        }

        /// <summary> 編輯模式初始化 </summary>
        private void InitEditMode()
        { 
            
        }

        /// <summary> 欄位檢查 (格式、型別檢查、必選填、上傳) </summary>
        /// <param name="errorMsgList"></param>
        /// <returns></returns>
        private bool CheckInput(out List<string> errorMsgList)
        {
            errorMsgList = new List<string>();

            if (string.IsNullOrWhiteSpace(this.txtCategoryName.Text))
                errorMsgList.Add("類別為必填。");
            
            if (string.IsNullOrWhiteSpace(this.txtAuthorName.Text))
                errorMsgList.Add("作者為必填。");

            if (string.IsNullOrWhiteSpace(this.txtBookName.Text))
                errorMsgList.Add("書名為必填。");

            if (string.IsNullOrWhiteSpace(this.txtDescription.Text))
                errorMsgList.Add("描述為必填。");

            if (!this._isEditMode)  // 只有新增模式，才做封面圖的必填
            {
                if (!this.fuImage.HasFile)
                    errorMsgList.Add("封面圖為必填。");
            }

            if (errorMsgList.Count > 0)
                return false;
            else
                return true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<string> errorMsgList = new List<string>();
            if (!this.CheckInput(out errorMsgList))
            {
                this.lblMsg.Text = string.Join("<br />", errorMsgList);
                return;
            }

            Book bookModel = new Book()
            {
                CategoryName = this.txtCategoryName.Text.Trim(),
                AuthorName = this.txtAuthorName.Text.Trim(),
                BookName = this.txtBookName.Text.Trim(),
                Description = this.txtDescription.Text.Trim(),
                IsEnable = this.ckbIsEnable.Checked,
            };

            if (this.fuImage.HasFile)  // 儲存檔案，並將路徑寫至 model ，以供保存
            {
                Thread.Sleep(3);
                Random random = new Random((int)DateTime.Now.Ticks);
                string folderPath = "~/FileDownload/Book/";
                string fileName =
                    DateTime.Now.ToString("yyyyMMdd_HHmmss_FFFFFF") +
                    "_" + random.Next(10000).ToString("0000") +
                    Path.GetExtension(this.fuImage.FileName);

                folderPath = HostingEnvironment.MapPath(folderPath);

                if (!Directory.Exists(folderPath))  // 假如資料夾不存在，先建立
                    Directory.CreateDirectory(folderPath);

                string newFilePath = Path.Combine(folderPath, fileName);
                this.fuImage.SaveAs(newFilePath);

                bookModel.Image = "/FileDownload/Book/" + fileName;
            }

            // Temp UserID
            string userID = "11f49178-69bc-4057-ad06-7cbb74b4e38d";
            bool isSuccess = Guid.TryParse(userID, out Guid gUserID);

            // 儲存
            if (isSuccess)
                this._bookMgr.CreateBook(bookModel, gUserID);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            // 跳回前頁
        }
    }
}