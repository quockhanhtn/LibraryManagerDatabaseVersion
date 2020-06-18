using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.EntityFramework.Model.DataAccessLayer
{
    public class DALAccount
    {
        private static DALAccount instance;

        public static DALAccount Instance { get => (instance == null) ? new DALAccount() : instance; }
        private DALAccount() { }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>-1 = login fail, 0 = admin, 1 = librarian, 2 = member</returns>
        public int Login(string username, string password)
        {
            if (username == "" || password == "") { return -1; }

            username = username.Trim();
            password = password.Trim();
            string passwordEncode = Utility.PasswordEncode.Base64ThenMD5(password);

            var account = DataProvider.Instance.Database.Accounts.Where(a => a.Username == username && a.Password == passwordEncode).Count();
            if (account <= 0) { return -1; }

            return DataProvider.Instance.Database.Accounts.ToList().Find(a => a.Username == username).AccountType;
        }
    }
}
