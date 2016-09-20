using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WOS_.Models;

namespace WOS_.Helpers
{
    class ParseHelper
    {
        string authorsAbrXPath;
        string authorsExpXPath;
        string publicationXPath;
        string titleXPath1;
        string titleXPath2;
        string yearXPathWhite;
        string yearXPathGrey;
        string volumeXPathWhite;
        string volumeXPathGrey;
        string issueXPathWhite;
        string issueXPathGrey;
        string pageXPathWhite;
        string pageXPathGrey;
        string citationsXPathWhite;
        string citationsXPathGrey;
        public int startIndex;
        public int count;

        static string pagesCountXPath = "//*[@id='pageCount.top']";

        public ParseHelper()
        {

            authorsAbrXPath = "//*[@id='author_abr_{0}']";
            authorsExpXPath = "//*[@id='author_exp_{0}']";
            publicationXPath = "//*[@id='cited_work_exp_{0}']/text()";

            titleXPath1 = "//*[@id='links_isi_product_{0}']/value/text()";
            titleXPath1 = "//*[@id='cited_work_exp_{0}']";
            titleXPath2 = "//*[@id='cited_work_exp_{0}']/a/text()";

            yearXPathWhite = "//*[@class='citedRefTableRow1'][3]";
            yearXPathGrey = "//*[@class='citedRefTableRow2'][3]";

            volumeXPathWhite = "//*[@class='citedRefTableRow1'][4]";
            volumeXPathGrey = "//*[@class='citedRefTableRow2'][4]";
            issueXPathWhite = "//*[@class='citedRefTableRow1'][5]";
            issueXPathGrey = "//*[@class='citedRefTableRow2'][5]";
            pageXPathWhite = "//*[@class='citedRefTableRow1'][6]";
            pageXPathGrey = "//*[@class='citedRefTableRow2'][6]";
            citationsXPathWhite = "//*[@class='citedRefTableRow1'][8]";
            citationsXPathGrey = "//*[@class='citedRefTableRow2'][8]";

            startIndex = 1;
        }

        public List<ArticleModel> ParseHTML(string html)
        {
            List<ArticleModel> articles = new List<ArticleModel>();
            List<string> authors = GetAuthors(html);
            List<string> pubs = GetPublications(html);
            List<string> titles = GetTitles(html);
            List<string> years = GetElements(html, yearXPathWhite, yearXPathGrey);
            List<string> voulmes = GetElements(html, volumeXPathWhite, volumeXPathGrey);
            List<string> issues = GetElements(html, issueXPathWhite, issueXPathGrey);
            List<string> page = GetElements(html, pageXPathWhite, pageXPathGrey);
            List<string> citations = GetElements(html, citationsXPathWhite, citationsXPathGrey);
            //&nbsp;
            for (int i = 0; i < count; i++)
            {
                var art = new ArticleModel();
                art.Authors = authors[i];
                art.Publication = pubs[i];
                art.Title = titles[i];
                art.Year = years[i] != null && years[i] != "" ? int.Parse(years[i].Replace("&nbsp;", "0")) : 0;
                art.Volume = voulmes[i].Replace("&nbsp;", string.Empty);
                art.Issue = issues[i].Replace("&nbsp;", string.Empty);
                art.Pages = page[i].Replace("&nbsp;", string.Empty);
                art.NumOfCitations = citations[i] != null && citations[i] != "" ? int.Parse(citations[i].Replace("&nbsp;", "0")) : 0;
                articles.Add(art);
            }
            return articles;
        }

        List<string> GetAuthors(string html)
        {
            List<string> authors = new List<string>();
            HtmlDocument document = new HtmlDocument();
            int authorCount = 0;
            HtmlNode node = null;
            document.LoadHtml(html);
            var asd = document.DocumentNode.SelectNodes("//*[@class='citedRefTableRow1'][1]");
            do
            {
                node = document.DocumentNode.SelectSingleNode(string.Format(authorsExpXPath, startIndex + authorCount));//.FirstOrDefault();
                if (node == null)
                    node = document.DocumentNode.SelectSingleNode(string.Format(authorsAbrXPath, startIndex + authorCount));
                if (node == null)
                    break;
                authors.Add(node.InnerText);
                authorCount++;
            } while (node != null);
            count = authorCount;
            return authors;
        }

        List<string> GetPublications(string html)
        {
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);
            List<string> elems = new List<string>();
            for (int i = 0; i < count; i++)
            {
                if (i == 51)
                {

                }
                var node = document.DocumentNode.SelectSingleNode(string.Format(publicationXPath, i + startIndex));//.FirstOrDefault(); 
                if (node == null)
                    elems.Add("");
                else
                    elems.Add(node.InnerText);


                // //*[@id="cited_work_exp_10"]/a/text()
            }
            return elems;
        }

        List<string> GetTitles(string html)
        {
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);
            List<string> titles = new List<string>();
            for (int i = 0; i < count; i++)
            {
                var node = document.DocumentNode.SelectSingleNode(string.Format(titleXPath1, i + startIndex));
                if (node == null)
                    node = document.DocumentNode.SelectSingleNode(string.Format(titleXPath2, i + startIndex));


                if (node == null)
                    titles.Add("");
                else
                {
                    titles.Add(string.Join(" ", node.ChildNodes.Select(x => x.InnerText)));

                    //titles.Add(node.InnerText);
                }

                // //*[@id="cited_work_exp_10"]/a/text()
            }
            return titles;
        }


        List<string> GetElements(string html, string whitXPath, string greyXPath)
        {
            try
            {
                List<string> white = new List<string>();
                List<string> grey = new List<string>();
                HtmlDocument document = new HtmlDocument();
                document.LoadHtml(html);

                var node = document.DocumentNode.SelectNodes(whitXPath);
                //var whiteTemp = node.ToList();
                if (node != null && node.Count() != 0)
                    white = node.ToList().Select(x => x.InnerText).ToList();

                node = document.DocumentNode.SelectNodes(greyXPath);
                //var greyTemp = node.ToList();
                if (node != null && node.Count() != 0)
                    grey = node.ToList().Select(x => x.InnerText).ToList();

                var list = ZipLists(white, grey);

                return list;
            }
            catch (Exception e)
            {

                throw new Exception(string.Format("Error: {0}\r\n withXPath {1} greyXPath {2}", e.Message, whitXPath, greyXPath));
            }
        }

        List<T> ZipLists<T>(List<T> list1, List<T> list2)
        {
            List<T> list = new List<T>();
            int count = (new List<int>() { list1.Count, list2.Count }).Min();
            bool first = true;
            for (int i = 0; i < count; i++)
            {
                list.Add(list1[i]);
                list.Add(list2[i]);
            }
            List<T> temp = new List<T>();
            if (list1.Count > list2.Count)
            {
                //list1 is bigger
                temp = list1.GetRange(list2.Count, list1.Count - list2.Count);
                list.AddRange(temp);
            }
            else if (list2.Count > list1.Count)
            {
                //list2 is bigger
                temp = list1.GetRange(list2.Count, list1.Count - list2.Count);
                list.AddRange(temp);
            }

            return list;
        }

        public static int GetPagesCount(string html)
        {
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);
            var node = document.DocumentNode.SelectSingleNode(pagesCountXPath);
            string count = node.InnerText;
            return count != null && count != "" ? int.Parse(count.Replace("&nbsp;", "0")) : 0;
        }
    }
}