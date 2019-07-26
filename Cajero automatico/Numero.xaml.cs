using RestSharp;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Cajero_automatico
{
    /// <summary>
    /// Lógica de interacción para Numero.xaml
    /// </summary>
    public partial class Numero : Window
    {
        SqlConnection conection = new SqlConnection("server=DESKTOP-GS3N4RQ\\SQLEXPRESS; database=JonWick; user = DESKTOP-GS3N4RQ\\ALA_A; integrated security = true");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;

        public Numero()
        {
            InitializeComponent();
            VariablesGlobales.NumCuenta = "None";
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow subwindow = new MainWindow();
            subwindow.Show();
            this.Close();
        }

        private void Uno_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.Text.Length < 10)
            {
                textBox.Text = textBox.Text + "1";
                VariablesGlobales.NumCuenta = textBox.Text;
            }
            
        }

        private void Dos_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.Text.Length < 10)
            {
                textBox.Text = textBox.Text + "2";
                VariablesGlobales.NumCuenta = textBox.Text;
            }
        }

        private void Tres_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.Text.Length < 10)
            {
                textBox.Text = textBox.Text + "3";
                VariablesGlobales.NumCuenta = textBox.Text;
            }
        }

        private void Cuatro_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.Text.Length < 10)
            {
                textBox.Text = textBox.Text + "4";
                VariablesGlobales.NumCuenta = textBox.Text;
            }
        }

        private void Cinco_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.Text.Length < 10)
            {
                textBox.Text = textBox.Text + "5";
                VariablesGlobales.NumCuenta = textBox.Text;
            }
        }

        private void Seis_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.Text.Length < 10)
            {
                textBox.Text = textBox.Text + "6";
                VariablesGlobales.NumCuenta = textBox.Text;
            }
        }

        private void Siete_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.Text.Length < 10)
            {
                textBox.Text = textBox.Text + "7";
                VariablesGlobales.NumCuenta = textBox.Text;
            }
        }

        private void Ocho_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.Text.Length < 10)
            {
                textBox.Text = textBox.Text + "8";
                VariablesGlobales.NumCuenta = textBox.Text;
            }
        }

        private void Nueve_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.Text.Length < 10)
            {
                textBox.Text = textBox.Text + "9";
                VariablesGlobales.NumCuenta = textBox.Text;
            }
        }

        private void Cero_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.Text.Length < 10)
            {
                textBox.Text = textBox.Text + "0";
                VariablesGlobales.NumCuenta = textBox.Text;
            }
        }

        private void Continuar_Click(object sender, RoutedEventArgs e)
        {
            VariablesGlobales.NumCuenta = textBox.Text;

            //  GET 
           
               var client = new RestClient("http://linkxenter.com:3000/");
               var request = new RestRequest("account_balance", Method.GET);
               request.AddParameter("token", "dfb11a11722164a4e98c2fdb86c48343");
               request.AddParameter("account", VariablesGlobales.NumCuenta);
               var content = client.Execute(request).Content;

            if (content.ToString() == "{'message': 'no existen datos del usuario.'}")
            {
                MessageBox.Show("usuario invalido");
                textBox.Text = "";
            }
            else
            {
                Balance subwindow = new Balance();
                subwindow.Show();
                this.Close();
            }






        }
    }
}





//String codigo = "";

//    cmd.CommandText = "select ID from cliente where Account = @Cuenta";
//    cmd.Parameters.AddWithValue("@Cuenta", VariablesGlobales.NumCuenta);
//    cmd.CommandType = System.Data.CommandType.Text;
//    cmd.Connection = conection;

//    conection.Open();
//    reader = cmd.ExecuteReader();

//    if (reader.Read())
//    {
//        codigo = reader["ID"].ToString();
//    }
//    cmd.Parameters.Clear();
//    conection.Close();





//if (codigo == null || codigo == "" || codigo == "None")
//{
//    MessageBox.Show("El número de cuenta no existe");
//}
//else {
//    Balance subwindow = new Balance();
//    subwindow.Show();
//    this.Close();
//}