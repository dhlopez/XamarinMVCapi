using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dhlopezmoreno1Final.Models
{
    public class Artists
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public string WholeName { get; set; }

        public string YearOfBirth { get; set; }

        public string Country { get; set; }

        //public virtual ICollection<Painting> Paintings { get; set; }
    }
}
