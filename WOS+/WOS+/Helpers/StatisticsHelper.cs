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
            _AverageNumOfCitations = Math.Round(CalcAverageCitations(articles), 2);
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
            int hindex = 0;
            articles = articles.OrderByDescending(x => x.NumOfCitations).ToList();
            for(int i = 0; i < articles.Count; i++)
            {
                if (articles[i].NumOfCitations >= (i + 1))
                    hindex = i + 1;
                else
                    break;
            }
            return hindex;
        }
    }
}