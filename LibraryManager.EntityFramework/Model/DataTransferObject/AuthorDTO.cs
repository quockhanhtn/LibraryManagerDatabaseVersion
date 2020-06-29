using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.EntityFramework.Model.DataTransferObject
{
    public class AuthorDTO : Author
    {
        public ObservableCollection<string> ListBookTitle
        {
            get
            {
                var list = new ObservableCollection<string>();
                foreach (var b in Books) { list.Add(b.Title); }
                return list;
            }
        }

        public int NumberOfBook { get => Books.Count; }
        public string Note { get { return (this.Status != true) ? "Đã ẩn" : ""; } }
        public AuthorDTO() : base() { }

        public AuthorDTO(Author authorRaw) : base()
        {
            this.Id = authorRaw.Id;
            this.NickName = authorRaw.NickName;
            this.Status = authorRaw.Status;
            this.Books = authorRaw.Books;
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
