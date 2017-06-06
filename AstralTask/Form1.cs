using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace AstralTask
{
    public partial class Form1 : Form
    {
        private string _html;
        private List<string> _listTitles;
        public Form1()
        {
            InitializeComponent();
        }

        public void WriteFile(string s)
        {
            StreamWriter SW = new StreamWriter(new FileStream("FileName.txt", FileMode.Create, FileAccess.Write));
            SW.Write(s);
            SW.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.hh.ru");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "M0ULRI6D64B9JQNTM26TFSNUILLMM7AKESA7V3N5BP3TALLM5UR7140697AQ46BR");
                client.DefaultRequestHeaders.Add("User-Agent", "api-test-agent");
              
                var result = client.GetAsync("/vacancies?describe_arguments=true&area=1&per_page=50").Result;
                string resultContent = result.Content.ReadAsStringAsync().Result;
             //   textBox1.AppendText(resultContent);
                WriteFile(resultContent);

                var newObject = JsonConvert.DeserializeObject<RootObject>(resultContent);
                var b = "";
                var count = 0;
                foreach (var item in newObject.items)
                {
                    var title = item.name;
                    var salary = item.salary == null
                        ? "Не указана"
                        : item.salary.@from + " - " + item.salary.to + " " + item.salary.currency;
                    var employer = item.employer.name;
                    var url = item.alternate_url;
                    var requirement = item.snippet.requirement ?? "Не указаны";
                    var responsibility = item.snippet.responsibility ?? "Не указаны";
                    var address = item.address == null || (item.address.city == null && item.address.street == null &&
                                                       item.address.raw == null)
                        ? "Не указан"
                        : item.address.city + " " + item.address.street;

                    //new DataBase().WriteDataDb(title, salary, employer, url, requirement, responsibility, address);
                    textBox1.AppendText(++count + " "+ address + Environment.NewLine);
                }

                textBox1.AppendText("dвсе");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //new DataBase().WriteDataDb(TODO, TODO, TODO, TODO, TODO, TODO, TODO);
            textBox1.Clear();
            label1.Text = @"Данные сохранены в БД";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            var databaseInformation = new DataBase().GetVacancyTitle();
            if (databaseInformation.Length == 0)
                databaseInformation = "В БД нет данных!";
            textBox1.AppendText(databaseInformation);
            label1.Text = @"Данные загружены из БД";
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            textBox1.Clear();
            label1.Text = @"Данные из БД удалены";
            new DataBase().DeleteDataDb();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
