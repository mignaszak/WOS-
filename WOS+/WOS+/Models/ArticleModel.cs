using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WOS_.Helpers;
using PagedList;

namespace WOS_.Models
{
   

    public class ArticleModel
    {
        public string Query { get; set; }

        public int PageSieze
        {
            get
            {
                return 50;
            }
        }

        IPagedList<ArticleItem> _Articles;

        StatisticsHelper _Statistics;

        public IPagedList<ArticleItem> Articles
        {
            get
            {
                return _Articles;
            }

            set
            {
                _Articles = value;
            }
        }

        public StatisticsHelper Statistics
        {
            get
            {
                return _Statistics;
            }

            set
            {
                _Statistics = value;
            }
        }

        public ArticleModel()
        {
            _Statistics = new StatisticsHelper();
            _Articles = new List<ArticleItem>().ToPagedList(1, 1);
        }
         
    }
}