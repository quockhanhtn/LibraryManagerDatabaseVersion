using LibraryManager.EntityFramework.Model.DataTransferObject;
using LibraryManager.Utility.Enums;
using LibraryManager.Utility.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;

namespace LibraryManager.EntityFramework.Model.DataAccessLayer
{
    /// <summary>
    /// Class Data Access Layer for Publisher
    /// </summary>
    public class PublisherDAL : IDatabaseAccess<PublisherDTO, int>
    {
        public static PublisherDAL Instance { get => (instance == null) ? new PublisherDAL() : instance; }
        private PublisherDAL() { }

        public ObservableCollection<PublisherDTO> GetList(StatusFillter fillter = StatusFillter.AllStatus)
        {
            var listPublisherDTO = new ObservableCollection<PublisherDTO>();
            var listRaw = new List<Publisher>();

            switch (fillter)
            {
                case StatusFillter.AllStatus:
                    listRaw = DataProvider.Instance.Database.Publishers.ToList();
                    break;
                case StatusFillter.Active:
                    listRaw = DataProvider.Instance.Database.Publishers.Where(x => x.Status == true).ToList();
                    break;
                case StatusFillter.InActive:
                    listRaw = DataProvider.Instance.Database.Publishers.Where(x => x.Status == false).ToList();
                    break;
            }

            foreach (var pub in listRaw) { listPublisherDTO.Add(new PublisherDTO(pub)); }
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
