<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Coursework1.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div style="margin: 2em 0 0 35%" >
           <h1> Berkeley College Management System</h1>
        </div>
        <div style="margin: 4em 0 0 2em" >
           <h2>Available webforms: </h2>
        </div>
    <div class="container-fluid" style="display:flex; flex-wrap:wrap; gap:1em; padding:2rem">

        <div class="card text-white bg-danger mb-3" style="max-width: 18rem;">
            <div class="card-header">Basic Form</div>
            <div class="card-body">
            <h5 class="card-title">Teacher Details</h5>
            <p class="card-text">View and edit teacher details through this form.</p>
            </div>
        </div>
        <div class="card text-white bg-danger mb-3" style="max-width: 18rem;">
            <div class="card-header">Basic Form</div>
            <div class="card-body">
            <h5 class="card-title">Student Details</h5>
            <p class="card-text">View, add, update or delete student details through this form.</p>
            </div>
        </div>
        <div class="card text-white bg-danger mb-3" style="max-width: 18rem;">
            <div class="card-header">Basic Form</div>
            <div class="card-body">
            <h5 class="card-title">Module Details</h5>
            <p class="card-text">View, add, update or delete module details through this form.</p>
            </div>
        </div>
        <div class="card text-white bg-danger mb-3" style="max-width: 18rem;">
            <div class="card-header">Basic Form</div>
            <div class="card-body">
            <h5 class="card-title">Address Details</h5>
            <p class="card-text">View, add, update or delete address details through this form.</p>
            </div>
        </div>
        <div class="card text-white bg-danger mb-3" style="max-width: 18rem;">
            <div class="card-header">Basic Form</div>
            <div class="card-body">
            <h5 class="card-title">Departemnt Details</h5>
            <p class="card-text">View, add, update or delete department details through this form.</p>
            </div>
        </div>
    </div>
    <div class="container-fluid" style="display:flex; flex-wrap:wrap; gap:1em; padding:2rem">

        <div class="card text-white bg-info mb-3" style="max-width: 18rem;">
            <div class="card-header">Complex Form</div>
            <div class="card-body">
            <h5 class="card-title">Teacher-Module Details</h5>
            <p class="card-text">View teacher details including their associated module details through this form.</p>
            </div>
        </div>
        <div class="card text-white bg-info mb-3" style="max-width: 18rem;">
            <div class="card-header">Complex Form</div>
            <div class="card-body">
            <h5 class="card-title">Student-Fees Details</h5>
            <p class="card-text">View student details along with their fee details through this form.</p>
            </div>
        </div>
        <div class="card text-white bg-info mb-3" style="max-width: 18rem;">
            <div class="card-header">Complex Form</div>
            <div class="card-body">
            <h5 class="card-title">Student_Assignment Details</h5>
            <p class="card-text">Viewstudent details along with their assignment details through this form.</p>
            </div>
        </div>
    </div>
</asp:Content>
