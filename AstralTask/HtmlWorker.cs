using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace AstralTask
{
    class HtmlWorker
    {
        private readonly string _url;
        private readonly Encoding _encoding;

        public HtmlWorker()
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
                        MessageBox.Show(@"Загрузка вакансий с сайта прошла успешно");
                        return reader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }

        public List<string> ParseHtml(string html)
        {
            html = Regex.Replace(html, @"\s+", " ");
            var vacanciesSearchString = "<a class=\"list-vacancies__title\" target=\"_blank\" href=\"/vacancy/(.*?)/\" title=\"(.*?)\"";
            var matches = Regex.Matches(html, Regex.Unescape(vacanciesSearchString));

            var listTitles = new List<string>();
            foreach (Match match in matches)
            {
                var stroka = match.Value;
                var gg = stroka.Substring(stroka.IndexOf("title=\"", StringComparison.Ordinal) + 6);
                listTitles.Add(gg);
            }
           // new DataBase().WriteDb(listTitle);
            return listTitles;
        }

    }
}
