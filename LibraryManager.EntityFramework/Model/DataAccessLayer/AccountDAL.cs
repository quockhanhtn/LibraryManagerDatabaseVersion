using LibraryManager.Utility;
using System.Data.Entity;
using System.Linq;

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
            string passwordEncode = PasswordEncoder.Base64ThenMD5(password);

            return DataProvider.Instance.Database.Accounts.Where(a => a.Username == username && a.Password == passwordEncode).FirstOrDefault();
        }

        public bool CheckPassword(Account account, string passwordToCheck)
        {
            string passwordEncode = PasswordEncoder.Base64ThenMD5(passwordToCheck);
            if (account.Password == passwordEncode) { return true; }
            return false;
        }

        public bool CheckPassword(string personId, string password)
        {
            password = password.Trim();
            string passwordEncode = PasswordEncoder.Base64ThenMD5(password);
            if (DataProvider.Instance.Database.Accounts.Count(a => a.Username == personId && a.Password == passwordEncode) > 0)
            {
                return true;
            }
            return false;
        }

        public void ChangePassword(string personId, string newPassword)
        {
            newPassword = newPassword.Trim();
            string passwordEncode = PasswordEncoder.Base64ThenMD5(newPassword);
            var acc = DataProvider.Instance.Database.Accounts.Where(a => a.Username == personId).FirstOrDefault();
            acc.Password = passwordEncode;
            DataProvider.Instance.SaveEntity(acc, EntityState.Modified);
        }

        public Account Update(string personId, string username, string password)
        {
            try
            {
                var accUpdate = DataProvider.Instance.Database.Accounts.Where(a => a.PersonId == personId).FirstOrDefault();
                accUpdate.Username = username;
                accUpdate.Password = PasswordEncoder.Base64ThenMD5(password);
                DataProvider.Instance.SaveEntity(accUpdate, EntityState.Modified);
                return accUpdate;
            }
            catch (System.Exception) { /*MessageBox.Show(exp.Message);*/ }
            return null;
        }

        private static AccountDAL instance;
    }
}
