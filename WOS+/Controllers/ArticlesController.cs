using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WOS_.Models;
using WOS_.Helpers;
using PagedList;

namespace WOS_.Controllers
{
    public class ArticlesController : Controller
    {
        static List<ArticleModel> CurrentArticles;

        // GET: Articles
        //public ActionResult Index(string author)
        //{
        //    ViewBag.Authors = WOSHelper.GetArticles(author).OrderBy(x => x.NumOfCitations).ToList(); 

        //    return View();
        //}

        public ActionResult Index(string author, string sortOrder, int? page)
        {
            var curAuth = (string)this.Session["CurrentAuthor"];
            if (author != curAuth || CurrentArticles == null)
            {
                //nowy autor
                //CurrentArticles = WOSApiHelper.GetArticles(author);
                CurrentArticles = WOSWebHelper.GetArticles(author);
                StatisticsHelper.CalculateStatistics(CurrentArticles);
            }
            List<ArticleModel> articles = new List<ArticleModel>();
            foreach (var art in CurrentArticles)
                art.FirstNotInHIndex = false;
            this.Session["CurrentSort"] = sortOrder;
            this.Session["CurrentAuthor"] = author;
            //StateValues.CurrentAuthor = author;
            this.Session["CitationsSortParam"] = String.IsNullOrEmpty(sortOrder) ? "num_of_citations" : "";
            this.Session["DateSortParm"] = sortOrder == "year" ? "year_desc" : "year";
            switch (sortOrder)
            {
                case "num_of_citations":
                    articles = CurrentArticles.OrderBy(s => s.NumOfCitations).ToList();
                    for (int i = 0; i < articles.Count; i++)
                    {
                        if (articles[i].NumOfCitations >= StatisticsHelper.HIndex)
                        {
                            articles[i].FirstNotInHIndex = true;
                            break;
                        }
                    }
                    break;
                case "year":
                    articles = CurrentArticles.OrderBy(s => s.Year).ToList();
                    break;
                case "year_desc":
                    articles = CurrentArticles.OrderByDescending(s => s.Year).ToList();
                    break;
                default:
                    articles = CurrentArticles.OrderByDescending(s => s.NumOfCitations).ToList();                    
                    for(int i =0;i<articles.Count;i++)
                    {
                        if (articles[i].NumOfCitations < StatisticsHelper.HIndex)
                        {
                            articles[i - 1].FirstNotInHIndex = true;
                            break;
                        }
                    }
                    break;
            }
            ViewBag.Articles = articles;

            int pageSize = 50;
            int pageNumber = (page ?? 1);
            return View(articles.ToPagedList(pageNumber, pageSize));

            //return View(); 
        }
    }
}