using System.Diagnostics;

namespace LibraryManager.Utility
{
    /// <summary>
    /// Hỗ trợ với link và email
    /// </summary>
    public class WebHelper
    {
        /// <summary>
        /// Gửi email đến địa chỉ cụ thể
        /// </summary>
        /// <param name="emailAddress">Địa chỉ mail người nhận</param>
        /// <param name="emailSubject">Chủ để mail</param>
        /// <param name="emailBody">Nội dung mail</param>
        public static void SendEmail(string emailAddress, string emailSubject = "", string emailBody = "")
        {
            Process process = new Process();
            process.StartInfo.FileName = "mailto:" + emailAddress + "?subject=" + emailSubject + "&body=" + emailBody;
            process.Start();
        }

        /// <summary>
        /// Mở một trang web bằng trình duyệt mặc định
        /// </summary>
        /// <param name="link">Địa chỉ trang web</param>
        public static void OpenLink(string link)
        {
            Process.Start(link);
        }
    }
}
