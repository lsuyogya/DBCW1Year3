﻿<%@ Page Title="Department" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Department.aspx.cs" Inherits="Coursework1.Department" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="DepartmentForm" runat="server" style="margin-left:1.5rem" class="was-validated">
        <div>
            <asp:TextBox ID="IDTxt" runat="server" Visible ="false"></asp:TextBox>
        </div>


        <div class="form-group">
            <asp:Label ID="departmentLbl" runat="server" Text="Department"></asp:Label>
            <asp:TextBox ID="departmentTxt" runat="server" class="form-control" Width="250px" required="required"></asp:TextBox>
        </div>

        <div>
            <asp:Button ID="submitBtn" runat="server" Text="Add" OnClick="SubmitHandler"  class="btn btn-primary form-group"/>
            <asp:Label ID="ErrorLbl" runat="server" Text="" Visible="false"></asp:Label>
        </div>

        <div>
            <asp:GridView ID="departmentGv" runat="server" DataKeyNames="ID" OnRowDataBound="OnRowDataBound"
                OnRowEditing="OnRowEditing" OnRowCancelingEdit="OnRowCancelingEdit"  OnRowDeleting="OnRowDeleting" 
                EmptyDataText="No records has been added." AutoGenerateEditButton="true" AutoGenerateDeleteButton="true" 
                class="table table-bordered table-condensed table-responsive table-hover "/>
        </div>

        <div>
            <asp:Button ID="updateBtn" runat="server" Text="Update" OnClick="SubmitHandler" Visible="false" class="btn btn-primary form-group"/>
        </div>
    </form>
</asp:Content>