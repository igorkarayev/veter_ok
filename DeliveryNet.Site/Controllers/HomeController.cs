using System;
using DeliveryNet.Interfaces;
using Delivery.ViewModels.Home;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using Delivery.BLL.Helpers;
using Delivery.DAL.DataBaseObjects;
using DeliveryNet.Services;
using PagedList;
using System.Net;
using System.Text;
using Delivery.ViewModels.News;
using DeliveryNet.Data;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using Microsoft.Owin.Security.Twitter.Messages;

namespace Delivery.Controllers
{
    public class HomeController : Controller
    {
        #region Fields

        [Import]
        protected IBackendService BackendService;

        [Import]
        protected IPageService PageService;

        [Import]
        protected ICityService CityService;

        [Import]
        protected IGoodsService GoodsService;

        [Import]
        protected INewsService NewsService;

        [Import]
        protected ITicketsService TicketsService;

        public List<DeliveryNet.Data.News> AllNews;
        List<CityAdditionalInfo> FirstResults;

        #endregion

        #region Constructor

        public HomeController()
        {
        }

        public HomeController(IBackendService backendService, IPageService pageService, ICityService cityService, INewsService newsService, ITicketsService ticketsService)
        {
            BackendService = backendService;
            PageService = pageService;
            CityService = cityService;
            NewsService = newsService;
            TicketsService = ticketsService;
        }
        
        #endregion        

        public ActionResult TestNewLayout()
        {
            var model = new IndexViewModel
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
                PageInfo = PageService.GetByName("default")
            };

            return View(model);
        }

        public ActionResult News(int? page)
        {
            var pageSize = 4;
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
                FooterBody = BackendService.GetValueByTag("footer_body"),
                Paging = true
            };
            AllNews = NewsService.GetAllGuestOrderByCreateDate();
            if(AllNews.Count() <= pageSize)
            {
                model.Paging = false;
            }
            //model.LatestNews = AllNews[0];                          
            //AllNews.RemoveAt(0);
            model.News = AllNews.ToPagedList(pageNumber, pageSize);                
            
            return View(model);
        }

        public ActionResult Index()
        {
            var model = new IndexViewModel
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
                PageInfo = PageService.GetByName("default")
            };
            
            return View(model);
        }

        public ActionResult AboutUs()
        {
            var model = new AboutViewModel()
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
                PageInfo = PageService.GetByName("about")
            };

            return View(model);
        }

        public ActionResult Contacts()
        {
            var model = new ContactsViewModel
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
                PageInfo = PageService.GetByName("contacts")
            };
            return View(model);
        }

        public ActionResult Vacancies()
        {
            var model = new VacanciesViewModel
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
                PageInfo = PageService.GetByName("vacancies")
            };
            return View(model);
        }

        public ActionResult City(CityViewModel city)
        {
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            var model = new CityViewModel
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
                PageInfo = PageService.GetByName("city"),
                City = CityService.GetMain(),
                CityCompanions = CityService.GetCompanions(),
                Id = city.Id,
                RegionId = city.RegionId,
                Value = city.Value,
                Days = city.Days
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult CheckStatus(CityViewModel model)
        {
            
            return RedirectToAction("City", model);
        }
        
        
        public ActionResult AutocompleteCity(string term)
        {
            IEnumerable<CityAdditionalInfo> City = CityService.GetAllCities();
            IEnumerable<DeliveryNet.Data.Goods> Goods = GoodsService.GetAllGoods();

            IEnumerable<CityAdditionalInfo> filteredItems = City.Where(
                item => item.CityName.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0
                ).Take(50);
            
            string days = "";
            foreach(CityAdditionalInfo city in filteredItems)
            {
                city.RegionName = CityAdditionalInfo.GetRegionName(city.RegionId);
                days = "";
                if (city.Monday == 1)
                    days += "пн.,";
                if (city.Tuesday == 1)
                    days += "вт.,";
                if (city.Wednesday == 1)
                    days += "ср.,";
                if (city.Thursday == 1)
                    days += "чт.,";
                if (city.Friday == 1)
                    days += "пт.,";
                if (city.Saturday == 1)
                    days += "сб.,";
                if (city.Sunday == 1)
                    days += "вс.,";
                if (days != "")
                {
                    char y = days[days.Length - 1];
                    if (y == ',')
                    {
                        days = days.Substring(0, days.Length - 1);
                    }
                }
                city.Days = days;
            }
            
            return Json(filteredItems, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AutocompleteGoods(string term)
        {            
            IEnumerable<DeliveryNet.Data.Goods> Goods = GoodsService.GetAllGoods();

            IEnumerable<DeliveryNet.Data.Goods> filteredItems = Goods.Where(
                item => item.Name.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0
                ).Take(10);            

            return Json(filteredItems, JsonRequestBehavior.AllowGet);
        }

        
        public ActionResult Calculate(CityViewModel model)
        {
            var discountCityIdsString = BackendService.GetValueByTag("discount_city_id");
            bool IsDiscountCity = discountCityIdsString.Split(new[] { ',' }).Select(Int32.Parse).ToList().Contains(Convert.ToInt32(model.Id));
            double CoefficientFiz = double.Parse(IsDiscountCity ? BackendService.GetValueByTag("coefficient_fiz_discount_city") : BackendService.GetValueByTag("coefficient_fiz"), CultureInfo.InvariantCulture);
            double result = 0;
            bool error = false;
            string errorString = "";
            if(model.Id == null)
            {
                error = true;
                errorString += "Неверный город ";
            }
            if (model.Coef == null)
            {
                error = true;
                errorString += "Неверный товар";
            }
            if(error == true)
                return Json(errorString);
            
            try
            {
                int stavka = Convert.ToInt32(BackendService.GetValueByTag("baz_stavka"));
                if (model.RegionId == 1)
                    stavka = Convert.ToInt32(BackendService.GetValueByTag("baz_stavka_minsk_region"));
                if (model.Id == 11)
                    stavka = Convert.ToInt32(BackendService.GetValueByTag("baz_stavka_minsk"));
                double coef = double.Parse(model.Coef, CultureInfo.InvariantCulture);
                
                result = CoefficientFiz * ((coef * stavka) + ((coef * stavka * model.Num) - ((coef * stavka))) / 2);
            }
            catch (FormatException)
            {

            }
            result = Math.Round(result, 2);
            string foolResult = result.ToString("0.00");
            foolResult += " руб";
            return Json(foolResult);
        }

        public ActionResult GetStatus(string id)
        {
            //model.Status = TicketsService.GetStatusById(model.ID);
            //return View(model);            
            return Json(TicketsService.GetStatusById(id));
        }
    }
}