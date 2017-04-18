using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test3_ArtHistory.Models
{
    public class Movement
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "You cannot leave the name for the Movement blank.")]
        [StringLength(50)]
        [Display(Name = "Movement/Style")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must enter the time period for the Movement.")]
        [StringLength(12)]
        [Display(Name = "Time Period")]
        public string Period { get; set; }

        [Required(ErrorMessage = "You must describe the characteristics of the Movement.")]
        [StringLength(255)]
        public string Characteristics { get; set; }
        
        //public virtual ICollection<Painting> Paintings { get; set; }
    }
}
