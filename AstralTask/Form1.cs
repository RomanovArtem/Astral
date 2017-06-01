using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
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
            var a = "https://rabota.yandex.ru/search?job_industri=275&rid=213&page_num=2";
            var b = Encoding.UTF8;
            //textBox1.AppendText(GetHTMLPageText(a, b));
            File(GetHTMLPageText(a, b));

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

        public void File(string s)
        {
            StreamWriter SW = new StreamWriter(new FileStream("FileName.txt", FileMode.Create, FileAccess.Write));
            SW.Write(s);
            SW.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
