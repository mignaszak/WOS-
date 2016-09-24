using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WOS_.Models;

namespace WOS_.Helpers
{
    public class StatisticsHelper
    {
        int _SumOfCitations;
        double _AverageNumOfCitations;
        int _HIndex;
        int _ItemsFound;

        public int HIndex
        {
            get
            {
                return _HIndex;
            }

            set
            {
                _HIndex = value;
            }
        }

        public double AverageNumOfCitations
        {
            get
            {
                return _AverageNumOfCitations;
            }

            set
            {
                _AverageNumOfCitations = value;
            }
        }

        public int SumOfCitations
        {
            get
            {
                return _SumOfCitations;
            }

            set
            {
                _SumOfCitations = value;
            }
        }

        public int ItemsFound
        {
            get
            {
                return _ItemsFound;
            }

            set
            {
                _ItemsFound = value;
            }
        }

        public void CalculateStatistics(List<ArticleItem> articles)
        {
            _ItemsFound = articles.Count;
            _SumOfCitations = CalcSumOfCitations(articles);
            _AverageNumOfCitations = CalcAverageCitations(articles);
            _HIndex = CalcHIndex(articles);
        }

        private int CalcSumOfCitations(List<ArticleItem> articles)
        {
            return articles.Select(x => x.NumOfCitations).Sum();
        }

        private double CalcAverageCitations(List<ArticleItem> articles)
        {
            return articles.Select(x => x.NumOfCitations).Average();
        }

        private int CalcHIndex(List<ArticleItem> articles)
        {
            List<int> uniqueCitations = articles.Select(x => x.NumOfCitations).Distinct().ToList();
            List<HIndexTemp> temp = new List<HIndexTemp>();
            foreach (var num in uniqueCitations)
            {
                temp.Add(new HIndexTemp(num, articles.Where(x => x.NumOfCitations == num).Count()));
            }
            temp = temp.OrderBy(x => x.numOfCitations).ToList();
            int hindex = temp[0].numOfCitations;
            for (int i= 0; i < temp.Count;i++)
            {
                var num = temp[i];
                if (num.diff == 0)
                {
                    hindex = num.numOfCitations;
                    break;
                }
                else if (num.diff > 0)
                {
                    hindex = temp[i - 1].numOfCitations;
                    break;
                }
                else if (i + 1 != temp.Count)
                    hindex = temp[i + 1].numOfCitations;
            }
            var artTemp = articles.OrderBy(x => x.NumOfCitations).ToList();
            foreach (var art in artTemp)
            {
                if (art.NumOfCitations == hindex)
                {
                    articles.Where(x => x == art).FirstOrDefault().FirstNotInHIndex = true;
                    break;
                }
            }
            return hindex;
        }

        public class HIndexTemp
        {
            public int numOfCitations { get; set; }
            public int numOfArticles { get; set; }
            public int diff { get; set;  }
            public HIndexTemp(int o, int t)
            {
                numOfCitations = o;
                numOfArticles = t;
                diff = o - t;
            }
        
        }
    }
}