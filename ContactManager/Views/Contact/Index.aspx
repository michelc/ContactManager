<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.ViewData.IndexModel>" %>
<%@ Import Namespace="Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script type="text/javascript">

    $(document).ready(function() {

        // Ajax Loading
        $("#leftColumn li a").click(function() {
            $("#leftColumn li").removeClass('selected');
            $(this).parent().addClass('selected');
            var url = $(this).attr("href");
            $("#divContactList")
                .fadeOut()
                .load(url, function() {
                    $(this).fadeIn();
                    BindDelete();
                });
            return false;
        });

        // Ajax Deletes on page loading
        BindDelete();

    });

    function BindDelete() {
        // Ajax Deletes
        $(".delete a").click(function() {
            var answer = confirm('Delete contact?');
            if (answer == true) {
                var url = $(this).attr("href");
                $("#divContactList")
                    .fadeOut()
                    .html($.ajax({
                        type: "DELETE",
                        url: url,
                        cache: false,
                        async: false
                    }).responseText)
                    .fadeIn();
                    BindDelete();
            }
            return false;
        });
    }    

</script>

<div class="container">
    
    <ul id="leftColumn">
    <% foreach (var item in Model.Groups) { %>
        <li<%= Html.Selected(item.Id, Model.SelectedGroup.Id) %>>
            <%= Html.ActionLink(item.Name, "Index", new { id = item.Id }) %>
        </li>
    <% } %>
    </ul>

    <div id="divContactList">
        <% Html.RenderPartial("ContactList", Model.SelectedGroup); %>
    </div>
    
    <div class="clear"></div>

</div>

</asp:Content>