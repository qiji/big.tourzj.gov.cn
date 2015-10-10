using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using big.tourzj.gov.cn.Controllers;
using System.IO;
using System.Web.Mvc;

namespace BigFile.Test
{
    [TestFixture]
    public class BllTest
    {
        [Test]
        public void Upload()
        {
            BFInfoController controller = new BFInfoController();
            
            byte[] fileBytes = File.ReadAllBytes(Environment.CurrentDirectory + "\\TestFiles\\Chrysanthemum.jpg");
           ActionResult ar= controller.UpLoad(fileBytes, "testfile.jpg", 1);
            
        }
    }
}
