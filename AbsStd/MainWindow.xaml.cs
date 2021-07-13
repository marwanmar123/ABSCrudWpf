using System;
using System.Collections.Generic;
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
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace AbsStd
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SqlConnection connection = new SqlConnection();
        String connect = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;


        public MainWindow()
        {
            InitializeComponent();
            getcountdb();
        }


        private void getcountdb()
        {


            connection.ConnectionString = connect;
            connection.Open();
            string query = $"SELECT COUNT(*) FROM Apprenant";
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();

            // Executes the query, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored.
            int numRecords = Convert.ToInt32(command.ExecuteScalar());

            tajriba.Text = numRecords.ToString();
            connection.Close();

        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_edit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DG_Zone_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btn_appr_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                apprenant winapprenant = new apprenant();
                winapprenant.Show();


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex + "");
            }

        }

        private void btn_scrt_Click(object sender, RoutedEventArgs e)
        {
            secretaire secretaire = new secretaire();
            secretaire.Show();
        }

        private void btn_formt_Click(object sender, RoutedEventArgs e)
        {
            Formateur1 forma = new Formateur1();
            forma.Show();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
