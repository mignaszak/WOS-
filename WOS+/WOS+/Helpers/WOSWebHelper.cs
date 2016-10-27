using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WOS_.Models;

namespace WOS_.Helpers
{
    public class WOSWebHelper
    {
        public static List<string> ExecuteQuery(string query)
        {
            List<string> htmls = new List<string>();
            //Test
            if (Properties.Settings.Default.test)
            {
                foreach (var file in Directory.GetFiles(Properties.Settings.Default.testFiles))
                {
                    htmls.Add(File.ReadAllText(file));
                }
                return htmls; 
            }
            
            HttpWebResponse response = null;
            int tries = 0;
            string str = "";
            string baseURI = "";
            string sid = "";
            //Getting First Page
            do
            {
                sid = GetSID();
                string postData = string.Format("fieldCount=3&action=search&product=UA&search_mode=CitedReferenceSearch&SID={0}&max_field_count=25&max_field_notice=Notice%3A+You+cannot+add+another+field.&input_invalid_notice=Search+Error%3A+Please+enter+a+search+term.&exp_notice=Search+Error%3A+Patent+search+term+could+be+found+in+more+than+one+family+%28unique+patent+number+required+for+Expand+option%29+&input_invalid_notice_limits=+%3Cbr%2F%3ENote%3A+Fields+displayed+in+scrolling+boxes+must+be+combined+with+at+least+one+other+search+field.&sa_params=UA%7C%7CX2xo9BBvfAf132c3qMF%7Chttps%3A%2F%2Fapps.webofknowledge.com%3A443%7C%27&formUpdated=true&value%28input1%29={1}&value%28select1%29=CAU&value%28hidInput1%29=&value%28bool_1_2%29=AND&value%28input2%29=&value%28select2%29=CW&value%28hidInput2%29=&value%28bool_2_3%29=AND&value%28input3%29=&value%28select3%29=CY&x=692&y=510&value%28hidInput3%29=&limitStatus=collapsed&ss_lemmatization=On&ss_spellchecking=Suggest&SinceLastVisit_UTC=&SinceLastVisit_DATE=&period=Range+Selection&range=ALL&startYear=1864&endYear=2016&update_back2search_link_param=yes&ssStatus=display%3Anone&ss_query_language=auto&rs_sort_by=PY.D%3BLD.D%3BSO.A%3BVL.D%3BPG.A%3BAU.A", sid, GetProperQuery(query)); //All databases
                response = PostPage(sid, postData, @"https://apps.webofknowledge.com/UA_CitedReferenceSearch.do");  //All databases
                str = new StreamReader(response.GetResponseStream()).ReadToEnd();
                baseURI = response.ResponseUri.AbsoluteUri;
                tries++;
            } while (str.Contains("searchErrorMessage") && str.Contains("searchErrorMessage") && tries < 10);
            if (tries == 10)
            {
                throw new Exception("Download first page error. Ten tries without succes");
            }
            int pagesCount = ParseHelper.GetPagesCount(str);
            htmls.Add(str);
            //Getting next pages
            for (int i = 2; i <= pagesCount; i++)
            {
                do
                {
                    response = GetPage(string.Format(@"{0}&page={1}", baseURI, i.ToString()));
                    str = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    tries++;
                } while (str.Contains("searchErrorMessage") && tries < 10);
                if (str.Contains("searchErrorMessage") && tries == 10)
                {
                    throw new Exception("Download page " + i.ToString() + " error. Ten tries without succes");
                }
                htmls.Add(str);
            }
            return htmls;
        }

        static string GetProperQuery(String query)
        {
            string realQuery = "";
            var queris = query.Split(' ');
            List<string> realQuieris = new List<string>();
            realQuieris.AddRange(queris.Where(x => x != " " && x != string.Empty));
            realQuery = string.Join("+", realQuieris);
            return realQuery;
        }

        static string GetSID()
        {
            var response = GetPage(@"https://www.webofknowledge.com?");
            return ParseSID(response.ResponseUri.Query);
        }

        static private string ParseSID(string query)
        {
            int start = query.IndexOf("SID=") + string.Format("SID=").Length;
            int end = query.Substring(start).IndexOf('&');
            return query.Substring(start, end);
        }

        static private HttpWebResponse GetPage(string uri)
        {
            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            request.CookieContainer = new CookieContainer();
            Uri target = new Uri("http://www.google.com/");
            request.CookieContainer.Add(new Cookie("CUSTOMER", "ICM Warsaw")
            {
                Domain = target.Host
            });
            request.CookieContainer.Add(new Cookie("E_GROUP_NAME", "Technical University Poznan")
            {
                Domain = target.Host
            });


            var response = (HttpWebResponse)request.GetResponse();

            return response;
        }

        static private HttpWebResponse PostPage(string sid, string postData, string uri)
        {
            var request = (HttpWebRequest)WebRequest.Create(uri);
            //string postData = string.Format("fieldCount=3&action=search&product=WOS&search_mode=CitedReferenceSearch&SID={0}&max_field_count=25&max_field_notice=Notice%3A+You+cannot+add+another+field.&input_invalid_notice=Search+Error%3A+Please+enter+a+search+term.&exp_notice=Search+Error%3A+Patent+search+term+could+be+found+in+more+than+one+family+%28unique+patent+number+required+for+Expand+option%29+&input_invalid_notice_limits=+%3Cbr%2F%3ENote%3A+Fields+displayed+in+scrolling+boxes+must+be+combined+with+at+least+one+other+search+field.&sa_params=WOS%7C%7CS2ASNuLQ2NIqNBZlpzT%7Chttps%3A%2F%2Fapps.webofknowledge.com%3A443%7C%27&formUpdated=true&value%28input1%29=wasik+s*&value%28select1%29=CAU&value%28hidInput1%29=&value%28bool_1_2%29=AND&value%28input2%29=&value%28select2%29=CW&value%28hidInput2%29=&value%28bool_2_3%29=AND&value%28input3%29=&value%28select3%29=CY&x=693&y=509&value%28hidInput3%29=&impliedReferenceSilo=WOS&limitStatus=collapsed&ss_lemmatization=On&ss_spellchecking=Suggest&SinceLastVisit_UTC=&SinceLastVisit_DATE=&period=Range+Selection&range=ALL&startYear=1945&endYear=2016&editions=SCI&editions=SSCI&editions=AHCI&editions=ISTP&editions=ISSHP&editions=BSCI&editions=BHCI&editions=ESCI&editions=CCR&editions=IC&update_back2search_link_param=yes&ss_query_language=&rs_sort_by=PY.D%3BLD.D%3BSO.A%3BVL.D%3BPG.A%3BAU.A", txtSID.Text);
            var data = Encoding.ASCII.GetBytes(postData);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.CookieContainer = new CookieContainer();
            Uri target = new Uri("http://www.google.com/");
            request.CookieContainer.Add(new Cookie("SID", sid)
            {
                Domain = target.Host
            });
            request.CookieContainer.Add(new Cookie("CUSTOMER", "ICM Warsaw")
            {
                Domain = target.Host
            });
            request.CookieContainer.Add(new Cookie("E_GROUP_NAME", "Technical University Poznan")
            {
                Domain = target.Host
            });

            if (request.Method == "POST")
            {
                request.ContentLength = data.Length;
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            var response = (HttpWebResponse)request.GetResponse();
            return response;
        }
    }
}