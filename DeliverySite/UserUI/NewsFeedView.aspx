<%@ Page Title="" Language="C#" MasterPageFile="~/UserMasterPage.master" AutoEventWireup="true" CodeBehind="NewsFeedView.aspx.cs" Inherits="Delivery.UserUI.NewsFeedView" %>
<%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h3 class="h3custom" style="margin-top: 0;">Лента новостей</h3>
    </div>
     <asp:ListView runat="server" ID="lvAllNews">
        <LayoutTemplate>
            <div id="itemPlaceholder" runat="server"></div>
        </LayoutTemplate>
        <ItemTemplate>
            <div runat="server" class="newsBlock">
                <table runat="server" id="Table1" style="width: 100%">
                    <tr id="Tr2" runat="server">
                        <td>
                            <div style="font-size: 18px; width: 100%; ">
                                <div style="font-size: 12px; color: #888; display: inline-block; font-weight: bold;">
                                    <asp:Label ID="Label1" runat="server" Text='<%#Convert.ToDateTime(Eval("CreateDate")).ToString("dd.MM.yyyy") %>' /> /
                                </div>
                                <asp:HyperLink ID="HyperLink1" CssClass="blueTitle" runat="server" NavigateUrl='<%#"~/UserUI/NewsFromFeedView.aspx?title="+Eval("TitleUrl") %>'>
                                    <asp:Label ID="lblTitle" runat="server" Text='<%#Eval("Title") %>'/>
                                </asp:HyperLink>
                            </div>
                
                            
                            <div style="width: 100%; text-align: justify;">
                                <div ID="lblBody" runat="server" InnerHTML='<%# OtherMethods.StringCutAllNews(Eval("Body").ToString()) %>' />
                                <span id="Span2" style="width: 100%; text-align: right; margin-top: 8px; float: right;" runat="server" Visible='<%# OtherMethods.ReadMoreVisible(Eval("Body").ToString()) %>'>
                                    <asp:HyperLink ID="HyperLink3" CssClass="read-more" runat="server" NavigateUrl='<%#"~/UserUI/NewsFromFeedView.aspx?title="+Eval("TitleUrl") %>'>читать дальше...</asp:HyperLink>
                                </span>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
                <hr class="styleHR2"/>
        </ItemTemplate>
    </asp:ListView>
    <div class="infoBlock" style="margin-bottom: 0;">
        <div class="pager">
            <asp:Label ID="lblPage" runat="server">Страница:</asp:Label>
            <asp:DataPager ID="lvDataPager" runat="server" PagedControlID="lvAllNews" PageSize="10">
                <Fields>
                    <asp:NumericPagerField ButtonType="Link" />
                </Fields>
            </asp:DataPager>  
        </div>
    </div>
    
    <script>
        var unreadNewsNotDisplay = true;
    </script>

</asp:Content>
