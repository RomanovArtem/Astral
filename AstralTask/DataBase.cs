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
            var locationDb = Application.StartupPath + @"\Database.mdf";
            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + locationDb;
            _sqlConnection = new SqlConnection(connectionString);
            _sqlConnection.Open();
        }

        public string GetVacancyTitle()
        {
            SqlDataReader sqlDataReader = null;
            var command = new SqlCommand("SELECT * FROM [Vacancy]", _sqlConnection);
            var html = "";
            try
            {
                sqlDataReader = command.ExecuteReader();
                var count = 0;
                while (sqlDataReader.Read())
                {
                    count++;
                    html += count + " " + Convert.ToString(sqlDataReader["title"]) + "\r\n";
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

        public void WriteDataDb(List<string> listTitle)
        {
           
            foreach (var list in listTitle)
            {
                var command = new SqlCommand("INSERT INTO[Vacancy] (title)Values(@title)", _sqlConnection);
                command.Parameters.AddWithValue("title", list);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteDataDb()
        {
            var command = new SqlCommand("DELETE FROM[Vacancy]", _sqlConnection);
            command.ExecuteNonQuery();
        }
    }
}
