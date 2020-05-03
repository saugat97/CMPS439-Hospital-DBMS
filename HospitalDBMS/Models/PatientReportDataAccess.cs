using HospitalDBMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalDBMS.Models
{
    public class PatientReportDataAccess
    {
        private static string _connectionString = "Server=(localdb)\\mssqllocaldb;Database=HospitalDBMS;Trusted_Connection=True;";

        public static IEnumerable<Patient> GetAllPatients()
        {
            List<Patient> getListOfPatients = new List<Patient>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(@"
                    SELECT
                    p.Patient_Id, p.FirstName, p.LastName, p.Gender, p.Age, p.Contact, p.AdmitDateAndTime, p.ReportDateAndTime, r.Report_Id, r.Description
                    FROM PATIENT p
                    JOIN REPORT r ON p.Report_Id = r.Report_Id
                ", connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Patient newPatient = new Patient();
                    Report newReport = new Report();

                    newPatient.Patient_Id = reader.GetInt32(0);
                    newPatient.FirstName = reader.GetString(1);
                    newPatient.LastName = reader.GetString(2);
                    newPatient.Gender = reader.GetString(3);
                    newPatient.Age = reader.GetInt32(4);
                    newPatient.Contact = reader.GetString(5);
                    newPatient.AdmitDateAndTime = reader.GetDateTime(6);
                    newPatient.ReportDateAndTime = reader.GetDateTime(7);

                    newReport.Report_Id = reader.GetInt32(8);
                    newReport.Description = reader.GetString(9);

                    newPatient.Report = newReport;

                    getListOfPatients.Add(newPatient);
                }
            }

            return getListOfPatients;
        }

        public static IEnumerable<SelectListItem> GetAllReportList()
        {
            List<SelectListItem> getListOfReports = new List<SelectListItem>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(@"
                    SELECT
                    Report_Id, Description
                    FROM REPORT
                ", connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    SelectListItem newReport = new SelectListItem();

                    newReport.Value = reader.GetInt32(0).ToString();
                    newReport.Text = reader.GetString(1);

                    getListOfReports.Add(newReport);
                }
            }
            return getListOfReports;
        }

        public static void UpdatePatient(PatientReportViewModel patient)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand($@"
                    UPDATE PATIENT
                    SET FirstName='{patient.FirstName}', LastName='{patient.LastName}', Gender='{patient.Gender}', 
                        Age={patient.Age}, Contact='{patient.Contact}', AdmitDateAndTime='{patient.AdmitDateAndTime}', 
                        ReportDateAndTime='{patient.ReportDateAndTime}', Report_Id={patient.ReportId}
                    WHERE Patient_Id = {patient.Patient_Id}
            ", connection);

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        public static void CreatePatient(PatientReportViewModel patient)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand($@"
                INSERT INTO PATIENT
                (FirstName, LastName, Gender, Age, Contact, AdmitDateAndTime, ReportDateAndTime, Report_Id)
                VALUES('{patient.FirstName}','{patient.LastName}','{patient.Gender}',{patient.Age},
                        '{patient.Contact}','{patient.AdmitDateAndTime}','{patient.ReportDateAndTime}',{patient.ReportId})
            ", connection);

                connection.Open();

                command.ExecuteNonQuery();
            }
        }
    }
}