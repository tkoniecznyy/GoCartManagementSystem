using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoCart_ManagementSystem.Models
{


    [Table("Rank")]
    public class RankModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "ID użytkownika")]
        public int UserId { get; set; }

        [Display(Name = "Imię i nazwisko")]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        public string UserEmail { get; set; }

        [Display(Name = "ID toru")]
        public int TrackId { get; set; }

        [Display(Name = "Czas")]
        public decimal Time { get; set; }

        [Display(Name = "Data ustanowienia rekordu")]
        public DateTime Date { get; set; }




        public RankModel()
        {
        }



    }







/*
 * 
 * 
 * CREATE TABLE `pwsz`.`Rank` (
`Id` INT NOT NULL AUTO_INCREMENT,
`UserId` INT NOT NULL,
`UserName` VARCHAR(255) NULL,
`UserEmail` VARCHAR(255) NULL,
`TrackId` INT NOT NULL,
`Time` VARCHAR(45) NOT NULL DEFAULT '00.00',
`Date` DATETIME NULL,
PRIMARY KEY (`Id`));
*
*/
}
