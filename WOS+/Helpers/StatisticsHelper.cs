using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WOS_.Models;

namespace WOS_.Helpers
{
    public class StatisticsHelper
    {
        static int _SumOfCitations;
        static double _AverageNumOfCitations;
        static int _HIndex;
        static int _ItemsFound;

        public static int HIndex
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

        public static double AverageNumOfCitations
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

        public static int SumOfCitations
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

        public static int ItemsFound
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

        public static void CalculateStatistics(List<ArticleModel> articles)
        {
            _ItemsFound = articles.Count;
            _SumOfCitations = StatisticsHelper.CalcSumOfCitations(articles);
            _AverageNumOfCitations = StatisticsHelper.CalcAverageCitations(articles);
            _HIndex = StatisticsHelper.CalcHIndex(articles);
        }

        private static int CalcSumOfCitations(List<ArticleModel> articles)
        {
            return articles.Select(x => x.NumOfCitations).Sum();
        }

        private static double CalcAverageCitations(List<ArticleModel> articles)
        {
            return articles.Select(x => x.NumOfCitations).Average();
        }

        private static int CalcHIndex(List<ArticleModel> articles)
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