using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WOS_.Models
{
    public class ArticleItem
    {
        string _Title;
        string _Authors;
        string _Publication;
        string _Volume;
        string _Issue;
        string _Pages;
        int _Year;
        int _NumOfCitations;
        bool _FirstNotInHIndex;

        public string Title
        {
            get
            {
                if (_Title == null || _Title == "")
                    _Title = @"N/D*";
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
                if (_Publication == null || _Publication == "")
                    _Publication = @"N/D*";
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
                if (_Volume == null || _Volume == "")
                    _Volume = @"N/D*";
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
                if (_Issue == null || _Issue == "")
                    _Issue = @"N/D*";
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
                if (_Pages == null || _Pages == "")
                    _Pages = @"N/D*";
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
                return string.Format("  Volume: {2} Issue: {3} Pages: {4}",
                _Authors, Publication, Volume.ToString(), Issue.ToString(), Pages);
            }
        }

        public bool FirstNotInHIndex
        {
            get
            {
                return _FirstNotInHIndex;
            }

            set
            {
                _FirstNotInHIndex = value;
            }
        }

        public string YearToString()
        {
            if (this.Year == 0)
                return "-";
            return Year.ToString();
        }

    }
}