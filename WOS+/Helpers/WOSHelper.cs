using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WOS_.Models;

namespace WOS_.Helpers
{
    public class WOSApiHelper
    {
        //Deprecated
        public static List<AuthorModel> GetAuthors(string searchString)
        {
            List<AuthorModel> authors = new List<AuthorModel>()
            {
                new AuthorModel() {Name = "Autor", Surname = "Jeden", Organization = "Uniwersytet 1", Id = "autor 1"},
                new AuthorModel() {Name = "Autor", Surname = "Dwa", Organization = "Uniwersytet 2", Id = "autor 2"},
                new AuthorModel() {Name = "Autor", Surname = "Trzy", Organization = "Uniwersytet 3", Id = "autor 3"}
            };
            if (!String.IsNullOrEmpty(searchString))
            {
                string nameToSearch = searchString.Split(' ')[0].ToLower();
                if (searchString.Trim().Contains(" "))
                {
                    string surnameLetterToSearch = searchString.TrimEnd().Last().ToString().ToLower();
                    authors = authors.Where(X => X.Name.ToLower() == nameToSearch && X.Surname[0].ToString().ToLower() == surnameLetterToSearch).ToList();
                }
                else
                {
                    authors = authors.Where(X => X.Name.ToLower() == nameToSearch).ToList();
                }
            }
            return authors;
        }

        public static List<ArticleModel> GetArticles(string searchString)
        {
            //string path = Environment.CurrentDirectory + @"\test2.xml";
            //return GetArticlesFromFile(path);
            string path = @"C:\Program Files (x86)\IIS Express\text_xml";
            List<ArticleModel> articles = new List<ArticleModel>();
            foreach (var p in Directory.GetFiles(path))
            {
                articles.AddRange(GetArticlesFromFile(p));
            }
            return articles;
        }

        private static List<ArticleModel> GetArticlesFromFile(string path)
        {
            XMLHelper xmlhelper = new XMLHelper();
            List<ArticleModel> articles = new List<ArticleModel>();
            if (File.Exists(path))
            {
                var recrod = xmlhelper.DesrializeXML<records>(File.ReadAllText(path));
                foreach (var rec in recrod.REC)
                {
                    //deprecated
                    //articles.Add(rec);
                }
            }

            return articles;

        }
    }
}