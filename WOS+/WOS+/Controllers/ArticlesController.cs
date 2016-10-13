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
        static List<ArticleItem> CurrentArticles;

        // GET: Articles
        //public ActionResult Index(string author)
        //{
        //    ViewBag.Authors = WOSHelper.GetArticles(author).OrderBy(x => x.NumOfCitations).ToList(); 

        //    return View();
        //}

        

        public ActionResult Index(string author, string sortOrder, int? page)
        {
            //ViewBag.LoadingInfo = "Downloading pages...";
            return View();
            //Stare działające
            var curAuth = (string)this.Session["CurrentAuthor"];
            var tempCurrentArticles = new ArticleModel();
            if (author == null || author == "")
            {
                return View(new ArticleModel());
            }
            else if (author != curAuth)// || CurrentArticles == null)
            {
                //nowy autor
                //CurrentArticles = WOSApiHelper.GetArticles(author);
                //CurrentArticles = new ArticleModel();
                CurrentArticles = WOSWebHelper.GetArticles(author);
                //CurrentArticles.Statistics.CalculateStatistics(CurrentArticles.Articles);
            }
            List<ArticleItem> articles = new List<ArticleItem>();
            if (CurrentArticles != null && CurrentArticles.Count != 0)
            {
                ArticleModel newArticles = new ArticleModel();
                newArticles.Statistics.CalculateStatistics(CurrentArticles);
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
                            if (articles[i].NumOfCitations >= newArticles.Statistics.HIndex)
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
                        for (int i = 0; i < articles.Count; i++)
                        {
                            if (articles[i].NumOfCitations < newArticles.Statistics.HIndex)
                            {
                                articles[i].FirstNotInHIndex = true;
                                break;
                            }
                        }
                        break;
                }
                //ViewBag.Articles = articles;
                int pageSize = 50;
                int pageNumber = (page ?? 1);
                newArticles.Articles = articles.ToPagedList(pageNumber, pageSize);
                ViewBag.Page = (page ?? 1);
                return View(newArticles);
                //return View(articles.ToPagedList(pageNumber, pageSize));
            }
            //return View(articles.ToPagedList(1, 1));       

            return View(new ArticleModel()
            {
                Articles = articles.ToPagedList(1, 1),
                Statistics = new StatisticsHelper()
            });

            //return View(); 
        }

        public ActionResult ArticlesLoading(string author,string sortOrder, int? page)
        {
            var curAuth = (string)this.Session["CurrentAuthor"];
            var tempCurrentArticles = new ArticleModel();
            if (author == null || author == "")
            {
                return PartialView("_ArticlesList", (new ArticleModel()));
            }
            else if (author != curAuth)// || CurrentArticles == null)
            {
                //nowy autor
                //CurrentArticles = WOSApiHelper.GetArticles(author);
                //CurrentArticles = new ArticleModel();
                CurrentArticles = GetArticles(author); 
                //CurrentArticles.Statistics.CalculateStatistics(CurrentArticles.Articles);
            }
            //ViewBag.LoadingInfo = "Loading...";
            List<ArticleItem> articles = new List<ArticleItem>();
            if (CurrentArticles != null && CurrentArticles.Count != 0)
            {
                ArticleModel newArticles = new ArticleModel();
                newArticles.Statistics.CalculateStatistics(CurrentArticles);
                newArticles.Query = author;
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
                        //for (int i = 0; i < articles.Count; i++)
                        //{
                        //    if (articles[i].NumOfCitations >= newArticles.Statistics.HIndex)
                        //    {
                        //        articles[i].FirstNotInHIndex = true;
                        //        break;
                        //    }
                        //}
                        break;
                    case "year":
                        articles = CurrentArticles.OrderBy(s => s.Year).ToList();
                        break;
                    case "year_desc":
                        articles = CurrentArticles.OrderByDescending(s => s.Year).ToList();
                        break;
                    default:
                        articles = CurrentArticles.OrderByDescending(s => s.NumOfCitations).ToList();
                        for (int i = 0; i < articles.Count; i++)
                        {
                            if ((i+1) >  newArticles.Statistics.HIndex)
                            {
                                articles[i].FirstNotInHIndex = true;
                                break;
                            }
                        }
                        break;
                } 
                //ViewBag.Articles = articles;
                int pageSize = 50;
                int pageNumber = (page ?? 1);
                newArticles.Articles = articles.ToPagedList(pageNumber, pageSize);
                ViewBag.Page = (page ?? 1);
                return PartialView("_ArticlesList",newArticles);
                //return View(articles.ToPagedList(pageNumber, pageSize));
            }
            //return View(articles.ToPagedList(1, 1));       

            return PartialView("_ArticlesList", new ArticleModel()
            {
                Articles = articles.ToPagedList(1, 1),
                Statistics = new StatisticsHelper()
            });

            //return View(); 
        }

        public List<ArticleItem> GetArticles(string author)
        {
            //ViewBag.LoadingInfo = "Downloading pages...";
            List<ArticleItem> articles = new List<ArticleItem>();
            ParseHelper helper = new ParseHelper();
            var foundHtmls = WOSWebHelper.ExecuteQuery(author);
            for (int i = 0; i < foundHtmls.Count; i++)
            {
                //ViewBag.LoadingInfo = string.Format("Parsing page {0}/{1}", i + 1, foundHtmls.Count);
                var html = foundHtmls[i];
                if (html == foundHtmls.Last())
                {

                }
                var a = helper.ParseHTML(html);
                articles.AddRange(a);
                helper.startIndex += helper.count;
            }
            return articles;

        }
    }
}