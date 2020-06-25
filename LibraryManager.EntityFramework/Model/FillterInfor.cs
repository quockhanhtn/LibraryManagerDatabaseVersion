using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.EntityFramework.Model
{
    public class FillterInfor
    {
        public enum StatusFillter { Active, InActive, All };

        public StatusFillter StatusState { get; set; } = StatusFillter.Active;
    }
}
