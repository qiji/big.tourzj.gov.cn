using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace BigFile.DLL
{
    public class BFDBContext
    {
        public static BFEntities GetCurrDBContext()
        {
            BFEntities bfe = (BFEntities)CallContext.GetData("BFEntities");
            if (bfe == null)
            {
                bfe = new BFEntities();
                CallContext.SetData("BFEntities", bfe);
            }
            return bfe;
        }
    }
}
