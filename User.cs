//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AuthenticationAndAuthorization
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string EmaiIID { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public Nullable<int> RoleID { get; set; }
    
        public virtual RoleMaster RoleMaster { get; set; }
    }
}