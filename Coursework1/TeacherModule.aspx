<%@ Page Title="TeacherModule" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TeacherModule.aspx.cs" Inherits="Coursework1.TeacherModule" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="TeacherModuleForm" runat="server" style="margin-left:1.5rem" class="was-validated">
        <div class="form-group">
            <h2> Table of Teachers </h2>
        </div>

        <div class="form-group ">
            <asp:GridView ID="teacherGv" DataKeyNames="ID" runat="server"  EmptyDataText="No records has been added." 
                class="table table-bordered table-condensed table-responsive table-hover form-group" OnSelectedIndexChanged="onTeacherSelect" AutoGenerateSelectButton="true"></asp:GridView>
        </div>

        <div class="form-group">
            <h2> Table of Associated Modules </h2>
        </div>

        <div class="form-group">
            <asp:GridView ID="moduleGv" runat="server" DataKeyNames="Module Code" EmptyDataText="Please select a teacher teaching atleast one module to view their associalted modules."
                class="table table-bordered table-condensed table-responsive table-hover "/>
        </div>

    </form>
</asp:Content>
