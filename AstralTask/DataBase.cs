using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AstralTask
{
    class DataBase
    {
        private readonly SqlConnection _sqlConnection;

        public DataBase()
        {
            try
            {
                var locationDb = Application.StartupPath + @"\Database.mdf";
                var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + locationDb;
                _sqlConnection = new SqlConnection(connectionString);
                _sqlConnection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string GetVacancyTitle()
        {
            SqlDataReader sqlDataReader = null;
            var command = new SqlCommand("SELECT * FROM Vacancy", _sqlConnection);
            var html = "";
            try
            {
                sqlDataReader = command.ExecuteReader();
                var count = 0;
                while (sqlDataReader.Read())
                {
                    count++;
                    html += count + " " + Convert.ToString(sqlDataReader["title"]) + " " + Convert.ToString(sqlDataReader["url"]) + "\r\n";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlDataReader != null)
                    sqlDataReader.Close();
            }
            return html;
        }

        public void WriteDataDb(string title, string salary, string employer, string url, string requirement,
            string responsibility, string address)
        {
            try
            {
                using (SqlCommand command =
                    new SqlCommand(
                        "INSERT INTO Vacancy Values(@title, @salary, @employer, @url, @requirement, @responsibility, @address)",
                        _sqlConnection))
                {
                    command.Parameters.AddWithValue("title", title);
                    command.Parameters.AddWithValue("salary", title);
                    command.Parameters.AddWithValue("employer", title);
                    command.Parameters.AddWithValue("url", url);
                    command.Parameters.AddWithValue("requirement", title);
                    command.Parameters.AddWithValue("responsibility", title);
                    command.Parameters.AddWithValue("address", title);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void DeleteDataDb()
        {
            var command = new SqlCommand("DELETE FROM[Vacancy]", _sqlConnection);
            command.ExecuteNonQuery();
        }
    }
}
