using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hotel.Models
{
    public class Clients
    {
        public string IdCF {  get; set; }

        public string Name {  get; set; }

        public string Surname { get; set; }

        public string City { get; set; }

        public string County { get; set; }

        public string Email { get; set; }

        public string Telephone { get; set; }

        public static List<Clients> ClientList { get; set; } = new List<Clients>();
    }
}