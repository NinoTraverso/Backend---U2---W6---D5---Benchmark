using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hotel.Models
{
    public class Booking
    {
        public string CFID { get; set; }

        public int idBooking { get; set; }

        public string BookDate { get; set; }

        public string CheckIn { get; set; }

        public string CheckOut { get; set; }

        public int Deposit { get; set; }

        public int RoomID { get; set; }

        public bool FullPension { get; set; }

        public int Extra { get; set; }

        public int Total { get; set; }

        public static List<Booking> BookingList { get; set; } = new List<Booking>();

    }
}