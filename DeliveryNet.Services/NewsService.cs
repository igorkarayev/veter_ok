using DeliveryNet.Data;
using DeliveryNet.Data.Context;
using DeliveryNet.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace DeliveryNet.Services
{
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Export(typeof(INewsService))]
    public class NewsService : INewsService
    {

        #region Properties and Fields  

        readonly DeliveryNetContext _context;

        #endregion

        #region Constructors

        [ImportingConstructor]
        public NewsService(DeliveryNetContext ctx)
        {
            _context = ctx;
        }

        #endregion

        public List<News> GetAllGuestOrderByCreateDate()
         {
            return _context.News.Where(u => u.NewsTypeID == 0).OrderByDescending(u => u.CreateDate).ToList();
        }

        public News GetByTitleUrl(string titleUrl)
        {
            return _context.News.FirstOrDefault(u => u.TitleUrl == titleUrl);
        }

        public List<News> GetFirstTwoNews()
        {
            List<News> news = new List<News>();
            List<News> newsAll =  _context.News.Where(u => u.NewsTypeID == 0).OrderByDescending(u => u.CreateDate).ToList();
            news.Add(newsAll[0]);
            news.Add(newsAll[1]);
            return news;
        }
    }
}
