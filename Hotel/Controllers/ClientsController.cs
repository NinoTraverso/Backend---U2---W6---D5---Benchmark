using Hotel.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Net;

namespace Hotel.Controllers
{
    public class ClientsController : Controller
    {
        
        public ActionResult Index()
        {

            List<Clients> clientList = new List<Clients>();

            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "select * from Clients";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Clients client = new Clients
                            {
                                IdCF = reader["IdCF"].ToString(),
                                Name = reader["Name"].ToString(),
                                Surname = reader["Surname"].ToString(),
                                City = reader["City"].ToString(),
                                County = reader["County"].ToString(),
                                Email = reader["Email"].ToString(),
                                Telephone = reader["Telephone"].ToString()
                            };

                            clientList.Add(client);
                        }
                    }
                }
            }

            return View(clientList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Clients newClient)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionstring);

            if (ModelState.IsValid)
            {
                Clients.ClientList.Add(newClient);

                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;

                    cmd.CommandText = "insert into Clients values (@IdCF, @Name, @Surname, @City, @County, @Email, @Telephone)";

                    cmd.Parameters.AddWithValue("IdCF", newClient.IdCF);
                    cmd.Parameters.AddWithValue("Name", newClient.Name);
                    cmd.Parameters.AddWithValue("Surname", newClient.Surname);
                    cmd.Parameters.AddWithValue("City", newClient.City);
                    cmd.Parameters.AddWithValue("County", newClient.County);
                    cmd.Parameters.AddWithValue("Email", newClient.Email);
                    cmd.Parameters.AddWithValue("Telephone", newClient.Telephone);

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


                return RedirectToAction("Index", "Clients");


            }
            else
            {
                return View();
            }
        }


        [HttpGet]
        public ActionResult Edit(string IdCf)
        {

            return View();
        }

        [HttpPost]
        public ActionResult Edit(Clients newClient)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionstring);

            if (ModelState.IsValid)
            {
                Clients.ClientList.Add(newClient);

                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;

                    cmd.CommandText = "update Clients set Name = @Name, Surname = @Surname, City = @City, County = @County, Email = @Email, Telephone = @Telephone where IdCF = @IdCF";

                    cmd.Parameters.AddWithValue("IdCF", newClient.IdCF);
                    cmd.Parameters.AddWithValue("Name", newClient.Name);
                    cmd.Parameters.AddWithValue("Surname", newClient.Surname);
                    cmd.Parameters.AddWithValue("City", newClient.City);
                    cmd.Parameters.AddWithValue("County", newClient.County);
                    cmd.Parameters.AddWithValue("Email", newClient.Email);
                    cmd.Parameters.AddWithValue("Telephone", newClient.Telephone);

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


                return RedirectToAction("Index", "Clients");


            }
            else
            {
                return View();
            }
        }



        [HttpPost]
        public ActionResult Delete(string IdCF)
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
                        cmd.CommandText = "delete from Clients where idCF = @idCF";
                        cmd.Parameters.AddWithValue("@idCF", IdCF);

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

            return RedirectToAction("Index", "Clients");
        }
        
    }
}
