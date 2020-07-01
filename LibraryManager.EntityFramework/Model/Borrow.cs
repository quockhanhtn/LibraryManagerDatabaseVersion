
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
    using System.Collections.Generic;
    
public partial class Borrow
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Borrow()
    {

        this.PayFineInfoes = new HashSet<PayFineInfo>();

        this.ReturnBooks = new HashSet<ReturnBook>();

    }


    public int Id { get; set; }

    public string BookId { get; set; }

    public string MemberId { get; set; }

    public string LibrarianId { get; set; }

    public System.DateTime BorrowDate { get; set; }

    public bool Status { get; set; }



    public virtual Book Book { get; set; }

    public virtual Librarian Librarian { get; set; }

    public virtual Member Member { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<PayFineInfo> PayFineInfoes { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<ReturnBook> ReturnBooks { get; set; }

}

}
