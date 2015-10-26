using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BigFile.DAL;
using BigFile.BLL;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using Aliyun.OpenServices.OpenStorageService;
using System.Net;

namespace big.tourzj.gov.cn.Controllers
{
    [AllowAnonymous]
    public class BFInfoController : Controller
    {
        private static string AccessKeyID = "";
        private static string AccessKeySecret = "";
        private static string BucketName = "";
        [HttpPost]
        public ActionResult UpLoad(int id)
        {
            Response.AddHeader("Access-Control-Allow-Origin", "*");
            int bfid = Request["bfid"] == null ? 0 : Convert.ToInt32(Request["bfid"]);
            int width = Request["width"] == null ? 0 : Convert.ToInt32(Request["width"]);
            int height = Request["height"] == null ? 0 : Convert.ToInt32(Request["height"]);
            BLLBFInfo bllbf = new BLLBFInfo();
            try
            {
                List<int> ret = new List<int>();
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    ret.Add(SaveBF(bllbf, Request.Files[i], id, bfid, width, height).BFID);
                }
                if (ret.Count() == 1)
                    return Json(new { code = 1, message = ret[0] });
                else
                    return Json(new { code = 1, message = ret });
            }
            catch (Exception e)
            {
                return Json(new { code = 0, message = e.Message });
            }
        }
        private BFInfo SaveBF(BLLBFInfo bllbf, HttpPostedFileBase hf, int sysid, int bfid, int width, int height)
        {
            string filename = hf.FileName;
            string extname = System.IO.Path.GetExtension(filename).ToLower();
            string fname = System.IO.Path.GetFileNameWithoutExtension(filename);

            Stream newfile = hf.InputStream;

            BFInfo bfinfo;
            if (hf.ContentType.ToLower().Contains("image"))
            {
                Image image = System.Drawing.Image.FromStream(hf.InputStream);
                newfile = GetThumbnail(image, width, height);
            }
            newfile.Seek(0, SeekOrigin.Begin);
            OssClient oc = new OssClient(AccessKeyID, AccessKeySecret);
            string newfilename = DateTime.Now.ToString("yyMMdd") + "/" + Guid.NewGuid().ToString();
            PutObjectResult pr = oc.PutObject(BucketName, newfilename, newfile);

            if (bfid == 0)
            {
                bfinfo = new BFInfo();
                bfinfo.SysID = sysid;
                bfinfo.OrignName = fname;
                bfinfo.ExtName = extname;
                bfinfo.MineType = hf.ContentType;
                bfinfo.LastViewDateTime = bfinfo.CrtDateTime = DateTime.Now;
                bfinfo.OSSFileName = newfilename;
                bfinfo.GetCount = 0;
                bllbf.Add(bfinfo);
            }
            else
            {
                bfinfo = bllbf.Find(bfid);
                bfinfo.OrignName = fname;
                bfinfo.ExtName = extname;
                bfinfo.MineType = hf.ContentType;
                bfinfo.CrtDateTime = DateTime.Now;
                bfinfo.OSSFileName = newfilename;
                bfinfo.GetCount = 0;
                bllbf.UpDate(bfinfo);
            }
            return bfinfo;
        }

        public MemoryStream GetThumbnail(Image imgSource, int destWidth, int destHeight)
        {
            MemoryStream ms = new MemoryStream();

            ImageFormat thisformat = imgSource.RawFormat;
            int sourcewidth = imgSource.Width;
            int sourceheight = imgSource.Height;
            if (destWidth == 0 && destHeight == 0)
            {
                imgSource.Save(ms, thisformat);
                return ms;
            }
            else
            {
                //按照比例缩放
                int towidth = destWidth;
                int toheight = destHeight;

                if (towidth == 0)
                {
                    towidth = Convert.ToInt32(Math.Round(toheight * 1.0 * (sourcewidth * 1.0 / sourceheight * 1.0)));
                }
                else if (toheight == 0)
                {
                    toheight = Convert.ToInt32(Math.Round(towidth * 1.0 / (sourcewidth * 1.0 / sourceheight * 1.0)));
                }

                Bitmap outBmp = new Bitmap(towidth, toheight);

                Graphics g = Graphics.FromImage(outBmp);
                g.Clear(Color.Transparent);
                // 设置画布的描绘质量         
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(imgSource,
                    new Rectangle(0, 0, towidth, toheight),
                    new Rectangle(0, 0, sourcewidth, sourceheight),
                    GraphicsUnit.Pixel);
                g.Dispose();

                // 以下代码为保存图片时，设置压缩质量     
                EncoderParameters encoderParams = new EncoderParameters();
                long[] quality = new long[1];
                quality[0] = 100;
                EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
                encoderParams.Param[0] = encoderParam;
                imgSource.Dispose();
                outBmp.Save(ms, thisformat);
                return ms;
            }
        }
        public ActionResult Delete(int bfid)
        {
            BLLBFInfo bllbfinfo = new BLLBFInfo();
            var bfinfo = bllbfinfo.Find(bfid);
            OssClient oc = new OssClient(AccessKeyID, AccessKeySecret);
            oc.DeleteObject(BucketName, bfinfo.OSSFileName);
            bllbfinfo.Delete(bfinfo);
            return View();
        }

        public ActionResult GetFile(int id, int width = 0, int height = 0)
        {
            int bfid = id;
            BLLBFInfo bllbfinfo = new BLLBFInfo();
            var bfinfo = bllbfinfo.Find(bfid);
            if (bfinfo == null)
            {
                return Content("不存在的数据");
            }
            else
            {
                bfinfo.LastViewDateTime = DateTime.Now;
                bfinfo.GetCount++;
                bllbfinfo.UpDate(bfinfo);
                //返回一个二进制流！根据类型，来判断，需要返回的内容！
                MemoryStream ms = new MemoryStream();
                OssClient oc = new OssClient(AccessKeyID, AccessKeySecret);
                GetObjectRequest getObjectRequest = new GetObjectRequest(BucketName, bfinfo.OSSFileName);
                oc.GetObject(getObjectRequest, ms);
                if (bfinfo.MineType.Contains("image"))
                {
                    if (width == 0 && height == 0)
                    {
                        return File(ms.ToArray(), bfinfo.MineType);
                    }
                    else
                    {
                        Image image = System.Drawing.Image.FromStream(ms);
                        return File(GetThumbnail(image, width, height).ToArray(), bfinfo.MineType);
                    }
                }
                else
                {
                    return File(ms.ToArray(), bfinfo.MineType);
                }
            }
        }
    }

}
