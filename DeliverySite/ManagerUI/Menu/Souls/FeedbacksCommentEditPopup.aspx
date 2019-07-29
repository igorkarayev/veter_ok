<%@ Page Title="" Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="FeedbacksCommentEditPopup.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Souls.FeedbacksCommentEditPopup" Async="true" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="Delivery.BLL.Helpers" %>
<!DOCTYPE html>
<html>
    <head id="Head1" runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title>Редактирование комментария</title>
        <%: Styles.Render("~/styles/site") %>
        <link rel="icon" type="image/png" href="<%=ResolveClientUrl("~/Styles/Images/favicon.png") %>" />
        <style>
            body {
                background-color: white;
                background-image: none;
                width: 500px;
                padding-top: 15px;
            }
            #btnSave:hover {
                text-decoration: none !important;
            }
        </style>
        <%: Scripts.Render("~/js/jquery") %>
    </head>
    <body>
    <script>
        function go() {
            $.ajax({
                type: "POST",
                url: "../../../AppServices/SaveAjaxService.asmx/SaveFeedbackComment",
                data: ({
                    appkey: "<%= AppKey %>",
                    comment: $('#<%= tbComment.ClientID%>').val(),
                    commentId: "<%= CommentId %>",
                    currentUserId: "<%= CurrentUserId %>"
                }),
                success: function (response) {
                    setTimeout(refreshAndClose, 200);
                },
                error: function (result) {
                    alert("Ошибка... Попробуйте позже...");
                }
            });
        }

        function refreshAndClose() {
            window.opener.location.reload(true);
            window.close();
        }
    </script>
        <asp:Panel runat="server" ID="pnlError" Visible="False" style="color: red; text-align: center">У вас нет доступа на редактирование данного комментария!</asp:Panel>
        <form id="Main2" runat="server" action="#">
            <div style="width: 500px; margin: 0 auto 0 auto;">
                
            </div>
            <asp:TextBox runat="server" ID="tbComment" TextMode="MultiLine" style="width: 96%; height: 225px;" CssClass="form-control"></asp:TextBox>
            <a href="#" ID="btnSave" runat="server" class="btn btn-default" style="width: 96%"  onclick="go(); return false;">сохранить</a>
        </form>
    </body>
</html>

