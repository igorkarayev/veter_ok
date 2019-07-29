<%@ Page Title="" Language="C#" MasterPageFile="~/UserMasterPage.master" AutoEventWireup="true" CodeBehind="FeedbackView.aspx.cs" Inherits="Delivery.UserUI.FeedbackView"  Async="true"%>
<%@ Import Namespace="Delivery.BLL.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .feedback-table td {
            padding: 5px 8px;
        }
        .single-input-fild {
            margin-bottom: 0px !important;
        }
    </style>
    <script>
        $(document).ready(function()	{
            $('.bbcode').markItUp(myBBCodeSettings);

            $('.bbcode').keydown(function (e) {
                if (e.ctrlKey && e.keyCode == 13) {
                    $("#<%=btnCreate.ClientID%>").click();
                }
            });
        });
    </script>
    <h3 class="h3custom" style="margin-top: 0;">Просмотр обращения</h3>
    <div class="single-input-fild" style="width: 90%; vertical-align: top;">
        <div style="width: 52%; display: inline-block">
            <div class="form-group">
                <table class="feedback-table">
                    <tr>
                        <td>
                            <i>ID обращения:</i>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblID"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <i>Тема:</i>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblTitle"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <i>Статус:</i>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblStatus"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                           <i>Тип:</i> 
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblType"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <i>Приоритет:</i>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblPriority"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">
                           <i> Содержание:</i>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblBody"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">
                           <i> Создано:</i>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblCreateDate"></asp:Label>
                        </td>
                    </tr>
                    <asp:Panel runat="server" ID="pnlImage" Visible="False">
                        <tr>
                            <td>
                                
                            </td>
                            <td style="vertical-align: top">
                                <asp:HyperLink runat="server" ID="hlImage" Target="_new">
                                    <asp:Image runat="server" ID="imgPhoto" style="width: 100%;"></asp:Image>
                                </asp:HyperLink>
                            </td>
                        </tr>
                    </asp:Panel>
                </table>
            </div>
            
            
        </div>

        <div style="width: 44%; display: inline-block; padding: 0px 0 0 0px; margin-bottom: 50px; vertical-align: top; text-align: left">
            <div style="overflow: hidden; padding: 10px 0;">
                <asp:TextBox runat="server" ID="tbComment" CssClass="multi-control bbcode" TextMode="MultiLine" 
                style="width:93%; height: 100px; margin-bottom: 5px; padding: 10px;"></asp:TextBox>
                <asp:Button ID="btnCreate" runat="server" Text='отправить комментарий' CssClass="btn btn-default" 
                        style="width: 100%" ValidationGroup="LoginGroup"/>
            </div>
            <asp:ListView runat="server" ID="lvAllComments">
                <LayoutTemplate>
                        <div runat="server" id="itemPlaceholder"></div>
                </LayoutTemplate>
                <ItemTemplate>
                    <div id="Tr2" runat="server" class='<%# String.Format("noteDiv feedbackDiv {0}", FeedbackHelper.ColoredCommentManagerUI(Convert.ToInt32(Eval("UserID")))) %>' style="width: 100%; margin-bottom: 10px; font-weight: normal; font-style: normal; text-align: left;">
                        <div class="comment-history-body">
                            <%# Eval("Comment") %>
                        </div>
                        <hr class="styleHR"/>
                        <div class="comment-history-footer">
                            <span class="comment-history-name"><%# FeedbackHelper.FioFilter(Eval("Fio").ToString()) %></span>
                            <span class="comment-history-date"><%# Convert.ToDateTime(Eval("CreateDate")).ToString("dd.MM") %> в <%# Convert.ToDateTime(Eval("CreateDate")).ToString("HH:mm") %></span>
                        </div>
                        
                    </div>
                </ItemTemplate>
       
            </asp:ListView>
        </div>
    </div>
    
    <asp:CustomValidator ID="CustomValidator18" ControlToValidate="tbComment" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели комментарий" ></asp:CustomValidator>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="LoginGroup" ShowMessageBox="true" ShowSummary="false"/>
</asp:Content>
