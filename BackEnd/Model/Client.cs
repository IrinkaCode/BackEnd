using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Model
{
    public partial class Client
    {
        public int Clientid { get; set; }

        public string Firstname { get; set; } = null!;

        public string? Surname { get; set; }

        public string Lastname { get; set; } = null!;

        public string Company { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string City { get; set; } = null!;

        public virtual ICollection<Sdelka> Sdelkas { get; set; } = new List<Sdelka>();
    }
}
