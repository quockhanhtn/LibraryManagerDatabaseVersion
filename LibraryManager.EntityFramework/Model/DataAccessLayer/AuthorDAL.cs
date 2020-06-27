using LibraryManager.EntityFramework.Model.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.EntityFramework.Model.DataAccessLayer
{
    /// <summary>
    /// Class Data Access Layer for Author
    /// </summary>
    public class AuthorDAL
    {
        public static AuthorDAL Instance { get => (instance == null) ? new AuthorDAL() : instance; }
        private AuthorDAL() { }
        public ObservableCollection<AuthorDTO> GetList()
        {
            var listRaw = DataProvider.Instance.Database.View_Author.GroupBy(u => u.AuthorId).Select(grp => grp.ToList()).ToList();
            var listAuthorNoBook = DataProvider.Instance.Database.View_AuthorNoBook.ToList();
            var listAuthorDTO = new ObservableCollection<AuthorDTO>();

            foreach (var author in listRaw)
            {
                listAuthorDTO.Add(new AuthorDTO(author));
            }

            foreach (var authorNoBook in listAuthorNoBook)
            {
                listAuthorDTO.Add(new AuthorDTO(authorNoBook));
            }

            return listAuthorDTO;
        }

        public ObservableCollection<AuthorDTO> GetList(bool status)
        {
            var listRaw = DataProvider.Instance.Database.View_Author.GroupBy(u => u.AuthorId).Select(grp => grp.ToList()).ToList();
            var listAuthorNoBook = DataProvider.Instance.Database.View_AuthorNoBook.Where(x => x.Status == status).ToList();
            var listAuthorDTO = new ObservableCollection<AuthorDTO>();

            foreach (var author in listRaw)
            {
                if (author[0].Status == status) { listAuthorDTO.Add(new AuthorDTO(author)); }
            }

            foreach (var authorNoBook in listAuthorNoBook)
            {
                listAuthorDTO.Add(new AuthorDTO(authorNoBook));
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
            var authorUpdate = DataProvider.Instance.Database.Authors.Where(x => x.Id == author.AuthorId).SingleOrDefault();
            if (authorUpdate != null)
            {
                authorUpdate.NickName = author.NickName;
            }

            DataProvider.Instance.SaveEntity(authorUpdate, EntityState.Modified, true);
        }

        public void ChangeStatus(int idAuthor)
        {
            var authorUpdate = DataProvider.Instance.Database.Authors.Where(x => x.Id == idAuthor).SingleOrDefault();

            if (authorUpdate != null)
            {
                authorUpdate.Status = (authorUpdate.Status == true) ? false : true;
            }

            DataProvider.Instance.SaveEntity(authorUpdate, EntityState.Modified, true);
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
