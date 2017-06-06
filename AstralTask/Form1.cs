using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Forms;

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


        private void button1_Click(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.hh.ru");
                client.DefaultRequestHeaders.Add("User-Agent", "api-test-agent");
              
                var result = client.GetAsync("/vacancies").Result;
                string resultContent = result.Content.ReadAsStringAsync().Result;
                textBox1.AppendText(resultContent);

                var data = JsonConvert.
                
            }


            /*var htmlWorker = new HtmlWorker();
            _html = htmlWorker.GetHtmlPageText();
            label1.Text = _html != "" ? @"Данные загружены с сайта https://www.rabota.ru/vacancy" : @"Ошибка загрузки данных с сайта https://www.rabota.ru/vacancy";
            
            button2.Visible = true;

            _listTitles = new HtmlWorker().ParseHtml(_html);
            textBox1.Clear();
            foreach (var title in _listTitles)
            {
                textBox1.AppendText(title + "\t\n");
            } */
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            new DataBase().WriteDataDb(_listTitles);
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
