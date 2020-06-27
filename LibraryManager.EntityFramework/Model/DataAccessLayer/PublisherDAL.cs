using LibraryManager.EntityFramework.Model.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.EntityFramework.Model.DataAccessLayer
{
    /// <summary>
    /// Class Data Access Layer for Publisher
    /// </summary>
    //public class PublisherDAL
    //{
    //    public static PublisherDAL Instance { get => (instance == null) ? new PublisherDAL() : instance; }
    //    private PublisherDAL() { }
    //    public ObservableCollection<PublisherDTO> GetList()
    //    {
    //        var listRaw = DataProvider.Instance.Database.Publishers.ToList();
    //        var listPublisherDTO = new ObservableCollection<PublisherDTO>();

    //        foreach (var lib in listRaw)
    //        {
    //            listPublisherDTO.Add(new PublisherDTO(lib));
    //        }

    //        return listPublisherDTO;
    //    }
    //    public ObservableCollection<PublisherDTO> GetList(bool status)
    //    {
    //        var listRaw = DataProvider.Instance.Database.Publishers.Where(x => x.Status == status).ToList();
    //        var listPublisherDTO = new ObservableCollection<PublisherDTO>();

    //        foreach (var lib in listRaw)
    //        {
    //            listPublisherDTO.Add(new PublisherDTO(lib));
    //        }

    //        return listPublisherDTO;
    //    }

    //    public void Add(PublisherDTO newPublisher)
    //    {
    //        var newLib = newPublisher.GetBaseModel();
    //        DataProvider.Instance.SaveEntity(newLib, EntityState.Added);

    //        //DataProvider.Instance.Database.Entry(newLib).State = EntityState.Added;
    //        //DataProvider.Instance.Database.SaveChanges();
    //        //DataProvider.Instance.Database.Entry(newLib).State = EntityState.Detached;
    //    }

    //    public void Update(PublisherDTO librarian)
    //    {
    //        var librarianUpdate = DataProvider.Instance.Database.Publishers.Where(x => x.Id == librarian.Id).SingleOrDefault();
    //        if (librarianUpdate != null)
    //        {
    //            librarianUpdate.LastName = librarian.LastName;
    //            librarianUpdate.FirstName = librarian.FirstName;
    //            librarianUpdate.Sex = librarian.Sex;
    //            librarianUpdate.Birthday = librarian.Birthday;
    //            librarianUpdate.SSN = librarian.SSN;
    //            librarianUpdate.Address = librarian.Address;
    //            librarianUpdate.Email = librarian.Email;
    //            librarianUpdate.PhoneNumber = librarian.PhoneNumber;
    //            librarianUpdate.StartDate = librarian.StartDate;
    //            librarianUpdate.Salary = librarian.Salary;
    //        }

    //        DataProvider.Instance.SaveEntity(librarianUpdate, EntityState.Modified);

    //        //DataProvider.Instance.Database.Entry(librarianUpdate).State = EntityState.Modified;
    //        //DataProvider.Instance.Database.SaveChanges();
    //        //DataProvider.Instance.Database.Entry(librarianUpdate).State = EntityState.Detached;
    //    }

    //    public void ChangeStatus(string idPublisher)
    //    {
    //        var librarianUpdate = DataProvider.Instance.Database.Publishers.Where(x => x.Id == idPublisher).SingleOrDefault();

    //        if (librarianUpdate != null)
    //        {
    //            librarianUpdate.Status = (librarianUpdate.Status == true) ? false : true;
    //        }

    //        DataProvider.Instance.SaveEntity(librarianUpdate, EntityState.Modified);

    //        //DataProvider.Instance.Database.Entry(librarianUpdate).State = EntityState.Modified;
    //        //DataProvider.Instance.Database.SaveChanges();
    //        //DataProvider.Instance.Database.Entry(librarianUpdate).State = EntityState.Detached;
    //    }

    //    private static PublisherDAL instance;
    //}
}
