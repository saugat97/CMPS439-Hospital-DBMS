using HospitalDBMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalDBMS.Models
{
    public static class DataAccess
    {
        private static string _connectionString = "Server=(localdb)\\mssqllocaldb;Database=HospitalDBMS;Trusted_Connection=True;";

        public static IEnumerable<Doctor> GetAllDoctors()
        {
            List<Doctor> getListDoctors = new List<Doctor>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(@"
                    SELECT  
                        D.Doctor_Id,
                        D.FirstName, 
                        D.LastName,
                        D.Contact,
                        X.Department_Id,
                        X.Speciality,
                        X.Location,
                        X.Contact,
                        X.NoOfDoctors
                    FROM DOCTOR D 
                    JOIN DEPARTMENT X ON X.Department_Id = D.Dept_Id", connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Doctor newDoctor = new Doctor();
                    Department newDepartment = new Department();

                    newDoctor.Doctor_Id = reader.GetInt32(0);
                    newDoctor.FirstName = reader.GetString(1);
                    newDoctor.LastName = reader.GetString(2);
                    newDoctor.Contact = reader.GetString(3);

                    newDepartment.Department_Id = reader.GetInt32(4);
                    newDepartment.Speciality = reader.GetString(5);
                    newDepartment.Location = reader.GetString(6);
                    newDepartment.Contact = reader.GetString(7);
                    newDepartment.NoOfDoctors = reader.GetInt32(8);

                    newDoctor.Department = newDepartment;

                    getListDoctors.Add(newDoctor);
                }
            }

            return getListDoctors;

        }

        public static IEnumerable<SelectListItem> GetAllDepartmentList()
        {
            List<SelectListItem> getDepartmentList = new List<SelectListItem>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(@"
                    SELECT  
                        Department_Id,
                        Speciality
                    FROM DEPARTMENT", connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {                 
                    SelectListItem newDepartment = new SelectListItem();

                    newDepartment.Value = reader.GetInt32(0).ToString();
                    newDepartment.Text = reader.GetString(1);

                    getDepartmentList.Add(newDepartment);
                }
            }

            return getDepartmentList;
        }

        public static void UpdateDoctor(DoctorDeptViewModel doctor)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand($@"
                    UPDATE DOCTOR
                    SET FirstName='{doctor.FirstName}', LastName='{doctor.LastName}', Contact='{doctor.Contact}', Dept_Id= {doctor.Dept_Id}
                    WHERE Doctor_Id = {doctor.Doctor_Id}", connection);

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        public static void CreateDoctor(DoctorDeptViewModel doctor)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand($@"
                    INSERT INTO DOCTOR
                    (FirstName, LastName, Contact, Dept_Id) 
                    VALUES('{doctor.FirstName}','{doctor.LastName}','{doctor.Contact}',{doctor.Dept_Id})", connection);

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

    }
}