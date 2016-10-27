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

        public ActionResult Index(string author, string sortOrder, int? page)
        {
            return View();
        }

        public ActionResult ArticlesLoading(string author,string sortOrder, int? page)
        {
            if (author == null || author == "")
            {
                return PartialView("_ArticlesList", (new ArticleModel()));
            }
            var curAuth = (string)this.Session["CurrentAuthor"];
            if (author != curAuth)
            {
                CurrentArticles = GetArticles(author); 
            } 
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
                this.Session["CitationsSortParam"] = String.IsNullOrEmpty(sortOrder) ? "num_of_citations" : "";
                this.Session["DateSortParm"] = sortOrder == "year" ? "year_desc" : "year";
                switch (sortOrder)
                {
                    case "num_of_citations":
                        articles = CurrentArticles.OrderBy(s => s.NumOfCitations).ToList();
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
                int pageSize = 50;
                int pageNumber = (page ?? 1);
                newArticles.Articles = articles.ToPagedList(pageNumber, pageSize);
                ViewBag.Page = (page ?? 1);
                return PartialView("_ArticlesList",newArticles);
            }
            return PartialView("_ArticlesList", new ArticleModel()
            {
                Articles = articles.ToPagedList(1, 1)
            });
        }

        public List<ArticleItem> GetArticles(string author)
        {
            List<ArticleItem> articles = new List<ArticleItem>();
            ParseHelper helper = new ParseHelper();
            var foundHtmls = WOSWebHelper.ExecuteQuery(author);
            for (int i = 0; i < foundHtmls.Count; i++)
            {
                var html = foundHtmls[i];
                var a = helper.ParseHTML(html);
                articles.AddRange(a);
                helper.startIndex += helper.count;
            }
            return articles;
        }
    }
}