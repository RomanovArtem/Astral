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
using System.Collections.Generic;
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
            var html = new Html().GetHtmlPageText();
            html = Regex.Replace(html, @"\s+", " ");
            ParseHTML(html);
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
           
            var listTitle = new List<string>();
            foreach (Match match in matches)
            {
                var stroka = match.Value;
                var gg = stroka.Substring(stroka.IndexOf("title=\"") + 6);
                listTitle.Add(gg);
                //comboBox1.Items.Add(gg);
               // textBox1.AppendText(gg);
            }
            foreach (var list in listTitle)
            {
                textBox1.AppendText(list);
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
