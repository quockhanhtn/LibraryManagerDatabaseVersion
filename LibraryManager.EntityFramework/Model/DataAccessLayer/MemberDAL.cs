using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.EntityFramework.Model.DataAccessLayer
{
    public class MemberDAL
    {
        public static MemberDAL Instance { get => (instance == null) ? new MemberDAL() : instance; }
        private MemberDAL() { }
        private static MemberDAL instance;
    }
}
