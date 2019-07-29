using DeliveryNet.Interfaces;
using Delivery.ViewModels.News;
using PagedList;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using DeliveryNet.Data;

namespace Delivery.Controllers
{
    public class NewsController : Controller
    {
        #region Fields

        [Import]
        protected IBackendService BackendService;

        [Import]
        protected INewsService NewsService;

        public List<DeliveryNet.Data.News> BuffAllNews;
        public News BuffNews;

        #endregion

        #region Constructor

        public NewsController()
        {
        }

        public NewsController(IBackendService backendService, INewsService newsService)
        {
            BackendService = backendService;
            NewsService = newsService;
        }

        #endregion

        
        

        public ActionResult Stream(int? page)
        {
            var pageSize = 20;
            var pageNumber = (page ?? 1);
            var model = new StreamViewModel()
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
                Skype = BackendService.GetValueByTag("main_skype"),
                FooterBody = BackendService.GetValueByTag("footer_body")
            };
            model.News = NewsService.GetAllGuestOrderByCreateDate().ToPagedList(pageNumber, pageSize);
            return PartialView(model);
        }



        public ActionResult StreamTwoNews(int? page)
        {
            var pageSize = 2;
            var pageNumber = 1;
            var model = new StreamViewModel()
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
                Skype = BackendService.GetValueByTag("main_skype"),
                FooterBody = BackendService.GetValueByTag("footer_body")
            };           

            model.News = NewsService.GetAllGuestOrderByCreateDate().ToPagedList(pageNumber, pageSize);
            
            return PartialView(model);
        }

        public ActionResult Details(string titleUrl)
        {
            var model = new DetailsViewModel()
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
                Skype = BackendService.GetValueByTag("main_skype"),
                FooterBody = BackendService.GetValueByTag("footer_body")
            };

            BuffAllNews = NewsService.GetAllGuestOrderByCreateDate();
            model.News = NewsService.GetByTitleUrl(titleUrl);
            BuffNews = BuffAllNews.Where(i => i == model.News).FirstOrDefault();
            int index = BuffAllNews.IndexOf(BuffNews);
            BuffAllNews.RemoveAt(index);
            model.AllNews = BuffAllNews.ToPagedList(1, 4);
            return View(model);
        }
    }
}