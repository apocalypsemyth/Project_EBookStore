<%@ Page Title="" Language="C#" MasterPageFile="~/BackAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="BookList.aspx.cs" Inherits="EBookStore.BackAdmin.BookList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Button ID="btnDelete" runat="server" Text="刪除" OnClick="btnDelete_Click" />
    <asp:Button ID="btnCreate" runat="server" Text="新增" OnClick="btnCreate_Click" />
    <br />

    <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
    <asp:Button ID="btnSearch" runat="server" Text="搜尋" OnClick="btnSearch_Click" />
    <br />

    <asp:Repeater ID="rptList" runat="server">
        <ItemTemplate>
            <div>
                <asp:CheckBox runat="server" ID="ckbDel" />
                <asp:HiddenField runat="server" ID="hfID" Value='<%# Eval("BookID") %>' />

                <p>
                    <a href="BookDetail.aspx?ID=<%# Eval("BookID") %>" title="前往查看：<%# Eval("BookName") %>">
                        <%# Eval("BookName") %> 
                    </a>
                </p>
                <asp:PlaceHolder runat="server" Visible='<%# 
                    !string.IsNullOrWhiteSpace(Eval("Image") as string) 
                %>'>
                    <a href="BookDetail.aspx?ID=<%# Eval("BookID") %>" title="前往查看：<%# Eval("BookName") %>">
                        <img src="<%# Eval("Image") %>" width="200" height="160" />
                    </a>
                </asp:PlaceHolder>
            </div>
        </ItemTemplate>
    </asp:Repeater>

    <asp:PlaceHolder runat="server" ID="plcEmpty" Visible="false">
        <p>尚未有資料 </p>
    </asp:PlaceHolder>
</asp:Content>
