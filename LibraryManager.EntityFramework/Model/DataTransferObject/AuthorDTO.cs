using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.EntityFramework.Model.DataTransferObject
{
    public class AuthorDTO : View_Author
    {
        public ObservableCollection<string> ListBook { get; set; } = new ObservableCollection<string>();
        public int NumberOfBook { get => ListBook.Count; }
        public string Note { get { return (this.Status != true) ? "Đã ẩn" : ""; } }
        public AuthorDTO() : base() { }

        public AuthorDTO(List<View_Author> authorRaw) : base()
        {
            this.AuthorId = authorRaw[0].AuthorId;
            this.NickName = authorRaw[0].NickName;
            this.Status = authorRaw[0].Status;

            foreach (var item in authorRaw)
            {
                ListBook.Add(item.BookTitle);
            }
        }

        public AuthorDTO(View_AuthorNoBook authorNoBook) : base()
        {
            this.AuthorId = authorNoBook.AuthorId;
            this.NickName = authorNoBook.NickName;
            this.Status = authorNoBook.Status;
        }

        public Author GetBaseModel()
        {
            return new Author()
            {
                NickName = this.NickName,
                Status = this.Status
            };
        }
    }
}
