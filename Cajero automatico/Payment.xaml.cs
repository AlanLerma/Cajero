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
    /// Lógica de interacción para Payment.xaml
    /// </summary>
    public partial class Payment : Window
    {
        SqlConnection conection = new SqlConnection("server=DESKTOP-GS3N4RQ\\SQLEXPRESS; database=JonWick; user = DESKTOP-GS3N4RQ\\ALA_A; integrated security = true");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        public Payment()
        {

            InitializeComponent();
            VariablesGlobales.ingreso = 0.0;
            Depositado_blue.Content = VariablesGlobales.ingreso;


            cmd.CommandText = "select Debt from cliente where Account = " + VariablesGlobales.NumCuenta;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = conection;

            conection.Open();
            reader = cmd.ExecuteReader();

            if (reader.Read())
            {

                VariablesGlobales.deuda = Double.Parse(reader["Debt"].ToString());
                Deuda_blue.Content = VariablesGlobales.deuda;

            }
            conection.Close();

            Restante_blue.Content = VariablesGlobales.deuda;

        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            

            cmd.CommandText = @"UPDATE cliente
                                SET Debt = @deuda, Paid = @pagado, Datee = GETDATE()
                                WHERE account = @cuenta";

            cmd.CommandType = System.Data.CommandType.Text;
            double resta = VariablesGlobales.deuda - VariablesGlobales.ingreso;
            if (resta < 0)
            {
                resta = 0;
            }

            cmd.Parameters.AddWithValue("@deuda", resta);
            cmd.Parameters.AddWithValue("@pagado", VariablesGlobales.ingreso);
            cmd.Parameters.AddWithValue("@cuenta", VariablesGlobales.NumCuenta);

            cmd.Connection = conection;



            conection.Open();
            reader = cmd.ExecuteReader();
            cmd.Parameters.Clear();
            conection.Close();

            ServiceReference.Service1Client Client = new ServiceReference.Service1Client();
            Client.Open();


            MessageBox.Show("Pago realizado con exito");
            MainWindow subwindow = new MainWindow();
            //System.Diagnostics.Process.Start("http://localhost:53756/Service1.svc");
            //System.Diagnostics.Process.Start("https://localhost:44349/");
        




            subwindow.Show();


                
            this.Close();


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            VariablesGlobales.ingreso = VariablesGlobales.ingreso + 500;
            Depositado_blue.Content = VariablesGlobales.ingreso;


            if (VariablesGlobales.deuda - VariablesGlobales.ingreso < 0)
            {
                Restante_blue.Content = 0;
            }
            else
            {
                Restante_blue.Content = VariablesGlobales.deuda - VariablesGlobales.ingreso;
            }
            


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            VariablesGlobales.ingreso = VariablesGlobales.ingreso + 10;
            Depositado_blue.Content = VariablesGlobales.ingreso;

            if (VariablesGlobales.deuda - VariablesGlobales.ingreso < 0)
            {
                Restante_blue.Content = 0;
            }
            else
            {
                Restante_blue.Content = VariablesGlobales.deuda - VariablesGlobales.ingreso;
            }
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            VariablesGlobales.ingreso = VariablesGlobales.ingreso + 200;
            Depositado_blue.Content = VariablesGlobales.ingreso;

            if (VariablesGlobales.deuda - VariablesGlobales.ingreso < 0)
            {
                Restante_blue.Content = 0;
            }
            else
            {
                Restante_blue.Content = VariablesGlobales.deuda - VariablesGlobales.ingreso;
            }
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            VariablesGlobales.ingreso = VariablesGlobales.ingreso + 100;
            Depositado_blue.Content = VariablesGlobales.ingreso;

            if (VariablesGlobales.deuda - VariablesGlobales.ingreso < 0)
            {
                Restante_blue.Content = 0;
            }
            else
            {
                Restante_blue.Content = VariablesGlobales.deuda - VariablesGlobales.ingreso;
            }
        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            VariablesGlobales.ingreso = VariablesGlobales.ingreso + 50;
            Depositado_blue.Content = VariablesGlobales.ingreso;

            if (VariablesGlobales.deuda - VariablesGlobales.ingreso < 0)
            {
                Restante_blue.Content = 0;
            }
            else
            {
                Restante_blue.Content = VariablesGlobales.deuda - VariablesGlobales.ingreso;
            }
        }

        private void Button_Click4(object sender, RoutedEventArgs e)
        {
            VariablesGlobales.ingreso = VariablesGlobales.ingreso + 20;
            Depositado_blue.Content = VariablesGlobales.ingreso;

            if (VariablesGlobales.deuda - VariablesGlobales.ingreso < 0)
            {
                Restante_blue.Content = 0;
            }
            else
            {
                Restante_blue.Content = VariablesGlobales.deuda - VariablesGlobales.ingreso;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            VariablesGlobales.ingreso = VariablesGlobales.ingreso + 5;
            Depositado_blue.Content = VariablesGlobales.ingreso;

            if (VariablesGlobales.deuda - VariablesGlobales.ingreso < 0)
            {
                Restante_blue.Content = 0;
            }
            else
            {
                Restante_blue.Content = VariablesGlobales.deuda - VariablesGlobales.ingreso;
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            VariablesGlobales.ingreso = VariablesGlobales.ingreso + 2;
            Depositado_blue.Content = VariablesGlobales.ingreso;

            if (VariablesGlobales.deuda - VariablesGlobales.ingreso < 0)
            {
                Restante_blue.Content = 0;
            }
            else
            {
                Restante_blue.Content = VariablesGlobales.deuda - VariablesGlobales.ingreso;
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            VariablesGlobales.ingreso = VariablesGlobales.ingreso + 1;
            Depositado_blue.Content = VariablesGlobales.ingreso;

            if (VariablesGlobales.deuda - VariablesGlobales.ingreso < 0)
            {
                Restante_blue.Content = 0;
            }
            else
            {
                Restante_blue.Content = VariablesGlobales.deuda - VariablesGlobales.ingreso;
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            VariablesGlobales.ingreso = VariablesGlobales.ingreso + .50;
            Depositado_blue.Content = VariablesGlobales.ingreso;

            if (VariablesGlobales.deuda - VariablesGlobales.ingreso < 0)
            {
                Restante_blue.Content = 0;
            }
            else
            {
                Restante_blue.Content = VariablesGlobales.deuda - VariablesGlobales.ingreso;
            }
        }
    }
}
