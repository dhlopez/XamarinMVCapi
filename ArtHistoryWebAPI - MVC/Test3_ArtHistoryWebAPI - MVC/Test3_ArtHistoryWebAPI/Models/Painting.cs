using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test3_ArtHistory.Models
{
    public class Painting
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "You cannot leave the name of the painting blank.")]
        [StringLength(70)]
        [Display(Name = "Name of Painting")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Painting must be dated with an approximate year.")]
        [RegularExpression("^\\d{4}$", ErrorMessage = "The year the painting is dated must be exactly 4 numeric digits.")]
        [StringLength(4)]
        public string Dated { get; set; }

        [ScaffoldColumn(false)]
        public byte[] imageContent { get; set; }

        [StringLength(256)]
        [ScaffoldColumn(false)]
        public string imageMimeType { get; set; }

        [StringLength(100, ErrorMessage = "The name of the file cannot be more than 100 characters.")]
        [ScaffoldColumn(false)]
        public string imageFileName { get; set; }

        [Required(ErrorMessage = "You must select an Art Movement for the painting.")]
        [Display(Name = "Art Movement")]
        public int MovementID { get; set; }

        public virtual Movement Movement { get; set; }

        [Required(ErrorMessage = "You must select the Artist.")]
        [Display(Name = "Artist")]
        public int ArtistID { get; set; }

        public virtual Artist Artist { get; set; }
    }
}
