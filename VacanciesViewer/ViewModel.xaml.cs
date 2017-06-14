using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace VacanciesViewer
{

    public partial class MainWindow
    {

        DataLoader dataLoader = new DataLoader();


        public MainWindow()
        {
            InitializeComponent();
            VacancyGrid.ItemsSource = new DataBase().GetContent("");
        }

        private void buttonDownloadClick(object sender, RoutedEventArgs e)
        {  
                dataLoader.Load();
                dataLoader.RecordInformationDatabase();
            VacancyGrid.ItemsSource = new DataBase().GetContent("");   
        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

            PropertyDescriptor propertyDescriptor = (PropertyDescriptor) e.PropertyDescriptor;
            e.Column.Header = propertyDescriptor.DisplayName;
            if (propertyDescriptor.DisplayName == "requirement" || propertyDescriptor.DisplayName == "responsibility" || propertyDescriptor.DisplayName == "Id")
            {
                e.Cancel = true;
            }
            var a = e.Column.Header;
            switch (propertyDescriptor.DisplayName)
            {
                case "title":
                    e.Column.Header = "Название";
                    break;
                case "salary":
                    e.Column.Header = "Зарплата";
                    break;
                case "employer":
                    e.Column.Header = "Работодатель";
                    break;
                case "address":
                    e.Column.Header = "Адрес";
                    break;
            }
        }

        private void LabelUrl_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(LabelUrlText.DataContext.ToString());
        }

        private void searchData_TextChanged(object sender, TextChangedEventArgs e)
        {
            var data = new DataBase().GetContent(SearchData.Text);
            if (data != null)
            {
                VacancyGrid.ItemsSource = data;
            }
            else
            {
                MessageBox.Show("Ничего не найдено");

            }
        }
    }
}

