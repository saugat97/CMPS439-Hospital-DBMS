using HospitalDBMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalDBMS.Models
{
    public static class StaffWardDataAccess
    {   
        private static string _connectionString = "Server=(localdb)\\mssqllocaldb;Database=HospitalDBMS;Trusted_Connection=True;";

        public static IEnumerable<Staff> GetAllStaffs()
        {
            List<Staff> getListOfStaffs = new List<Staff>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(@"
                    SELECT
                    Staff_Id, FirstName, LastName, StaffType, Contact, Ward_No, WardName, Location
                    FROM STAFF
                    JOIN WARD ON Ward_Id = Ward_No
                ", connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Staff newStaff = new Staff();
                    Ward newWard = new Ward();

                    newStaff.Staff_Id = reader.GetInt32(0);
                    newStaff.FirstName = reader.GetString(1);
                    newStaff.LastName = reader.GetString(2);
                    newStaff.StaffType = reader.GetString(3);
                    newStaff.Contact = reader.GetString(4);

                    newWard.Ward_No = reader.GetInt32(5);
                    newWard.WardName = reader.GetString(6);
                    newWard.Location = reader.GetString(7);

                    newStaff.Ward = newWard;

                    getListOfStaffs.Add(newStaff);
                }
            }

            return getListOfStaffs;
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

        public static void UpdateStaff(StaffWardViewModel staff)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand($@"
                    UPDATE STAFF
                    SET FirstName='{staff.FirstName}', LastName='{staff.LastName}', StaffType='{staff.StaffType}', Contact='{staff.Contact}', Ward_Id={staff.Ward_Id}
                    WHERE Staff_Id = {staff.Staff_Id}
            ", connection);

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        public static void CreateStaff(StaffWardViewModel staff)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand($@"
                INSERT INTO STAFF
                (FirstName, LastName, StaffType, Contact, Ward_Id)
                VALUES('{staff.FirstName}','{staff.LastName}','{staff.StaffType}','{staff.Contact}',{staff.Ward_Id})
            ", connection);

                connection.Open();

                command.ExecuteNonQuery();
            }
        }
    }
}