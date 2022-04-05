<%@ Page Title="StudentFees" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentFees.aspx.cs" Inherits="Coursework1.StudentFees" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="StudentFeesForm" runat="server" style="margin-left:1.5rem" class="was-validated">
        <div class="form-group">
            <h2> Table of Student </h2>
        </div>

        <div class="form-group ">
            <asp:GridView ID="studentGv" DataKeyNames="ID" runat="server"  EmptyDataText="No records has been added." 
                class="table table-bordered table-condensed table-responsive table-hover form-group" OnSelectedIndexChanged="onStudentSelect" AutoGenerateSelectButton="true"></asp:GridView>
        </div>

        <div class="form-group">
            <h2> Table of Fee Details </h2>
        </div>

        <div class="form-group">
            <asp:GridView ID="studentFeesGv" runat="server" EmptyDataText="Please select a student who has been assigned at least one fee invoice."
                class="table table-bordered table-condensed table-responsive table-hover "/>
        </div>

    </form>
</asp:Content>
