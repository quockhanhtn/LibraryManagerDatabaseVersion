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
    /// Class Data Access Layer for Publisher
    /// </summary>
    public class PublisherDAL
    {
        public static PublisherDAL Instance { get => (instance == null) ? new PublisherDAL() : instance; }
        private PublisherDAL() { }
        public ObservableCollection<PublisherDTO> GetList()
        {
            var listRaw = DataProvider.Instance.Database.View_Publisher.ToList();
            var listPublisherDTO = new ObservableCollection<PublisherDTO>();

            foreach (var pub in listRaw)
            {
                listPublisherDTO.Add(new PublisherDTO(pub));
            }

            return listPublisherDTO;
        }
        public ObservableCollection<PublisherDTO> GetList(bool status)
        {
            var listRaw = DataProvider.Instance.Database.View_Publisher.Where(x => x.Status == status).ToList();
            var listPublisherDTO = new ObservableCollection<PublisherDTO>();

            foreach (var pub in listRaw)
            {
                listPublisherDTO.Add(new PublisherDTO(pub));
            }

            return listPublisherDTO;
        }

        public void Add(PublisherDTO newPublisher)
        {
            var newPub = newPublisher.GetBaseModel();
            DataProvider.Instance.SaveEntity(newPub, EntityState.Added, true);
        }

        public void Update(PublisherDTO publisher)
        {
            var publisherUpdate = DataProvider.Instance.Database.Publishers.Where(x => x.Id == publisher.Id).SingleOrDefault();
            if (publisherUpdate != null)
            {
                publisherUpdate.Name = publisher.Name;
                publisherUpdate.PhoneNumber = publisher.PhoneNumber;
                publisherUpdate.Email = publisher.Email;
                publisherUpdate.Website = publisher.Website;
                publisherUpdate.Address = publisher.Address;
            }

            DataProvider.Instance.SaveEntity(publisherUpdate, EntityState.Modified, true);
        }

        public void ChangeStatus(int idPublisher)
        {
            var publisherUpdate = DataProvider.Instance.Database.Publishers.Where(x => x.Id == idPublisher).SingleOrDefault();

            if (publisherUpdate != null)
            {
                publisherUpdate.Status = (publisherUpdate.Status == true) ? false : true;
            }

            DataProvider.Instance.SaveEntity(publisherUpdate, EntityState.Modified, true);
        }

        public void Delete(int idPublisher)
        {
            var publisherDelete = DataProvider.Instance.Database.Publishers.Where(x => x.Id == idPublisher).SingleOrDefault();

            if (publisherDelete != null)
            {
                DataProvider.Instance.Database.Publishers.Remove(publisherDelete);
                DataProvider.Instance.Database.SaveChanges();
            }
        }

        private static PublisherDAL instance;
    }
}
