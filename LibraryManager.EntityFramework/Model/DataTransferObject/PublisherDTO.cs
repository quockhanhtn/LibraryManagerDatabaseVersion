using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.EntityFramework.Model.DataTransferObject
{
    public class PublisherDTO : View_Publisher
    {
        public PublisherDTO() : base() { }

        public PublisherDTO(View_Publisher publisherRaw) : base()
        {
            this.Id = publisherRaw.Id;
            this.Name = publisherRaw.Name;
            this.PhoneNumber = publisherRaw.PhoneNumber;
            this.Address = publisherRaw.Address;
            this.Email = publisherRaw.Email;
            this.Website = publisherRaw.Website;
            this.NumberOfBook = publisherRaw.NumberOfBook;
        }

        public Publisher GetBaseModel()
        {
            return new Publisher()
            {
                Name = this.Name,
                PhoneNumber = this.PhoneNumber,
                Address = this.Address,
                Email = this.Email,
                Website = this.Website
            };
        }
    }
}
