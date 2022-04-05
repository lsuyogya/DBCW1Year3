<%@ Page Title="StudentAssignment" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentAssignment.aspx.cs" Inherits="Coursework1.StudentAssignment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="TeacherModuleForm" runat="server" style="margin-left:1.5rem" class="was-validated">
        <div class="form-group">
            <h2> Table of Student </h2>
        </div>

        <div class="form-group ">
            <asp:GridView ID="studentGv" DataKeyNames="ID" runat="server"  EmptyDataText="No records has been added." 
                class="table table-bordered table-condensed table-responsive table-hover form-group" OnSelectedIndexChanged="onStudentSelect" AutoGenerateSelectButton="true"></asp:GridView>
        </div>

        <div class="form-group">
            <h2> Table of Assignment Details </h2>
        </div>

        <div class="form-group">
            <asp:GridView ID="studentAssignmentGv" runat="server" EmptyDataText="Please select a student who has been graded on atleast one assignment."
                class="table table-bordered table-condensed table-responsive table-hover "/>
        </div>

    </form>
</asp:Content>
