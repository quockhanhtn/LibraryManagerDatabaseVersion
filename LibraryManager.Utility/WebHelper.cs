using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Utility
{
    public class WebHelper
    {
        public static void SendEmail(string emailAddress, string emailSubject = "", string emailBody = "")
        {
            Process process = new Process();
            process.StartInfo.FileName = "mailto:" + emailAddress + "?subject=" + emailSubject + "&body=" + emailBody;
            process.Start();
        }

        public static void OpenLink(string link)
        {
            Process.Start(link);
        }
    }
}
