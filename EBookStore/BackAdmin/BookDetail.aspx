<%@ Page Title="" Language="C#" MasterPageFile="~/BackAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="BookDetail.aspx.cs" Inherits="EBookStore.BackAdmin.BookDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>打 * 號者為必填(或必選)</p>
    <table>
        <tr>
            <th> 類別 (*) </th>
            <td> <asp:TextBox ID="txtCategoryName" runat="server"></asp:TextBox> </td>
        </tr>
        <tr>
            <th> 作者 (*) </th>
            <td> <asp:TextBox ID="txtAuthorName" runat="server"></asp:TextBox> </td>
        </tr>
        <tr>
            <th> 書名 (*) </th>
            <td> <asp:TextBox ID="txtBookName" runat="server"></asp:TextBox> </td>
        </tr>
        <tr>
            <th> 描述 (*) </th>
            <td> <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine"></asp:TextBox> </td>
        </tr>
        <tr>
            <th> 封面圖 (*) </th>
            <td> 
                <asp:FileUpload ID="fuImage" runat="server" />
                <asp:Image ID="imgImage" runat="server" />
            </td>
        </tr>
        <tr>
            <th> 使否顯示 (*) </th>
            <td> <asp:CheckBox ID="ckbIsEnable" runat="server" /> </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
            </td>
        </tr>
    </table>

    <asp:Button runat="server" ID="btnSave" Text="儲存" OnClick="btnSave_Click" />
    <asp:Button runat="server" ID="btnCancel" Text="取消" OnClick="btnCancel_Click" />
</asp:Content>
