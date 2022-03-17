<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="BookDetail.aspx.cs" Inherits="EBookStore.BookDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Css/BookDetail.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="book__container">
        <div class="book__image-container">
            <img class="book__image" id="imgImage" runat="server" />
        </div>

        <div class="book__content">
            <p class="book__category-name">
                類別：
                <asp:Literal ID="ltlCategoryName" runat="server"></asp:Literal>
            </p>
            <h1 class="book__book-name">
                書名：
                <asp:Literal ID="ltlBookName" runat="server"></asp:Literal>
            </h1>
            <h2 class="book__author-name">
                作者：
                <asp:Literal ID="ltlAuthorName" runat="server"></asp:Literal>
            </h2>
            <h3 class="book__description">
                描述：
                <asp:Literal ID="ltlDescription" runat="server"></asp:Literal>
            </h3>
            <p class="book__date">
                伊始期：
                <asp:Literal ID="ltlDate" runat="server"></asp:Literal>
            </p>
            <p class="book__end-date">
                <asp:Literal ID="ltlEndDate" Text="結束期： " runat="server"></asp:Literal>
            </p>
        </div>

        <div class="book__add-shopping-cart-container">
            <div class="book__add-shopping-cart" runat="server">
                加入購物車
            </div>
        </div>
    </div>
</asp:Content>
