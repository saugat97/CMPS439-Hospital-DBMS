using HospitalDBMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalDBMS.Models
{
    public static class NurseWardDataAccess
    {
        private static string _connectionString = "Server=(localdb)\\mssqllocaldb;Database=HospitalDBMS;Trusted_Connection=True;";

        public static IEnumerable<Nurse> GetAllNurses()
        {
            List<Nurse> getListOfNurses = new List<Nurse>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(@"
                    SELECT
                    Nurse_Id, FirstName, LastName, Contact, Ward_No, WardName, Location
                    FROM NURSE
                    JOIN WARD ON WardId = Ward_No
                ", connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Nurse newNurse = new Nurse();
                    Ward newWard = new Ward();

                    newNurse.Nurse_Id = reader.GetInt32(0);
                    newNurse.FirstName = reader.GetString(1);
                    newNurse.LastName = reader.GetString(2);
                    newNurse.Contact = reader.GetString(3);

                    newWard.Ward_No = reader.GetInt32(4);
                    newWard.WardName = reader.GetString(5);
                    newWard.Location = reader.GetString(6);

                    newNurse.Ward = newWard;

                    getListOfNurses.Add(newNurse);
                }
            }

            return getListOfNurses;
        }

        public static IEnumerable<SelectListItem> GetAllWardList()
        {
            List<SelectListItem> getListOfWards = new List<SelectListItem>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(@"
                    SELECT
                    Ward_No, WardName
                    FROM WARD
                ", connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    SelectListItem newWard = new SelectListItem();

                    newWard.Value = reader.GetInt32(0).ToString();
                    newWard.Text = reader.GetString(1);

                    getListOfWards.Add(newWard);
                }
            }

            return getListOfWards;
        }

        public static void UpdateNurse(NurseWardViewModel nurse)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand($@"
                    UPDATE NURSE
                    SET FirstName='{nurse.FirstName}', LastName='{nurse.LastName}', Contact='{nurse.Contact}', WardId={nurse.WardId}
                    WHERE Nurse_Id = {nurse.Nurse_Id}
            ", connection);

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        public static void CreateNurse(NurseWardViewModel nurse)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand($@"
                INSERT INTO NURSE
                (FirstName, LastName, Contact, WardId)
                VALUES('{nurse.FirstName}','{nurse.LastName}','{nurse.Contact}',{nurse.WardId})
            ", connection);

                connection.Open();

                command.ExecuteNonQuery();
            }
        }
    }
}