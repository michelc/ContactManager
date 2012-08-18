<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ContactManager.Models.Group>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index</h2>

    <%= Html.ValidationSummary() %>

    <table>
        <tr>
            <th class="actions delete">
                Delete
            </th>
            <th>
                Name
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td class="actions delete">
                <a href='<%= Url.Action("Delete", new {id=item.Id}) %>'><img src="../../Content/Delete.png" alt="Delete" /></a>
            </td>
            <td>
                <%= Html.Encode(item.Name) %>
            </td>
        </tr>
    
    <% } %>

        <tr>
            <% using (Html.BeginForm("Create", "Group")) { %>
            <th>
                Add
            </th>
            <th>
                <%= Html.TextBox("Name") %>
                <input type="submit" value="Create" />
                <%= Html.ValidationMessage("Name", "*") %>
            </th>
            <% } %>
        </tr>

    </table>

</asp:Content>

