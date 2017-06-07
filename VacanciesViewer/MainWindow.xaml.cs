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

        }

     

        private void button_Click(object sender, RoutedEventArgs e)
        {
            cn = new SqlConnection(@"Data Source=ARTEM\SQLEXPRESS;Initial Catalog=DataBaseVacancy;Integrated Security=true");

            da = new SqlDataAdapter("Select * from Vacancy", cn);
            ds = new DataSet();
            da.Fill(ds);
            dataGrid.ItemsSource = ds.Tables[0].DefaultView;
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

      /*  public void DataSet Page_Load(int id)
        {
            var a = new DatabaseEntities();
            var da = new SqlDataAdapter("SELECT * FROM Vacancy", );
            return a.Vacancies;
        }*/
    }
}
