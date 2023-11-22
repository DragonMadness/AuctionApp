using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AuctionApp.src.database
{
    public class DatabaseAccessor
    {

        string sqlConnectionString = "Data Source = DESKTOP-39JAN8A\\SQLEXPRESS;Initial Catalog=AuctionApp;Integrated Security=true";

        public DatabaseAccessor() {

        }

        public SqlConnection connect()
        {
            try
            {
                SqlConnection conn = new SqlConnection(sqlConnectionString);
                conn.Open();
                return conn;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to connect! ex: \n" + ex, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public List<string> ExecuteSelect(string query)
        {
            SqlConnection connection = connect();

            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataReader reader = cmd.ExecuteReader();

            List<string> result = new List<string>();

            while (reader.Read())
            {
                string rowData = reader[0].ToString();
                for (int i = 1; i < reader.FieldCount; i++)
                {
                    rowData += ";" + reader[i].ToString();
                }
                result.Add(rowData);
                rowData = "";
            }
            return result;
        }

    }
}
