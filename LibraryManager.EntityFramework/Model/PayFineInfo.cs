
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
    
public partial class PayFineInfo
{

    public int Id { get; set; }

    public string BookId { get; set; }

    public string MemberId { get; set; }

    public string LibrarianId { get; set; }

    public System.DateTime TermDate { get; set; }

    public System.DateTime ReturnDate { get; set; }

    public decimal Cash { get; set; }

    public bool IsLostBook { get; set; }

}

}
