using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows;


namespace VacanciesViewer
{
    class DataBase
    {
        private readonly SqlConnection _sqlConnection;

        public DataBase()
        {
            try
            {
                var locationDb = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database.mdf");

                var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + locationDb;
                _sqlConnection = new SqlConnection(connectionString);
                _sqlConnection.Open();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, exception.Source);
            }
        }

        public DataView GetContent(string filter)
        {
            var dataSet = new DataSet();
            try
            {
                string query;
                if (filter == "")
                {
                    query = "SELECT * FROM Vacancy";
                }
                else
                {
                    filter = " N'%" + filter + "%' ";

                    query = "SELECT * FROM Vacancy WHERE title LIKE" + filter + "or salary LIKE" +
                            filter + "or employer LIKE" + filter + "or requirement LIKE" + filter +
                            "or responsibility LIKE" + filter + "or address LIKE" + filter;
                }

                var dataAdapter = new SqlDataAdapter(query, _sqlConnection);
                dataAdapter.Fill(dataSet);
                _sqlConnection.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, exception.Source);
            }
            return dataSet.Tables[0].DefaultView.Count > 0 ? dataSet.Tables[0].DefaultView : null;
        }

        public void Write(VacancyObject data)
        {
            try
            {
                using (var command =
                    new SqlCommand(
                        "INSERT INTO Vacancy Values(@title, @salary, @employer, @url, @requirement, @responsibility, @address)",
                        _sqlConnection))
                {
                    command.Parameters.AddWithValue("title", data.Title);
                    command.Parameters.AddWithValue("salary", data.Salary);
                    command.Parameters.AddWithValue("employer", data.Employer);
                    command.Parameters.AddWithValue("url", data.Url);
                    command.Parameters.AddWithValue("requirement", data.Requirement);
                    command.Parameters.AddWithValue("responsibility", data.Responsibility);
                    command.Parameters.AddWithValue("address", data.Address);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, exception.Source);
            }
        }
    }
}