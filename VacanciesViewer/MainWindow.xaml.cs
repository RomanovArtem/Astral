using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Newtonsoft.Json;

namespace VacanciesViewer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SqlConnection cn;
        private SqlDataAdapter da;
        private DataSet ds;

        public MainWindow()
        {
            InitializeComponent();
            var dataSet = new DataBase().GetContent("");
            vacancyGrid.ItemsSource = dataSet.Tables[0].DefaultView;

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.hh.ru");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    "M0ULRI6D64B9JQNTM26TFSNUILLMM7AKESA7V3N5BP3TALLM5UR7140697AQ46BR");
                client.DefaultRequestHeaders.Add("User-Agent", "api-test-agent");

                var result = client.GetAsync("/vacancies?describe_arguments=true&area=1&per_page=50").Result;
                var resultContent = result.Content.ReadAsStringAsync().Result;

            //   textBox1.AppendText(resultContent);
                WriteFile(resultContent);

                var newObject = JsonConvert.DeserializeObject<RootObject>(resultContent);
                var b = "";
                var count = 0;
                foreach (var item in newObject.items)
                {
                    var title = item.name;
                    var salary = "";
                    if (item.salary != null)
                    {
                        if (item.salary.@from == null)
                        {
                            salary = "до " + item.salary.to + " " + item.salary.currency;
                        }
                        if (item.salary.to == null)
                        {
                            salary = "от " + item.salary.@from + " " + item.salary.currency;
                        }
                        else
                        {
                            salary = "от " + item.salary.@from + " до " + item.salary.to + " " + item.salary.currency;
                        }
                    }
                    else
                    {
                        salary = "Не указана";
                    }
                    var salarya = item.salary == null
                        ? "Не указана"
                        : item.salary.@from + " - " + item.salary.to + " " + item.salary.currency;
                    var employer = item.employer.name;
                    var url = item.alternate_url;
                    var requirement = item.snippet.requirement?.Replace(". ", "\n") ?? "Не указаны";
                    var responsibility = item.snippet.responsibility ?? "Не указаны";
                    var address = item.address == null || (item.address.city == null && item.address.street == null &&
                                                           item.address.raw == null)
                        ? "Не указан"
                        : item.address.city + " " + item.address.street;

                    new DataBase().WriteDataDb(title, salary, employer, url, requirement, responsibility, address);
                   // textBox1.AppendText(++count + " " + title + " " + salary + " " + employer + " " + url + " " +
                   //                     requirement + " " + responsibility + " " + address + Environment.NewLine);
                }
                // label1.Text = @"Данные добавлены";
                var dataSet = new DataBase().GetContent("");

                vacancyGrid.ItemsSource = dataSet.Tables[0].DefaultView;
            }
        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            PropertyDescriptor propertyDescriptor = (PropertyDescriptor) e.PropertyDescriptor;
            e.Column.Header = propertyDescriptor.DisplayName;
            if (propertyDescriptor.DisplayName == "requirement" || propertyDescriptor.DisplayName == "responsibility")
            {
                e.Cancel = true;
            }
        }

        private void LabelUrl_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(labelUrlText.DataContext.ToString());
        }


        public void WriteFile(string s)
        {
            StreamWriter SW = new StreamWriter(new FileStream("FileName.txt", FileMode.Create, FileAccess.Write));
            SW.Write(s);
            SW.Close();
        }

        private void SearchData_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            //((DataView) vacancyGrid.DataContext).RowFilter = "(A LIKE '" + searchData.Text + "*')";
        }

        public void button1_Click(object sender, RoutedEventArgs e)
        {
            var dataSet = new DataBase().GetContent(searchData.Text);
            if (dataSet.Tables[0].DefaultView.Count > 0)
            {
                vacancyGrid.ItemsSource = dataSet.Tables[0].DefaultView;
            }
            else
            {
                MessageBox.Show("Ничего не найдено!");
            }
            
        }
    }
}
