using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WOS_.Models;
using WOS_.Helpers;

namespace WOS_.Controllers
{
    public class AuthorsController : Controller
    {
        // GET: Authors
        public ActionResult Index(string author)
        {
            ViewBag.Authors = WOSApiHelper.GetAuthors(author);

            return View();
        } 
    }
}