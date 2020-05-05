using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalDBMS.Models
{
    public class DepartmentDataAccess
    {
        private static string _connectionString = "Server=(localdb)\\mssqllocaldb;Database=HospitalDBMS;Trusted_Connection=True;";

        public static IEnumerable<Department> GetAllDepartments()
        {
            List<Department> getListOfDepartments = new List<Department>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(@"
                    SELECT  
                    Department_Id,
                    Speciality,
                    Location,
                    Contact    
                    FROM DEPARTMENT", connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Department newDepartment = new Department();

                    newDepartment.Department_Id = reader.GetInt32(0);
                    newDepartment.Speciality = reader.GetString(1);
                    newDepartment.Location = reader.GetString(2);
                    newDepartment.Contact = reader.GetString(3);

                    getListOfDepartments.Add(newDepartment);
                }
            }

            return getListOfDepartments;

        }

        public static void UpdateDepartment(Department department)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand($@"
                    UPDATE DEPARTMENT
                    SET Speciality='{department.Speciality}', Location='{department.Location}', Contact='{department.Contact}'
                    WHERE Department_Id = {department.Department_Id}", connection);

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        public static void CreateDepartment(Department department)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand($@"
                    INSERT INTO DEPARTMENT
                    (Speciality, Location, Contact) 
                    VALUES('{department.Speciality}','{department.Location}','{department.Contact}')", connection);

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        public static void DeleteDepartment(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand($@"
                    DELETE FROM DEPARTMENT
	                WHERE Department_Id = {id}", connection);

                connection.Open();

                command.ExecuteNonQuery();
            }
        }
    }
}