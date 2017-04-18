using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dhlopezmoreno1Final.Models
{
    public class Painting
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Dated { get; set; }

        public byte[] imageContent { get; set; }

        public string imageMimeType { get; set; }

        public string imageFileName { get; set; }

        public int MovementID { get; set; }

        public virtual Movement Movement { get; set; }

        public int ArtistID { get; set; }

        public virtual Artists Artist { get; set; }
    }
}
