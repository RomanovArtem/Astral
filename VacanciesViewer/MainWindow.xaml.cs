using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VacanciesViewer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SqlConnection cn;
        private SqlDataAdapter da;
        private DataSet ds;

        public MainWindow()
        {
            InitializeComponent();
            var dataSet = new DataBase().GetContent();
            vacancyGrid.ItemsSource = dataSet.Tables[0].DefaultView;

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var dataSet = new DataBase().GetContent();
            vacancyGrid.ItemsSource = dataSet.Tables[0].DefaultView;
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
       // private void vacancyGrid_CellMouseEnter(object sender, DataGridCellEve)

      /*  public void DataSet Page_Load(int id)
        {
            var a = new DatabaseEntities();
            var da = new SqlDataAdapter("SELECT * FROM Vacancy", );
            return a.Vacancies;
        }*/
    }
}
