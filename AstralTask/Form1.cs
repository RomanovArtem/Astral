using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AstralTask
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var a = "https://www.rabota.ru/vacancy";
            var b = Encoding.UTF8;
            //textBox1.AppendText(GetHTMLPageText(a, b));
            var html = GetHTMLPageText(a, b);
            html = Regex.Replace(html, @"\s+", " ");

            // WriteFile(html);
            ParseHTML(html);
        }


        public string GetHTMLPageText(string url, Encoding encoding)
        {
            var client = new WebClient();
            try
            {
                using (var data = client.OpenRead(url))
                {
                    using (var reader = new StreamReader(data, encoding))
                    {
                        MessageBox.Show(String.Format("Загрузка вакансий с сайта прошла успешно"));
                        button2.Visible = true;
                        return reader.ReadToEnd();
                    }
                }
            }
            catch
            {
                MessageBox.Show(String.Format("Ошибка при загрузке вакансий с сайта!"));
                return "";
            }
        }

        public void WriteFile(string s)
        {
            StreamWriter SW = new StreamWriter(new FileStream("FileName.txt", FileMode.Create, FileAccess.Write));
            SW.Write(s);
            SW.Close();
        }

        public void ParseHTML(string html)
        {
            string a = "<a class=\"list-vacancies__title\" target=\"_blank\" href=\"/vacancy/(.*?)/\" title=\"(.*?)\"";
            string str = Regex.Unescape(a);
            MatchCollection matches = Regex.Matches(html, str);
            foreach (Match match in matches)
            {
                var stroka = match.Value;
                var gg = stroka.Substring(stroka.IndexOf("title=\"") + 6);
                comboBox1.Items.Add(gg);
                textBox1.AppendText(gg);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            string a = "hello, привет";
            new DataBase().WriteDB(a);

            var strok = new DataBase().GetVacancyTitle();
            textBox1.AppendText(strok);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var sqlConnection = new DataBase()._sqlConnection;
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            var strok = new DataBase().GetVacancyTitle();
            textBox1.AppendText(strok);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            var sqlConnection = new DataBase()._sqlConnection;
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
            Close();
        }
    }
}
