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

            WriteFile(html);
            ParseHTML(html);

        }


        public string GetHTMLPageText(string url, Encoding encoding)
        {
            var client = new WebClient();
            using (var data = client.OpenRead(url))
            {
                using (var reader = new StreamReader(data, encoding))
                {
                    return reader.ReadToEnd();
                }
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
            textBox1.AppendText(a);
            string str = Regex.Unescape(a);
            MatchCollection matches = Regex.Matches(html, str);
            foreach (Match match in matches)
            {
                comboBox1.Items.Add(match.Value);
            }
        }

      /*  public string SubStrDel(String html)
        {
            var substr = "
                           ";
            int n = html.IndexOf(substr);
            str.Remove(n, substr.Length);
            return str;
        }*/

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
