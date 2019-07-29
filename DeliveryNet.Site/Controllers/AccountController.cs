using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;
using Delivery.ViewModels.Account;
using DeliveryNet.Data;
using DeliveryNet.Interfaces;
using DeliveryNet.Services;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using CaptchaMvc.HtmlHelpers;
using reCAPTCHA.MVC;

namespace Delivery.Controllers
{
    public class AccountController : Controller
    {
        #region Fields
        [Import]
        protected IBackendService BackendService;

        [Import]
        protected IPageService PageService;

        [Import]
        protected IUserService UserService;

        private string errorMessage = "";
        #endregion

        #region Constructor
        public AccountController()
        {
            
        }

        public AccountController(IBackendService backendService, IPageService pageService, IUserService userService)
        {
            BackendService = backendService;
            PageService = pageService;
            UserService = userService;
        }
        #endregion
                
        // GET: Account/Register
        public ActionResult Register()
        {
            var model = new AccountViewModel
            {
                HeaderText = BackendService.GetValueByTag("site_header_text"),
                FooterAddress = BackendService.GetValueByTag("official_address"),
                FooterRequisites = BackendService.GetValueByTag("official_requisites"),
                MainTitle = BackendService.GetValueByTag("not_official_name"),
                FooterEmail = BackendService.GetValueByTag("main_email"),
                FooterPhones = BackendService.GetValueByTag("main_phones"),
                DeliveryNetVersion = BackendService.GetValueByTag("current_server_version"),
                CabinetLink = BackendService.GetValueByTag("current_admin_app_address"),
                Slogan = BackendService.GetValueByTag("slogan_title"),
                FooterBody = BackendService.GetValueByTag("footer_body"),
                Skype = BackendService.GetValueByTag("main_skype"),
                PageInfo = PageService.GetByName("register"),
                UserModel = new Customer()
            };

            return View(model);
        }

        // POST: Account/Register
        [HttpPost]
        [CaptchaValidator]
        public ActionResult Register(Customer user, bool captchaValid)
        {
            try
            {
                var model = new AccountViewModel
                {
                    HeaderText = BackendService.GetValueByTag("site_header_text"),
                    FooterAddress = BackendService.GetValueByTag("official_address"),
                    FooterRequisites = BackendService.GetValueByTag("official_requisites"),
                    MainTitle = BackendService.GetValueByTag("not_official_name"),
                    FooterEmail = BackendService.GetValueByTag("main_email"),
                    FooterPhones = BackendService.GetValueByTag("main_phones"),
                    DeliveryNetVersion = BackendService.GetValueByTag("current_server_version"),
                    CabinetLink = BackendService.GetValueByTag("current_admin_app_address"),
                    Slogan = BackendService.GetValueByTag("slogan_title"),
                    FooterBody = BackendService.GetValueByTag("footer_body"),
                    Skype = BackendService.GetValueByTag("main_skype"),
                    PageInfo = PageService.GetByName("register"),
                    UserModel = user
                };

                /*if (!this.IsCaptchaValid("Captcha is not valid"))
                {
                    ViewData["ErrorMessage"] = "Неверный ввод капчи";
                    return View(model);
                }*/

                if (ModelState.IsValid)
                {
                    user.Password = OtherMethods.HashPassword(user.Password);
                    var res = UserService.AddUser(user);

                    if (res == "0")
                        return RedirectToAction("Success");
                    else
                    {
                        ViewData["ErrorMessage"] = res;
                        return View(model);
                    }
                }
                else
                {
                    ViewData["ErrorMessage"] = "Проверьте правильность заполнения данных";
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Success()
        {
            var model = new AccountViewModel
            {
                HeaderText = BackendService.GetValueByTag("site_header_text"),
                FooterAddress = BackendService.GetValueByTag("official_address"),
                FooterRequisites = BackendService.GetValueByTag("official_requisites"),
                MainTitle = BackendService.GetValueByTag("not_official_name"),
                FooterEmail = BackendService.GetValueByTag("main_email"),
                FooterPhones = BackendService.GetValueByTag("main_phones"),
                DeliveryNetVersion = BackendService.GetValueByTag("current_server_version"),
                CabinetLink = BackendService.GetValueByTag("current_admin_app_address"),
                Slogan = BackendService.GetValueByTag("slogan_title"),
                FooterBody = BackendService.GetValueByTag("footer_body"),
                Skype = BackendService.GetValueByTag("main_skype"),
                PageInfo = PageService.GetByName("success"),
                UserModel = new Customer()
            };
            return View("RegisterMessage", model);
        }
    }
}
