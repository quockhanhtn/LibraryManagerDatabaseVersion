﻿

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace LibraryManager.EntityFramework.Model
{

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


public partial class LibraryManagerEntities : DbContext
{
    public LibraryManagerEntities()
        : base("name=LibraryManagerEntities")
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }


    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BookCategory> BookCategories { get; set; }

    public virtual DbSet<BookItem> BookItems { get; set; }

    public virtual DbSet<Borrow> Borrows { get; set; }

    public virtual DbSet<Librarian> Librarians { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<PayFineInfo> PayFineInfoes { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    public virtual DbSet<ReturnBook> ReturnBooks { get; set; }

    public virtual DbSet<View_Author> View_Author { get; set; }

    public virtual DbSet<View_AuthorNoBook> View_AuthorNoBook { get; set; }

    public virtual DbSet<View_Book> View_Book { get; set; }

    public virtual DbSet<View_BookCategory> View_BookCategory { get; set; }

    public virtual DbSet<View_Publisher> View_Publisher { get; set; }

}

}

