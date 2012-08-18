<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.Group>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Delete
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Delete</h2>

    <p>
    Are you sure that you want to delete the entry for
    <%= Model.Name %>
    </p>

    <% using (Html.BeginForm(new { Id = Model.Id }))
       { %>
       <p> 
            <input type="submit" value="Delete" /> &nbsp; <%=Html.ActionLink("Cancel", "Index") %>
        </p>
    <% } %>

</asp:Content>
