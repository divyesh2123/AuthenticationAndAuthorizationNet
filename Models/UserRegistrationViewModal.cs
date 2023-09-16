using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AuthenticationAndAuthorization.Models
{
    public class UserRegistrationViewModal
    {
        public string Name { get; set; }    

        public string EmailID { get; set; }

        public string Password { get; set; }    

        [Display(Name ="Role")]
        public int RoleId { get; set; }

        public List<SelectListItem> RoleData { get; set; }
    }
}