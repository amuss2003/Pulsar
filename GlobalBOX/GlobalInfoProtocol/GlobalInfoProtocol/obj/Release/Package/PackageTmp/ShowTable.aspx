<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowTable.aspx.cs" Inherits="GlobalInfoProtocol.ShowTable" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>     
        <asp:Label ID="lblTableInfo" runat="server" 
            style="font-weight: 700; font-size: medium; font-family: Arial, Helvetica, sans-serif" 
            Text="Table Info"></asp:Label>
        <br />
        <asp:GridView ID="GridView1" runat="server" BackColor="White" 
            BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
            EnableModelValidation="True" GridLines="Vertical" 
            style="font-size: small; font-family: Arial, Helvetica, sans-serif" 
            AllowPaging="True" AllowSorting="True" DataSourceID="AccessDataSource1" 
            PageSize="20" onrowcreated="GridView1_RowCreated" 
            onrowdatabound="GridView1_RowDataBound">            
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        </asp:GridView>    
        <asp:AccessDataSource ID="AccessDataSource1" runat="server"
            DataFile="~/App_Data/GlobalInfoProtocol.mdb">        
        </asp:AccessDataSource>
    </div>
    </form>
</body>
</html>
