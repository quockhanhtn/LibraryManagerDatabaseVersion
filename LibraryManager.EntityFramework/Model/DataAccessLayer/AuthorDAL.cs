using LibraryManager.EntityFramework.Model.DataTransferObject;
using LibraryManager.Utility.Enums;
using LibraryManager.Utility.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;

namespace LibraryManager.EntityFramework.Model.DataAccessLayer
{
    /// <summary>
    /// Class Data Access Layer for Author
    /// </summary>
    public class AuthorDAL : IDatabaseAccess<AuthorDTO, int>
    {
        public static AuthorDAL Instance { get => (instance == null) ? new AuthorDAL() : instance; }
        private AuthorDAL() { }

        public ObservableCollection<Author> GetRawList()
        {
            var rawList = DataProvider.Instance.Database.Authors.Where(x => x.Status == true).ToList();
            var result = new ObservableCollection<Author>();
            foreach (var item in rawList) { result.Add(item); }
            return result;
        }

        public ObservableCollection<AuthorDTO> GetList(StatusFillter fillter = StatusFillter.AllStatus)
        {
            var listAuthorDTO = new ObservableCollection<AuthorDTO>();
            var listRaw = new List<Author>();

            switch (fillter)
            {
                case StatusFillter.AllStatus:
                    listRaw = DataProvider.Instance.Database.Authors.ToList();
                    break;
                case StatusFillter.Active:
                    listRaw = DataProvider.Instance.Database.Authors.Where(x => x.Status == true).ToList();
                    break;
                case StatusFillter.InActive:
                    listRaw = DataProvider.Instance.Database.Authors.Where(x => x.Status == false).ToList();
                    break;
                default:
                    break;
            }

            foreach (var author in listRaw) 
            {
                listAuthorDTO.Add(new AuthorDTO(author));
            }

            return listAuthorDTO;
        }

        public void Add(AuthorDTO newAuthor)
        {
            var newAu = newAuthor.GetBaseModel();
            DataProvider.Instance.SaveEntity(newAu, EntityState.Added, true);
        }

        public void Update(AuthorDTO author)
        {
            var authorUpdate = DataProvider.Instance.Database.Authors.Where(x => x.Id == author.Id).SingleOrDefault();
            if (authorUpdate != null)
            {
                authorUpdate.NickName = author.NickName;
                DataProvider.Instance.SaveEntity(authorUpdate, EntityState.Modified, true);
            }
        }

        public void ChangeStatus(int idAuthor)
        {
            var authorUpdate = DataProvider.Instance.Database.Authors.Where(x => x.Id == idAuthor).SingleOrDefault();

            if (authorUpdate != null)
            {
                authorUpdate.Status = (authorUpdate.Status == true) ? false : true;
                DataProvider.Instance.SaveEntity(authorUpdate, EntityState.Modified, true);
            }
        }

        public void Delete(int idAuthor)
        {
            var authorDelete = DataProvider.Instance.Database.Authors.Where(x => x.Id == idAuthor).SingleOrDefault();

            if (authorDelete != null)
            {
                DataProvider.Instance.Database.Authors.Remove(authorDelete);
                DataProvider.Instance.Database.SaveChanges();
            }
        }

        private static AuthorDAL instance;
    }
}
