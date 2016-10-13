using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WOSApi.Helpers
{
    class ApiHelper
    {
        WOSAuthentication.WOKMWSAuthenticateService serviceAuth;
        WOSSearch.WokSearchService serviceSearch;
        private string sharedCookie;
        private string _sessionId;

        public ApiHelper()
        {
            serviceAuth = new WOSAuthentication.WOKMWSAuthenticateService();
            serviceSearch = new WOSSearch.WokSearchService();
            var cookieJar = new System.Net.CookieContainer();
            serviceAuth.CookieContainer = cookieJar;
            serviceSearch.CookieContainer = cookieJar;
        }

        public string Login()
        {

            string session = serviceAuth.authenticate();
            _sessionId = session;
            serviceSearch.CookieContainer = new System.Net.CookieContainer(); 
            serviceSearch.CookieContainer.Add(new System.Net.Cookie("SID", _sessionId)
            {
                Domain = "search.webofknowledge.com",
                Path = @"/esti/wokmws/ws/WokSearch"
            }); 
            return session;
        }
         

        public void CloseSession()
        { 
            serviceAuth.closeSession();
        }

        public WOSSearch.fullRecordSearchResults Search(int firstRecord, int recordCount, string authorQuery)
        {
            WOSSearch.queryParameters query = new WOSSearch.queryParameters();
            query.databaseId = "WOS";
            query.editions = new WOSSearch.editionDesc[10];
            query.editions[0] = new WOSSearch.editionDesc();
            query.editions[0].collection = "WOS";
            query.editions[0].edition = "SCI";
            query.editions[1] = new WOSSearch.editionDesc();
            query.editions[1].collection = "WOS";
            query.editions[1].edition = "SSCI";
            query.editions[2] = new WOSSearch.editionDesc();
            query.editions[2].collection = "WOS";
            query.editions[2].edition = "AHCI";
            query.editions[3] = new WOSSearch.editionDesc();
            query.editions[3].collection = "WOS";
            query.editions[3].edition = "ISTP";
            query.editions[4] = new WOSSearch.editionDesc();
            query.editions[4].collection = "WOS";
            query.editions[4].edition = "ISSHP";
            query.editions[5] = new WOSSearch.editionDesc();
            query.editions[5].collection = "WOS";
            query.editions[5].edition = "IC";
            query.editions[6] = new WOSSearch.editionDesc();
            query.editions[6].collection = "WOS";
            query.editions[6].edition = "CCR";
            query.editions[7] = new WOSSearch.editionDesc();
            query.editions[7].collection = "WOS";
            query.editions[7].edition = "BSCI";
            query.editions[8] = new WOSSearch.editionDesc();
            query.editions[8].collection = "WOS";
            query.editions[8].edition = "BHCI";
            query.editions[9] = new WOSSearch.editionDesc();
            query.editions[9].collection = "WOS";
            query.editions[9].edition = "ESCI";
            query.timeSpan = new WOSSearch.timeSpan();
            query.timeSpan.begin = "1945-01-01";
            //TODO: ogarnąć żeby pobierał obecną date
            //rok-
            query.timeSpan.end = "2016-08-09";
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd").Replace('/', '-'));
            query.timeSpan.end = DateTime.Now.ToString("yyyy-MM-dd").Replace('/', '-');
            query.queryLanguage = "en";
            query.userQuery = string.Format("AU=({0})",authorQuery);

            WOSSearch.retrieveParameters retreive = new WOSSearch.retrieveParameters();
            retreive.firstRecord = firstRecord;
            retreive.count = recordCount;
            retreive.option = new WOSSearch.keyValuePair[2];
            retreive.option[0] = new WOSSearch.keyValuePair();
            retreive.option[0].key = "RecordIDs";
            retreive.option[0].value = "On";
            retreive.option[1] = new WOSSearch.keyValuePair();
            retreive.option[1].key = "targetNamespace";
            retreive.option[1].value = "http://scientific.thomsonreuters.com/schema/wok5.4/public/FullRecord"; 
            var results = serviceSearch.search(query, retreive);
            return results;
        }

        public WOSSearch.fullRecordData RetreiveLastSearch(string queryId, int firstRecord)
        {
            WOSSearch.retrieveParameters retreive = new WOSSearch.retrieveParameters();
            retreive.firstRecord = firstRecord;
            retreive.count = 50;
            retreive.option = new WOSSearch.keyValuePair[2];
            retreive.option[0] = new WOSSearch.keyValuePair();
            retreive.option[0].key = "RecordIDs";
            retreive.option[0].value = "On";
            retreive.option[1] = new WOSSearch.keyValuePair();
            retreive.option[1].key = "targetNamespace";
            retreive.option[1].value = "http://scientific.thomsonreuters.com/schema/wok5.4/public/FullRecord";
            var results = serviceSearch.retrieve(queryId, retreive);
            return results;
        }
          
    }
}
