using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows.Documents;

namespace LibraryManager.EntityFramework.Model.DataAccessLayer
{
    /// <summary>
    /// Class Data Access Layer for Account
    /// </summary>
    public class AccountDAL
    {
        public static AccountDAL Instance { get => (instance == null) ? new AccountDAL() : instance; }
        private AccountDAL() { }

        /// <summary>
        /// Return Account nếu login thành công, return null nếu login thất bại
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Account Login(string username, string password)
        {
            if (username == "" || password == "") { return null; }

            username = username.Trim();
            password = password.Trim();
            string passwordEncode = Utility.PasswordEncoder.Base64ThenMD5(password);

            return DataProvider.Instance.Database.Accounts.Where(a => a.Username == username && a.Password == passwordEncode).FirstOrDefault();
        }

        public bool CheckPassword(string personId, string password)
        {
            password = password.Trim();
            string passwordEncode = Utility.PasswordEncoder.Base64ThenMD5(password);
            if (DataProvider.Instance.Database.Accounts.Count(a => a.Username == personId && a.Password == passwordEncode) > 0)
            {
                return true;
            }
            return false;
        }

        public void ChangePassword(string personId, string newPassword)
        {
            newPassword = newPassword.Trim();
            string passwordEncode = Utility.PasswordEncoder.Base64ThenMD5(newPassword);
            var acc = DataProvider.Instance.Database.Accounts.Where(a => a.Username == personId).FirstOrDefault();
            acc.Password = passwordEncode;
            DataProvider.Instance.SaveEntity(acc, System.Data.Entity.EntityState.Modified);
        }

        private static AccountDAL instance;
    }
}
