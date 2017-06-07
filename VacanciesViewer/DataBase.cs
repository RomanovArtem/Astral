using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mime;


namespace VacanciesViewer
{
    class DataBase
    {
            private readonly SqlConnection _sqlConnection;

            public DataBase()
            {
                try
                {
                    var connectionString = @"Data Source=ARTEM\SQLEXPRESS;Initial Catalog=DataBaseVacancy;Integrated Security=true";
                    _sqlConnection = new SqlConnection(connectionString);
                    _sqlConnection.Open();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            public DataSet GetContent()
            {
                var dataSet = new DataSet();
            try
                {
                var dataAdapter = new SqlDataAdapter("Select * from Vacancy", _sqlConnection);
                
                dataAdapter.Fill(dataSet);
                _sqlConnection.Close();

                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return dataSet;
            }

            public void WriteDataDb(string title, string salary, string employer, string url, string requirement,
                string responsibility, string address)
            {
                try
                {
                    using (var command =
                        new SqlCommand(
                            "INSERT INTO Vacancy Values(@title, @salary, @employer, @url, @requirement, @responsibility, @address)",
                            _sqlConnection))
                    {
                        command.Parameters.AddWithValue("title", title);
                        command.Parameters.AddWithValue("salary", salary);
                        command.Parameters.AddWithValue("employer", employer);
                        command.Parameters.AddWithValue("url", url);
                        command.Parameters.AddWithValue("requirement", requirement);
                        command.Parameters.AddWithValue("responsibility", responsibility);
                        command.Parameters.AddWithValue("address", address);
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
                var command = new SqlCommand("DELETE FROM Vacancy", _sqlConnection);
                command.ExecuteNonQuery();
            }
    }
}
