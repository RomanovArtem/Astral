using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace AstralTask
{
    class Html
    {
        public string _url;
        public Encoding _encoding;

        public Html()
        {
            _url= "https://www.rabota.ru/vacancy";
            _encoding = Encoding.UTF8;
        }

        public string GetHtmlPageText()
        {
            var client = new WebClient();
            try
            {
                using (var data = client.OpenRead(_url))
                {
                    using (var reader = new StreamReader(data, _encoding))
                    {
                        MessageBox.Show(String.Format("Загрузка вакансий с сайта прошла успешно"));
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
    }
}
