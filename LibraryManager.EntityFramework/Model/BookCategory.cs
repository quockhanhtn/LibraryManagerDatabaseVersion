
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
    
public partial class BookCategory
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public BookCategory()
    {

        this.Books = new HashSet<Book>();

    }


    public int Id { get; set; }

    public string Name { get; set; }

    public Nullable<int> LimitDays { get; set; }

    public Nullable<bool> Status { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Book> Books { get; set; }

}

}
