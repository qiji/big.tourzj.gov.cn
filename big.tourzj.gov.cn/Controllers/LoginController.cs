using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BigFile.BLL;
using BigFile.DLL;

namespace big.tourzj.gov.cn.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View(new ManageUser());
        }

        [HttpPost]
        public ActionResult Index(ManageUser mu)
        {
            ManageUser mu1 = new BLLManageUser().GetByUserName(mu.UserName);
            if (mu1 == null)
            {
                ViewBag.ErrorMsg = "不存在的用户名!";
                return View(new ManageUser());
            }
            if (mu1.Pwd != mu.Pwd)
            {
                ViewBag.ErrorMsg = "密码错误";
                mu.Pwd = "";
                return View(mu);
            }
            UserAuthentication.Authentication(Request.RequestContext.HttpContext, mu);
            return Redirect("/Main/Index");
        }

        public ActionResult LoginOut()
        {
            UserAuthentication.LoginOut(Request.RequestContext.HttpContext);
            return Redirect("~/Login/index");
        }

    }
}
