using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoCart_ManagementSystem.Models
{
    [Table("DiscountCoupon")]
    public class DiscountCouponModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Kupon zniżkowy (nazwa wpisywana aby uzyskać zniżkę)")]
        public string DiscountCoupon { get; set; }

        [Display(Name = "Wartość kuponu (podana w %)")]
        public int Value { get; set; }

        [Display(Name = "Data utworzenia")]
        public DateTime CreationDate { get; set; }

        public DiscountCouponModel()
        {
        }
    }
}
