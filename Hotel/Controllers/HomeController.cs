using Hotel.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Hotel.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Info()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AdminLogin() {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminLogin([Bind(Include = "Username, Password")] Login a)
        {
            string conn = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(conn);

            try
            {
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("select * from Users where Username=@Username and Password=@Password and Role='Admin'", sqlConnection);
                sqlCommand.Parameters.AddWithValue("Username", a.Username);
                sqlCommand.Parameters.AddWithValue("Password", a.Password);

                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {

                    Session["Username"] = a.Username;
                    FormsAuthentication.SetAuthCookie(a.Username, false);
                    return RedirectToAction("Index", "Clients");
                }
                else
                {
                    ViewBag.Error = "Access denied";
                    return View();
                }
            }
            catch (Exception ex)
            {

            }
            finally { sqlConnection.Close(); }

            return View();
        }

        [HttpPost]
        public ActionResult AdminLogout()
        {
            Session.Clear();

            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult GuestRL ()
        {
            return View();
        }

        [HttpGet]
        public ActionResult RegisterGuest()
        {
            return View();
        }
        /////////////////////////////////////////////////// DA FARE REGISTRAZIONE OSPITE /////////////////////////////////

        //[HttpPost]
        //public ActionResult RegisterGuest(Guest )
        //{
        //    return RedirectToAction("GuestLogin", "Home");
        //}

        [HttpGet]
        public ActionResult GuestLogin()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GuestLogin([Bind(Include = "Username, Password")] Login a)
        {
            string conn = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(conn);

            try
            {
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("select * from Users where Username=@Username and Password=@Password and Role='User'", sqlConnection);
                sqlCommand.Parameters.AddWithValue("Username", a.Username);
                sqlCommand.Parameters.AddWithValue("Password", a.Password);

                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {

                    Session["Username"] = a.Username;
                    FormsAuthentication.SetAuthCookie(a.Username, false);
                    return RedirectToAction("ULIndex");
                }
                else
                {
                    ViewBag.Error = "You are not a registered guest";
                    return View();
                }
            }
            catch (Exception ex)
            {

            }
            finally { sqlConnection.Close(); }

            return View();
        }
        public ActionResult ALIndex ()
        {
            return View();
        }

        public ActionResult ULIndex ()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GuestLogout()
        {
            Session.Clear();

            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult FirstPartial()
        {
            return PartialView("_GeneralPartial");
        }

    }
}