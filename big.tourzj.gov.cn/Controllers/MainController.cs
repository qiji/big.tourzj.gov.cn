using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BigFile.BLL;
using BigFile.DLL;

namespace big.tourzj.gov.cn.Controllers
{
    public class MainController : Controller
    {
        //
        // GET: /Main/

        public ActionResult Index()
        {
            var syslist = new BLLBFSysSet().FindList(i => true).OrderByDescending(i => i.AddDateTime);
            return View(syslist);
        }

    }
}
