using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigFile.DLL;

namespace BigFile.BLL
{
    public class BLLManageUser : BFBaseService<ManageUser>
    {
        public ManageUser GetByUserName(string username)
        {
            ManageUser mu = Find(m => m.UserName == username);
            return mu;
        }
    }
}
