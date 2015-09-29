using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace big.tourzj.gov.cn.Controllers
{
    [AllowAnonymous]
    public class BFInfoController : Controller
    {
        //
        // GET: /BFInfo/

        public ActionResult UpLoad()
        {
            return View();
        }

        public ActionResult GetFile()
        {
            return View();
        }

    }
}
