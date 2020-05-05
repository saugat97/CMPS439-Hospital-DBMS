using HospitalDBMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalDBMS.Models
{
    public class PatientWardDataAccess
    {
        private static string _connectionString = "Server=(localdb)\\mssqllocaldb;Database=HospitalDBMS;Trusted_Connection=True;";

        public static IEnumerable<Patient> GetAllPatients()
        {
            List<Patient> getListOfPatients = new List<Patient>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(@"
                    SELECT
                    Patient_Id, FirstName, LastName, Gender, Age, Contact, AdmitDateAndTime, Ward_No, WardName
                    FROM PATIENT 
                    JOIN WARD ON Ward_Id = Ward_No
                ", connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Patient newPatient = new Patient();
                    Ward newWard = new Ward();

                    newPatient.Patient_Id = reader.GetInt32(0);
                    newPatient.FirstName = reader.GetString(1);
                    newPatient.LastName = reader.GetString(2);
                    newPatient.Gender = reader.GetString(3);
                    newPatient.Age = reader.GetInt32(4);
                    newPatient.Contact = reader.GetString(5);
                    newPatient.AdmitDateAndTime = reader.GetDateTime(6);
                    
                    newWard.Ward_No = reader.GetInt32(7);
                    newWard.WardName = reader.GetString(8);

                    newPatient.Ward = newWard;

                    getListOfPatients.Add(newPatient);
                }
            }

            return getListOfPatients;
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

        public static void UpdatePatient(PatientWardViewModel patient)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand($@"
                    UPDATE PATIENT
                    SET FirstName='{patient.FirstName}', LastName='{patient.LastName}', Gender='{patient.Gender}', 
                        Age={patient.Age}, Contact='{patient.Contact}', AdmitDateAndTime='{patient.AdmitDateAndTime}', 
                        Ward_Id={patient.Ward_Id}
                    WHERE Patient_Id = {patient.Patient_Id}
            ", connection);

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        public static void CreatePatient(PatientWardViewModel patient)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand($@"
                INSERT INTO PATIENT
                (FirstName, LastName, Gender, Age, Contact, AdmitDateAndTime, Ward_Id)
                VALUES('{patient.FirstName}','{patient.LastName}','{patient.Gender}',{patient.Age},
                        '{patient.Contact}','{patient.AdmitDateAndTime}',{patient.Ward_Id})
            ", connection);

                connection.Open();

                command.ExecuteNonQuery();
            }
        }
    }
}