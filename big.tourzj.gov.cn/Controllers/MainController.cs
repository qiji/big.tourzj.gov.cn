using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BigFile.BLL;
using BigFile.DAL;

namespace big.tourzj.gov.cn.Controllers
{
    public class MainController : Controller
    {
        //
        // GET: /Main/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData(string key)
        {
            var syslist = new BLLBFSysSet().FindList(i => i.SysName.Contains(key)).OrderByDescending(i => i.AddDateTime);
            return Json(syslist.ToList().Select(ii => new { ii.SysID, ii.SysName, ii.SysDesp, AddTime = ii.AddDateTime.ToString() }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int sysid)
        {
            if (sysid == 0)
            {
                return View(new BFSysSet());

            }
            else
            {
                return View(new BLLBFSysSet().Find(sysid));
            }
        }

        [ActionName("Edit")]
        [HttpPost]
        public ActionResult Save(BFSysSet bfsysset)
        {
            BLLBFSysSet bllbf = new BLLBFSysSet();

            if (bfsysset.SysID == 0)
            {
                bfsysset.AddDateTime = DateTime.Now;
                bllbf.Add(bfsysset);
            }
            else
            {
                BFSysSet bf = bllbf.Find(bfsysset.SysID);
                bf.SysName = bfsysset.SysName;
                bf.SysDesp = bfsysset.SysDesp;
                bllbf.UpDate(bf);
                //TryUpdateModel<BFSysSet>(bfsysset,new string[]{"SysName","SysDesp"});
            }
            return Redirect("~/Main/Index");
        }

    }
}
