using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dhlopezmoreno1Final.Models
{
   public class Movement
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Period { get; set; }

        public string Characteristics { get; set; }

        //public virtual ICollection<Painting> Paintings { get; set; }
    }
}
