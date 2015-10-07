using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BigFile.DAL;
using BigFile.BLL;

namespace big.tourzj.gov.cn.Controllers
{
    public class ManageUserController : Controller
    {
        //
        // GET: /ManageUser/

       

        public ActionResult Pwd()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Pwd")]
        public ActionResult UpDatePwd()
        {
            string oldpwd = Request.Form["tboldPwd"];
            string newpwd = Request.Form["tbnewPwd"];
            string newpwd2 = Request.Form["tbnewPwd2"];
            if (newpwd != newpwd2)
            {
                ViewBag.ErrorMsg = "两次密码输入不一致!";
            }
            else
            {
                BLLManageUser bmu = new BLLManageUser();
                ManageUser mu = bmu.GetByUserName(User.GetUserName());
                if (mu.Pwd != oldpwd)
                {
                    ViewBag.ErrorMsg = "原密码输入错误";
                }
                else
                {
                    mu.Pwd = newpwd;
                    bmu.Save();
                    ViewBag.HintMsg = "密码修改成功";
                }
            }
            return View();
        }
    }
}
