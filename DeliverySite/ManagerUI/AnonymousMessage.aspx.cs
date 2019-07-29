using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using DeliverySite.Resources;
using System;

namespace Delivery.ManagerUI
{
    public partial class AnonymousMessage : ManagerBasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            btnSend.Click += bntSend_Click;
            DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            OtherMethods.ActiveRightMenuStyleChanche("hlMain", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlAnonymousMessage", this.Page);
            Page.Title = PagesTitles.ManagerAnonymousMessage + BackendHelper.TagToValue("page_title_part");

            lblEmalList.Text = BackendHelper.TagToValue("anonymous_message_email_list");
        }

        public void bntSend_Click(Object sender, EventArgs e)
        {
            var emailList = BackendHelper.TagToValue("anonymous_message_email_list");
            var emailArray = emailList.Split(new[] { ',' });
            var body = String.Format("Новое анонимное сообщение от сотрудника<br/>" +
                                     "<i>Заголовок: </i>{1}<br/>" +
                                     "<i>Содержание: </i>{0}", tbBody.Text, tbSubject.Text);
            if (emailArray[0].Length != 0)
            {
                EmailMethods.MailSendHTML("Новое анонимное сообщение от сотрудника", body, emailArray);
                Session["flash:now"] = "<span style='color: white; font-size: bold'>Ваше анонимное сообщение успешно отправлено!</span>";
                Page.Response.Redirect("~/ManagerUI/Default.aspx");
            }
            else
            {
                Session["flash:now"] = "<span style='color: red; font-size: bold'>Отсутствуют адресаты. Обратитесь к вашему программисту.</span>";
                Page.Response.Redirect("~/ManagerUI/Default.aspx");
            }
        }
    }
}