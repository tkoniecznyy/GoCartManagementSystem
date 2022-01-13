using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoCart_ManagementSystem.Models
{
    [Table("Payment")]
    public class PaymentModel
    {
        /*
         *
         CREATE TABLE `pwsz`.`Payment` (
        `PaymentId` INT NOT NULL AUTO_INCREMENT,
        `ReservationId` INT NOT NULL,
        `UserId` INT NULL,
        `Date` DATETIME NULL,
            PRIMARY KEY (`PaymentId`));

        *
        */

        [Key]
        public int PaymentId { get; set; }

        public int ReservationId { get; set; }

        public int UserId { get; set; }

        public DateTime Date { get; set; }



        public PaymentModel()
        {
           
        }

        public PaymentModel(int reservationId, int userId, DateTime date)
        {
           
            ReservationId = reservationId;
            UserId = userId;
            Date = date;
        }
    }
}
