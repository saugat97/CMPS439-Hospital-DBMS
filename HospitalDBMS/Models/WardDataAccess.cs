using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HospitalDBMS.Models
{
    public class WardDataAccess
    {
        private static string _connectionString = "Server=(localdb)\\mssqllocaldb;Database=HospitalDBMS;Trusted_Connection=True;";

        public static IEnumerable<Ward> GetAllWards()
        {
            List<Ward> getListOfWards = new List<Ward>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(@"
                    SELECT  
                    Ward_No,
                    WardName,
                    Location  
                    FROM WARD 
                    ", connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Ward newWard = new Ward();

                    newWard.Ward_No = reader.GetInt32(0);
                    newWard.WardName = reader.GetString(1);
                    newWard.Location = reader.GetString(2);

                    getListOfWards.Add(newWard);
                }
            }

            return getListOfWards;

        }

        public static void UpdateWard(Ward ward)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand($@"
                    UPDATE WARD
                    SET WardName='{ward.WardName}', Location='{ward.Location}'
                    WHERE Ward_No = {ward.Ward_No}", connection);

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        public static void CreateWard(Ward ward)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand($@"
                    INSERT INTO WARD
                    (WardName, Location) 
                    VALUES('{ward.WardName}','{ward.Location}')", connection);

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        public static void DeleteWard(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand($@"
                    DELETE FROM WARD
	                WHERE Ward_No = {id}", connection);

                connection.Open();

                command.ExecuteNonQuery();
            }
        }
    }
}
  