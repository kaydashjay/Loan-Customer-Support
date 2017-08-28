<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

   <%-- <div class="jumbotron">
        <h1>ASP.NET</h1>
        <p class="lead">ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS, and JavaScript.</p>
        <p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>
    </div>--%>
    <div class ="jumbotron">
         <h1>Loan Support</h1>
    </div>
    <div runat="server" id="caseAlert">
    </div>
     <div>
        <asp:Label ID="titleLabel" runat="server" Text="Case Title" AssociatedControlID ="title" ></asp:Label>
        <asp:TextBox ID="title" runat="server"></asp:TextBox>
       <asp:RequiredFieldValidator runat="server" id="reqTitle" controltovalidate="title" errormessage="*" />

    </div>
    <br>
    <div>
        <asp:Label ID="fnameLabel" runat="server" Text="First Name" AssociatedControlID ="fname"></asp:Label>
        <asp:TextBox ID="fname" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" id="reqFname" controltovalidate="fname" errormessage="*" />
    </div>
    <br>
    <div>
        <asp:Label ID="lnameLabel" runat="server" Text="Last Name" AssociatedControlID ="lname"></asp:Label>
        <asp:TextBox ID="lname" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" id="reqLname" controltovalidate="lname" errormessage="*" />
    </div>
    <br>
    <div>
        <asp:Label ID="loanLabel" runat="server" Text="Loan Number" AssociatedControlID ="loannumber"></asp:Label>
        <asp:TextBox ID="loannumber" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" id="reqNumber" controltovalidate="loannumber" errormessage="*" />
    </div>
    <br>
  
    <div>
        <label runat="server" for="description">Description</label>
        <textarea runat="server" id="description" cols="20" rows="2"></textarea>
    </div>
    <br>
    <div>
        <asp:Button CssClass ="btn btn-default" ID="submit" runat="server" Text="Submit" OnDataBinding="" OnClick="submit_Click" />
    </div>


    
    
  
<%--    <div class="row">
        
        <div class="col-md-4">
            <h2>Getting started</h2>
            <p>
                ASP.NET Web Forms lets you build dynamic websites using a familiar drag-and-drop, event-driven model.
            A design surface and hundreds of controls and components let you rapidly build sophisticated, powerful UI-driven sites with data access.
            </p>
            <p>
                <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301948">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Get more libraries</h2>
            <p>
                NuGet is a free Visual Studio extension that makes it easy to add, remove, and update libraries and tools in Visual Studio projects.
            </p>
            <p>
                <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301949">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Web Hosting</h2>
            <p>
                You can easily find a web hosting company that offers the right mix of features and price for your applications.
            </p>
            <p>
                <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301950">Learn more &raquo;</a>
            </p>
        </div>
    </div>--%>

</asp:Content>
