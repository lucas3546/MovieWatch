using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Domain.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public int Telefono { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
    }
}
