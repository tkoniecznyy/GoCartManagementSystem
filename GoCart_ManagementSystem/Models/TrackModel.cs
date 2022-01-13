using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoCart_ManagementSystem.Models
{
    [Table("Track")]
    public class TrackModel
    {
        [Key]
        public int TrackId { get; set; }

        [Display(Name = "Nazwa toru")]
        public string Name { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Display(Name = "Sprawny")]
        public bool IsWorking { get; set; }

        [Display(Name = "Max. ilość gokartów na torze")]
        public int MaxGoCartsAllowed { get; set; }






        public TrackModel()
        {
        }
    }
}
