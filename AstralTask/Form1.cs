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

        private string _html;
        private void button1_Click(object sender, EventArgs e)
        {
            var htmlWorker = new HtmlWorker();
            _html = htmlWorker.GetHtmlPageText();
            button2.Visible = true;

        }
       
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            var listTitles = new HtmlWorker().ParseHtml(_html);
            new DataBase().WriteDb(listTitles);


           // var strok = new DataBase().GetVacancyTitle();
           // textBox1.AppendText(strok);
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
            var databaseInformation = new DataBase().GetVacancyTitle();
            textBox1.AppendText(databaseInformation);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            var sqlConnection = new DataBase()._sqlConnection;
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
            Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
    }
}
