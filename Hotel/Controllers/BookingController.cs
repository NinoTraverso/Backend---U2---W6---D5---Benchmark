using Hotel.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hotel.Controllers
{
    public class BookingController : Controller
    {
        public ActionResult Index()
        {

            List<Booking> bookingList = new List<Booking>();

            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "select * from Bookings";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Booking booking = new Booking
                            {
                                CFID = reader["CFID"].ToString(),
                                idBooking = (int)reader["idBooking"],
                                BookDate = reader["BookDate"].ToString(),
                                CheckIn = reader["CheckIn"].ToString(),
                                CheckOut = reader["CheckOut"].ToString(),
                                Deposit = (int)reader["Deposit"],
                                RoomID = (int)reader["RoomID"],
                                FullPension = (bool)reader["FullPension"],
                                Extra = (int)reader["Extra"],
                                Total = (int)reader["Total"],
                            };

                            bookingList.Add(booking);
                        }
                    }
                }
            }

            return View(bookingList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Booking newBooking)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionstring);

            if (ModelState.IsValid)
            {
                Booking.BookingList.Add(newBooking);

                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;

                    cmd.CommandText = "insert into Bookings values (@CFID, @idBooking, @BookDate, @CheckIn, @CheckOut, @Deposit, @RoomID, @FullPension, @Extra, @Total)";

                    cmd.Parameters.AddWithValue("CFID", newBooking.CFID);
                    cmd.Parameters.AddWithValue("idBooking", newBooking.idBooking);
                    cmd.Parameters.AddWithValue("BookDate", newBooking.BookDate);
                    cmd.Parameters.AddWithValue("CheckIn", newBooking.CheckIn);
                    cmd.Parameters.AddWithValue("CheckOut", newBooking.CheckOut);
                    cmd.Parameters.AddWithValue("Deposit", newBooking.Deposit);
                    cmd.Parameters.AddWithValue("RoomID", newBooking.RoomID);
                    cmd.Parameters.AddWithValue("FullPension", newBooking.FullPension);
                    cmd.Parameters.AddWithValue("Extra", newBooking.Extra);
                    cmd.Parameters.AddWithValue("Total", newBooking.Total);

                    int insertedSuccessfully = cmd.ExecuteNonQuery();

                    if (insertedSuccessfully > 0)
                    {
                        Response.Write("Inserted into database!");

                    }

                }
                catch (Exception ex)
                {
                    Response.Write("Error" + ex.Message);
                }
                finally
                { conn.Close(); }


                return RedirectToAction("Index", "Booking");


            }
            else
            {
                return View();
            }
        }


        [HttpGet]
        public ActionResult Edit(string CFID)
        {

            return View();
        }

        [HttpPost]
        public ActionResult Edit(Booking newBooking)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionstring);

            if (ModelState.IsValid)
            {
                Booking.BookingList.Add(newBooking);

                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;

                    cmd.CommandText = "update Bookings set CFID = @CFID, idBooking = @idBooking, BookDate = @BookDate, CheckIn = @CheckIn, CheckOut = @CheckOut, Deposit = @Deposit, RoomID = @RoomID, FullPension = @FullPension, Extra = @Extra, Total = @Total where CFID = @CFID";

                    cmd.Parameters.AddWithValue("CFID", newBooking.CFID);
                    cmd.Parameters.AddWithValue("idBooking", newBooking.idBooking);
                    cmd.Parameters.AddWithValue("BookDate", newBooking.BookDate);
                    cmd.Parameters.AddWithValue("CheckIn", newBooking.CheckIn);
                    cmd.Parameters.AddWithValue("CheckOut", newBooking.CheckOut);
                    cmd.Parameters.AddWithValue("Deposit", newBooking.Deposit);
                    cmd.Parameters.AddWithValue("RoomID", newBooking.RoomID);
                    cmd.Parameters.AddWithValue("FullPension", newBooking.FullPension);
                    cmd.Parameters.AddWithValue("Extra", newBooking.Extra);
                    cmd.Parameters.AddWithValue("Total", newBooking.Total);

                    int insertedSuccessfully = cmd.ExecuteNonQuery();

                    if (insertedSuccessfully > 0)
                    {
                        Response.Write("Database updated!");

                    }

                }
                catch (Exception ex)
                {
                    Response.Write("Error" + ex.Message);
                }
                finally
                { conn.Close(); }


                return RedirectToAction("Index", "Booking");


            }
            else
            {
                return View();
            }
        }



        [HttpPost]
        public ActionResult Delete(string CFID)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "delete from Bookigs where CFID = @CFID";
                        cmd.Parameters.AddWithValue("@CFID", CFID);

                        int deletedRow = cmd.ExecuteNonQuery();

                        if (deletedRow > 0)
                        {
                            Response.Write("Database updated!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error" + ex.Message);
            }

            return RedirectToAction("Index", "Booking");
        }

    }
}
