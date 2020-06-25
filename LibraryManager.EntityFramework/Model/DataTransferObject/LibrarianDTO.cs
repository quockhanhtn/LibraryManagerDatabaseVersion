﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LibraryManager.EntityFramework.Model.DataTransferObject
{
    public class LibrarianDTO : Librarian
    {
        public string Note { get { return (this.Status == true) ? "Đang làm" : "Đã nghỉ"; } }
        public LibrarianDTO() : base() { }

        public LibrarianDTO(Librarian librarianRaw) : base()
        {
			this.Id = librarianRaw.Id;
            this.FirstName = librarianRaw.FirstName;
            this.LastName = librarianRaw.LastName;
			this.Birthday = librarianRaw.Birthday;
			this.Sex = librarianRaw.Sex;
			this.SSN = librarianRaw.SSN;
			this.Address = librarianRaw.Address;
			this.PhoneNumber = librarianRaw.PhoneNumber;
			this.Email = librarianRaw.Email;

			this.StartDate = librarianRaw.StartDate;
			this.Salary = librarianRaw.Salary;
			this.Status = librarianRaw.Status;
		}

		public Librarian GetBaseModel()
		{
			return new Librarian()
			{
				Id = this.Id,
				FirstName = this.FirstName,
				LastName = this.LastName,
				Birthday = this.Birthday,
				Sex = this.Sex,
				SSN = this.SSN,
				Address = this.Address,
				PhoneNumber = this.PhoneNumber,
				Email = this.Email,

				StartDate = this.StartDate,
				Salary = this.Salary,
				Status = this.Status,
			};
		}
    }
}
