using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WOSApi.Model
{
    public class ArticleModel
    {
        string _Title;
        string _Authors;
        string _Publication;
        string _Volume;
        string _Issue;
        string _Pages;
        int _Year;
        int _NumOfCitations;

        public string Title
        {
            get
            {
                return _Title;
            }

            set
            {
                _Title = value;
            }
        }

        public string Authors
        {
            get
            {
                return _Authors;
            }

            set
            {
                _Authors = value;
            }
        }

        public string Publication
        {
            get
            {
                if (_Publication == null)
                    _Publication = "ND";
                return _Publication;
            }

            set
            {
                _Publication = value;
            }
        }

        public string Volume
        {
            get
            {
                if (_Volume == null)
                    _Volume = "ND";
                return _Volume;
            }

            set
            {
                _Volume = value;
            }
        }

        public string Issue
        {
            get
            {
                if (_Issue == null)
                    _Issue = "ND";
                return _Issue;
            }

            set
            {
                _Issue = value;
            }
        }

        public string Pages
        {
            get
            {
                if (_Pages == null)
                    _Pages = "ND";
                return _Pages;
            }

            set
            {
                _Pages = value;
            }
        }

        public int Year
        {
            get
            {
                return _Year;
            }

            set
            {
                _Year = value;
            }
        }

        public int NumOfCitations
        {
            get
            {
                return _NumOfCitations;
            }

            set
            {
                _NumOfCitations = value;
            }
        }

        public string ArticleInfo
        {
            get
            {
                //return string.Format("BY: {0}\r\n{1} Volume: {2} Issue: {3} Pages: {4} Published: {5}",
                //string.Join("; ", _Authors), Publication, Volume.ToString(), Issue.ToString(), Pages, Year.ToString());
                return string.Format("{1} Volume: {2} Issue: {3} Pages: {4} Published: {5}",
                _Authors, Publication, Volume.ToString(), Issue.ToString(), Pages, Year.ToString());
            }
        }

        public static implicit operator ArticleModel(Rec REC)
        {
            ArticleModel article = new ArticleModel();
            //article.Authors = REC.static_data.summary.names.name.Select(x => x.display_name).ToArray();
            article.Publication = REC.static_data.summary.titles.title.Where(x => x.type == "source").Select(x => x.Value).FirstOrDefault().ToString();
            try
            {
                article.Authors = string.Join(" ; ", REC.static_data.summary.names.name.Select(x => x.full_name));
            }
            catch (Exception)
            {
                article.Authors = "";
            }
            article.Title = REC.static_data.summary.titles.title.Where(x => x.type == "item").Select(x => x.Value).FirstOrDefault().ToString();
            article.Year = REC.static_data.summary.pub_info.pubyear;
            article.Volume = REC.static_data.summary.pub_info.vol;
            article.Issue = REC.static_data.summary.pub_info.issue;
            if (REC.static_data.summary.pub_info.page.begin != null && REC.static_data.summary.pub_info.page.end != null)
                article.Pages = string.Format("{0} - {1}", REC.static_data.summary.pub_info.page.begin.ToString(), REC.static_data.summary.pub_info.page.end.ToString());
            article.NumOfCitations = REC.dynamic_data.citation_related.tc_list.silo_tc.local_count;
            return article;
        }
    }
}
