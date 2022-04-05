﻿<%@ Page Title="Module" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Module.aspx.cs" Inherits="Coursework1.Module" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="ModuleForm" runat="server" style="margin-left:1.5rem" class="was-validated">
      
        <div class="form-group">
            <asp:Label ID="IDLbl" runat="server" Text="Module Code"></asp:Label>
            <asp:TextBox ID="IDTxt" runat="server" class="form-control" Width="250px" required="required"></asp:TextBox>
        </div>

        <div class="form-group">
            <asp:Label ID="nameLbl" runat="server" Text="Name"></asp:Label>
            <asp:TextBox ID="nameTxt" runat="server" class="form-control" Width="250px" required="required"></asp:TextBox>
        </div>

        <div class="form-group">
            <asp:Label ID="creditHrsLbl" runat="server" Text="Credit Hours"></asp:Label>
            <asp:TextBox ID="creditHrsTxt" runat="server" class="form-control" Width="250px" type="number" required="required"></asp:TextBox>
        </div>

        <div>
            <asp:Button ID="submitBtn" runat="server" Text="Add" OnClick="SubmitHandler"  class="btn btn-primary form-group"/>
            <asp:Label ID="ErrorLbl" runat="server" Text="" Visible="false"></asp:Label>
        </div>

        <div>
            <asp:GridView ID="moduleGv" runat="server" DataKeyNames="ID" OnRowDataBound="OnRowDataBound"
                OnRowEditing="OnRowEditing" OnRowCancelingEdit="OnRowCancelingEdit"  OnRowDeleting="OnRowDeleting" 
                EmptyDataText="No records has been added." AutoGenerateEditButton="true" AutoGenerateDeleteButton="true" 
                class="table table-bordered table-condensed table-responsive table-hover "/>
        </div>

        <div>
            <asp:Button ID="updateBtn" runat="server" Text="Update" OnClick="SubmitHandler" Visible="false" class="btn btn-primary form-group"/>
        </div>
    </form>
</asp:Content>