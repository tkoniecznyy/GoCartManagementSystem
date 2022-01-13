using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GoCart_ManagementSystem.Models
{

    [Table("Reservation")]
    public class ReservationModel
    {
        [Key]
        public int ReservationId { get; set; }

        public int UserId { get; set; } //Client that made this reservation

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }

        [Display(Name = "Data rezerwacji")]
        [DataType(DataType.DateTime)]
        public DateTime ReservationDate { get; set; }

        [Display(Name = "Tor")]
        public int TrackId { get; set; }

        [Display(Name = "Ilość uczestników")]
        public int ParticipantsCounter { get; set; }

        [Display(Name = "Rezerwacja utworzona")]
        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Zapłacone")]
        public bool IsPaid { get; set; }

        public int PaymentId { get; set; }

        [Display(Name = "Dodatkowe informacje od rezerwującego")]
        [DataType(DataType.MultilineText)]
        public string UserComments { get; set; }

        [Display(Name = "Cena (PLN)")]
        [Column(TypeName = "decimal(9,2)")] //TO TEST (runtime exception can be caused) !
        public decimal PriceInPLN { get; set; }


        public ReservationModel()
        {
        }

        public ReservationModel(int reservationId, int userId, string userEmail, DateTime reservationDate, int trackId, int participantsCounter, DateTime creationDate, bool isPaid, int paymentId, string userComments, decimal priceInPLN)
        {
            ReservationId = reservationId;
            UserId = userId;
            UserEmail = userEmail;
            ReservationDate = reservationDate;
            TrackId = trackId;
            ParticipantsCounter = participantsCounter;
            CreationDate = creationDate;
            IsPaid = isPaid;
            PaymentId = paymentId;
            UserComments = userComments;
            PriceInPLN = priceInPLN;
        }






        /*       * MySql Code *
         * 
  CREATE TABLE `pwsz`.`Reservation` (
  `ReservationId` INT NOT NULL AUTO_INCREMENT,
  `UserId` INT NOT NULL,
  `UserEmail` VARCHAR(255) NOT NULL COMMENT 'User’s Email copy',
  `ReservationDate` DATETIME NOT NULL COMMENT 'Date on which the reservation is made',
  `TrackId` INT NULL,
  `ParticipantsCounter` INT NULL COMMENT 'Number of declared participants for this reservation',
  `CreationDate` DATETIME NOT NULL COMMENT 'Creation date (from web app)',
  `IsPaid` TINYINT NOT NULL COMMENT '0 = false\n1 = true\n\n',
  `PaymentId` INT NULL COMMENT 'If [isPaid] is true then must be not null',
  `UserComments` VARCHAR(255) NULL,
  `PriceInPLN` DECIMAL(9,2) NULL,
  PRIMARY KEY (`ReservationId`),
  UNIQUE INDEX `ReservationId_UNIQUE` (`ReservationId` ASC) VISIBLE);

         * 
         */

    }
}
