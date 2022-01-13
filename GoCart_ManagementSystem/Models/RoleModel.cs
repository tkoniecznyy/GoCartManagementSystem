using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoCart_ManagementSystem.Models
{

    [Table("Role")]
    public class RoleModel
    {
        [Key]
        public int RoleID { get; set; }

        [Display(Name = "Uprawnienia")]
        public string Permissions { get; set; }

        [Display(Name = "Rola")]
        public string RoleName { get; set; }




        public RoleModel()
        {
        }
    }
}
