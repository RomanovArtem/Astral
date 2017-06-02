using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AstralTask
{
    class DataBase
    {
        public SqlConnection _sqlConnection;

        public DataBase()
        {
            var locationDB = Application.StartupPath + @"\Database.mdf";
            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + locationDB;
            _sqlConnection = new SqlConnection(connectionString);
            _sqlConnection.Open();
        }

        public string GetVacancyTitle()
        {
            SqlDataReader sqlDataReader = null;
            var command = new SqlCommand("SELECT * FROM [Vacancy]", _sqlConnection);
            string a = "";
            try
            {
                sqlDataReader = command.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    a += Convert.ToString(sqlDataReader["id"]) + " " + Convert.ToString(sqlDataReader["title"]) + "\r\n";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlDataReader != null)
                    sqlDataReader.Close();
            }
            return a;
        }

        public void WriteDB(string title)
        {
            var command = new SqlCommand("INSERT INTO[Vacancy] (title)Values(@title)", _sqlConnection);
            command.Parameters.AddWithValue("title", title);
            command.ExecuteNonQuery();
        }
    }
}
