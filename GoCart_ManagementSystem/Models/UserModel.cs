using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoCart_ManagementSystem.Models
{

    [Table("User")]
    public class UserModel
    {
        /*
         * 
         *  This model maps to a table "pwsz.User" 
         *  
         *  Contains all data about every registered user in system
         * 
         * 
         */

        [Key]
        public int UserID { get; set; } //AUTO_INCREMENT NOT NULL

        public string Email { get; set; } // NOT NULL

        public string Password { get; set; } // NOT NULL

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public Int64 Phone { get; set; }

        public int  RoleID { get; set; }



        public UserModel(string email, string password, string lastName, string firstName, string address, string city, long phone, int roleID)
        {
          
            Email = email;
            Password = password;
            LastName = lastName;
            FirstName = firstName;
            Address = address;
            City = city;
            Phone = phone;
            RoleID = roleID;
        }

        public UserModel()
        {
        }
    }
}
