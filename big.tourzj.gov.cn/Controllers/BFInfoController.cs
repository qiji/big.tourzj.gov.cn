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

namespace big.tourzj.gov.cn.Controllers
{
    [AllowAnonymous]
    public class BFInfoController : Controller
    {
        //
        // GET: /BFInfo/
        [HttpPost]
        public ActionResult UpLoad(byte[] file, string filename, int sysid, int bfid = 0, int width = 0, int height = 0)
        {
            string extname = System.IO.Path.GetExtension(filename).ToLower();
            string fname = System.IO.Path.GetFileNameWithoutExtension(filename);
            byte[] newfile;

            BFInfo bfinfo;
            BLLBFInfo bllbf = new BLLBFInfo();
            if (extname == ".jpg"
                || extname == ".jpeg")
            {
                MemoryStream ms = new MemoryStream(file);
                Image image = System.Drawing.Image.FromStream(ms);
                newfile = GetThumbnail(image, width, height);
            }
            else
            {
                newfile = file;
            }
            if (bfid == 0)
            {
                bfinfo = new BFInfo();
                bfinfo.SysID = sysid;
                bfinfo.OrignName = fname;
                bfinfo.ExtName = extname;
                bfinfo.CrtDateTime = DateTime.Now;
                bfinfo.BFContent = newfile;
                bllbf.Add(bfinfo);
            }
            else
            {
                bfinfo = bllbf.Find(bfid);
                bfinfo.OrignName = fname;
                bfinfo.ExtName = extname;
                bfinfo.CrtDateTime = DateTime.Now;
                bfinfo.BFContent = newfile;
                bllbf.UpDate(bfinfo);
            }
            return Content("0");
        }

        public byte[] GetThumbnail(Image imgSource, int destWidth, int destHeight)
        {
            MemoryStream ms = new MemoryStream();
            ImageFormat thisformat = imgSource.RawFormat;
            int sourcewidth = imgSource.Width;
            int sourceheight = imgSource.Height;
            if (destWidth == 0 && destHeight == 0)
            {
                imgSource.Save(ms, thisformat);
                return ms.ToArray();
            }
            else
            {
                //按照比例缩放
                int towidth = destWidth;
                int toheight = destHeight;

                if (towidth == 0)
                {
                    towidth = toheight * (sourcewidth / sourceheight);
                }
                else if (toheight == 0)
                {
                    toheight = towidth / (sourcewidth / sourceheight);
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
                return ms.ToArray();
            }
        }
        public ActionResult Delete(int bfid)
        {
            return View();
        }

        private ActionResult GetFile(int bfid, int width = 0, int height = 0)
        {
            var bfinfo = new BLLBFInfo().Find(bfid);
            if (bfinfo == null)
            {
                return Content("不存在的数据");
            }
            else
            {
                //返回一个二进制流！根据类型，来判断，需要返回的内容！
                if (bfinfo.ExtName.ToLower().Contains("jpg") ||
                    bfinfo.ExtName.ToLower().Contains("jpeg"))
                {
                    if (width == 0 && height == 0)
                    {
                        return File(bfinfo.BFContent, "image/jpeg");
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream(bfinfo.BFContent);
                        Image image = System.Drawing.Image.FromStream(ms);
                        return File(GetThumbnail(image, width, height), "image/jpeg");
                    }
                }
                else
                {
                    return File(bfinfo.BFContent, "application/octet-stream");
                }
            }
        }


    }
}
