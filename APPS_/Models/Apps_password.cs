//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Apps_.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Apps_password
    {
        public int Id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string title { get; set; }
        public string notes { get; set; }
        public string tags { get; set; }
        public int Apps_UsersRoleId { get; set; }
        public string crypt { get; set; }
        public string auth { get; set; }
    
        public virtual Apps_UsersRole Apps_UsersRole { get; set; }
    }
}
