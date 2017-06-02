using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AstralTask
{
    class DataBaseReader
    {
        public SqlConnection _sqlConnection;

        public DataBaseReader()
        {
            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ArtemRomanov\documents\visual studio 2015\Projects\AstralTask\AstralTask\Database.mdf";
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
    }
}
