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
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;

namespace AbsStd
{
    /// <summary>
    /// Interaction logic for apprenant.xaml
    /// </summary>
    public partial class apprenant : Window
    {
        private SqlConnection connection = new SqlConnection();
        String connect = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
        int id_selected;
        private StringBuilder errors;
        private Regex validator;

        public apprenant()
        {
            InitializeComponent();
            loaddata();


        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {

            if (AreFieldsValid())
            {
                connection.Close();
                connection.ConnectionString = connect;
                connection.Open();
                string query = $"INSERT INTO Apprenant ( nom, prenom, email, mdp , phone ,  idFormation  ) values ('{input_Lname.Text}','{input_name.Text}','{input_mail.Text}','{input_mdp.Text}','{input_phone.Text}',{int.Parse(idform.Text)})";
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                MessageBox.Show("records has been Added");
                loaddata();
            }
        }


        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            connection.Close();
            connection.ConnectionString = connect;
            connection.Open();

            string query = $"DELETE FROM  Apprenant WHERE idApprenant = '{int.Parse(id_delete.Text)}'";
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
            MessageBox.Show("records has been deleted");
            loaddata();
            connection.Close();




        }

        private void btn_edit_Click(object sender, RoutedEventArgs e)
        {
            if (AreFieldsValid())
            {
                connection.Close();
                connection.ConnectionString = connect;
                connection.Open();
                string query = $"UPDATE Apprenant SET prenom='{input_name.Text}' , n m='{input_Lname.Text}'  , email='{input_mail.Text}' , mdp='{input_mdp.Text}' , idFormation={int.Parse(idform.Text)}   WHERE idApprenant={Int32.Parse(id_edit.Text)}";
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                loaddata();
            }
        }
        private void loaddata()
        {

            connection.Close();
            connection.ConnectionString = connect;
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM Apprenant", connection);
            command.ExecuteNonQuery();
            SqlDataReader dr = command.ExecuteReader();
            DG_Zone.ItemsSource = dr;



        }

        private bool AreFieldsValid()
        {
            errors = new StringBuilder();

            //Validate First Name and Last Name
            validator = new Regex(@"^([A-Z][a-z]+)(s[A-Z][a-z]+)*$");

            if (!validator.Match(input_name.Text).Success)
                errors.AppendLine("First name is not in the proper format.");

            if (!validator.Match(input_Lname.Text).Success)
                errors.AppendLine("Last name is not in the proper format.");

            //Validate Age
            validator = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");

            if (!validator.IsMatch(input_mail.Text))
                errors.AppendLine("Invalid Email.");

            validator = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");

            if (!validator.IsMatch(input_mdp.Text))
                errors.AppendLine("Invalid Password.");


            validator = new Regex(@"^[0-9]{10}");

            if (!validator.IsMatch(input_phone.Text))
                errors.AppendLine("Invalid phone.");

            validator = new Regex(@"^[0-4]{1}$");

            if (!validator.IsMatch(idform.Text))
                errors.AppendLine("Invalid Formation ID.");


            if (errors.ToString() == String.Empty)
            {
                return true;
            }
            else
            {
                MessageBox.Show(errors.ToString(), "Validation Failed");

                return false;
            }
        }

    }

}