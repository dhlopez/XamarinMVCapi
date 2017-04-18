using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test3_ArtHistory.Models
{
    public class Artist
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "You cannot leave the name for the Artist blank.")]
        [StringLength(50)]
        [Display(Name = "Artist")]
        [Index("IX_Unique_Artist", IsUnique = true)]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must enter the full name of the artist.")]
        [StringLength(255)]
        [Display(Name = "Artist's Full Name")]
        public string WholeName { get; set; }

        [Required(ErrorMessage = "You must enter the year of birth for the artist.")]
        [RegularExpression("^\\d{4}$", ErrorMessage = "The year of birth for the artist must be exactly 4 numeric digits.")]
        [StringLength(4)]
        [Display(Name = "Year of Birth")]
        public string YearOfBirth { get; set; }

        [Required(ErrorMessage = "You must enter the artist's country.")]
        [StringLength(50)]
        public string Country { get; set; }

        //public virtual ICollection<Painting> Paintings { get; set; }
    }
}
