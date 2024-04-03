<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BookManagement.aspx.cs" Inherits="BookManagement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Lbl_Id" runat="server" Text="Id:"></asp:Label>
            <asp:TextBox ID="Txt_Id" runat="server" Text="-1" ReadOnly="true"></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="Lbl_Title" runat="server" Text="Title:"></asp:Label>
            <asp:TextBox ValidationGroup='valGroup1' ID="Txt_Title" runat="server" placeholder="Title"></asp:TextBox>
            <asp:RequiredFieldValidator ValidationGroup='valGroup1' ID="RequiredFieldValidator1" runat="server" ControlToValidate="Txt_Title" ErrorMessage="Title is required." ForeColor="Red"></asp:RequiredFieldValidator>
        </div>
        <div>
            <asp:Label ID="Lbl_Author" runat="server" Text="Author:"></asp:Label>
            <asp:TextBox ValidationGroup='valGroup1' ID="Txt_Author" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ValidationGroup='valGroup1' ID="RequiredFieldValidator2" runat="server" ControlToValidate="Txt_Author" ErrorMessage="Author is required." ForeColor="Red"></asp:RequiredFieldValidator>
        </div>
        <div>
            <asp:Label ID="Lbl_Price" runat="server" Text="Price:"></asp:Label>
            <asp:TextBox ValidationGroup='valGroup1' ID="Txt_Price" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ValidationGroup='valGroup1' ID="RequiredFieldValidator3" runat="server" ControlToValidate="Txt_Price" ErrorMessage="Price is required." ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ValidationGroup='valGroup1' ID="RegularExpressionValidator1" runat="server" ControlToValidate="Txt_Price" ErrorMessage="Please enter a valid price." ForeColor="Red" ValidationExpression="^\d{1,7}(\.\d{1,2})?$"></asp:RegularExpressionValidator>
        </div>
        <div>
            <asp:Label ID="Lbl_PublishedYear" runat="server" Text="Published Year:"></asp:Label>
            <asp:TextBox ValidationGroup='valGroup1' ID="Txt_PublishedYear" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ValidationGroup='valGroup1' ID="RequiredFieldValidator4" runat="server" ControlToValidate="Txt_PublishedYear" ErrorMessage="Published Date required." ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ValidationGroup='valGroup1' ID="RegularExpressionValidator2" runat="server" ControlToValidate="Txt_PublishedYear" ErrorMessage="Please enter a valid year." ForeColor="Red" ValidationExpression="^[12][0-9]{3}$"></asp:RegularExpressionValidator>
        </div>
        <asp:Button ValidationGroup='valGroup1' ID="Btn_Add" runat="server" Text="Add" OnClick="Btn_Add_Click" />       
<%--        <asp:GridView ID="Tbl_Books" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" OnRowCommand="Tbl_Books_RowCommand">
            <Columns>
                <asp:BoundField DataField="DisplayID" HeaderText="S.N." />
                <asp:BoundField DataField="Id" HeaderText="Book Id" />
                <asp:BoundField DataField="Title" HeaderText="Title" />
                <asp:BoundField DataField="Author" HeaderText="Author" />
                <asp:BoundField DataField="Price" HeaderText="Price" />
                <asp:BoundField DataField="PublishedYear" HeaderText="Published Year" />
                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:Button ValidationGroup='noValidation' ID="btn_Edit" runat="server" Text="Edit" CommandName="Edit" />
                        <asp:Button ValidationGroup='noValidation' ID="btn_Delete" runat="server" Text="Delete" CommandName="Delete"/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>--%>
        <asp:GridView ID="Tbl_Books" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" OnRowEditing="Tbl_Books_RowEditing" OnRowUpdating="Tbl_Books_RowUpdating" OnRowDeleting="Tbl_Books_RowDeleting" OnRowCancelingEdit="Tbl_Books_RowCancelingEdit">
            <Columns>
                <asp:BoundField DataField="DisplayID" HeaderText="S.N." ReadOnly="true"/>
                <asp:BoundField DataField="Id" HeaderText="Book Id" ReadOnly="true"/>
                <asp:BoundField DataField="Title" HeaderText="Title" />
                <asp:BoundField DataField="Author" HeaderText="Author" />
                <asp:BoundField DataField="Price" HeaderText="Price" />
                <asp:BoundField DataField="PublishedYear" HeaderText="Published Year" />
                <asp:CommandField ShowEditButton="True" />
                <asp:CommandField ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
