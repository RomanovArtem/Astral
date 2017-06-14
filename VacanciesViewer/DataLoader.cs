using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;
using Newtonsoft.Json;

namespace VacanciesViewer
{
    internal class DataLoader
    {
        public VacancyObject VacancyObject = new VacancyObject();
        public DataBase DataBase = new DataBase();
        public RootObject InfoVacancy = new RootObject();

        public void Load()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://api.hh.ru");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                        "M0ULRI6D64B9JQNTM26TFSNUILLMM7AKESA7V3N5BP3TALLM5UR7140697AQ46BR");
                    client.DefaultRequestHeaders.Add("User-Agent", "api-test-agent");

                    var result = client.GetAsync("/vacancies?describe_arguments=true&area=1&per_page=50").Result;
                    var responseMessage = result.Content.ReadAsStringAsync().Result;
                    InfoVacancy = JsonConvert.DeserializeObject<RootObject>(responseMessage);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, exception.Source);
            }
        }


        public VacancyObject Parse(Item item)
        {
            VacancyObject.Title = item.name;
            VacancyObject.Salary = "";
            if (item.salary != null)
            {
                if (item.salary.@from == null)
                {
                    VacancyObject.Salary = "до " + item.salary.to + " " + item.salary.currency;
                }
                if (item.salary.to == null)
                {
                    VacancyObject.Salary = "от " + item.salary.@from + " " + item.salary.currency;
                }
                else
                {
                    VacancyObject.Salary = "от " + item.salary.@from + " до " + item.salary.to + " " + item.salary.currency;
                }
            }
            else
            {
                VacancyObject.Salary = "Не указана";
            }
            
            VacancyObject.Employer = item.employer.name;
            VacancyObject.Url = item.alternate_url;

            VacancyObject.Requirement = item.snippet.requirement == null ? "Не указаны" : string.Concat("\n", item.snippet.requirement.Replace(". ", "\n"));
            VacancyObject.Responsibility = item.snippet.responsibility == null ? "Не указаны" : string.Concat("\n", item.snippet.responsibility.Replace(". ", "\n"));
            if (item.address == null || (item.address.city == null && item.address.street == null && item.address.raw == null))
            {
                VacancyObject.Address = "Не указан";
            }
            else if (item.address.city == null && item.address.street == null)
            {
                VacancyObject.Address = item.address.raw;
            }
            else
            {
                VacancyObject.Address = item.address.city + " " + item.address.street;
            }
            return VacancyObject;
        }

        public void RecordInformationDatabase()
        {
            foreach (var item in InfoVacancy.items)
            {
                var data = Parse(item);
                try
                {
                    DataBase.Write(data);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, exception.Source);
                }
            }
        }
    }
}
