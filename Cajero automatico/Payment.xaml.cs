using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    /// Lógica de interacción para Payment.xaml
    /// </summary>
    public partial class Payment : Window
    {
        SqlConnection conection = new SqlConnection("server=DESKTOP-GS3N4RQ\\SQLEXPRESS; database=JonWick; user = DESKTOP-GS3N4RQ\\ALA_A; integrated security = true");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        
        DeviceLibrary.DeviceLibrary dv = new DeviceLibrary.DeviceLibrary();


        DeviceLibrary.Models.Document centavo = new DeviceLibrary.Models.Document((decimal)(0.5), DeviceLibrary.Models.Enums.DocumentType.Coin, 100+ VariablesGlobales.c_centavo);
        DeviceLibrary.Models.Document uno = new DeviceLibrary.Models.Document(1, DeviceLibrary.Models.Enums.DocumentType.Coin, 100 + VariablesGlobales.c_uno);
        DeviceLibrary.Models.Document dos = new DeviceLibrary.Models.Document(2, DeviceLibrary.Models.Enums.DocumentType.Coin, 100+VariablesGlobales.c_dos);
        DeviceLibrary.Models.Document cinco = new DeviceLibrary.Models.Document(5, DeviceLibrary.Models.Enums.DocumentType.Coin, 100+VariablesGlobales.c_cinco);
        DeviceLibrary.Models.Document diez = new DeviceLibrary.Models.Document(10, DeviceLibrary.Models.Enums.DocumentType.Coin, 100+ VariablesGlobales.c_diez);

        DeviceLibrary.Models.Document veinte = new DeviceLibrary.Models.Document(20, DeviceLibrary.Models.Enums.DocumentType.Bill, 100+ VariablesGlobales.c_veinte);
        DeviceLibrary.Models.Document cincuenta = new DeviceLibrary.Models.Document(50, DeviceLibrary.Models.Enums.DocumentType.Bill, 100+ VariablesGlobales.c_cincuenta);
        DeviceLibrary.Models.Document doscientos = new DeviceLibrary.Models.Document(200, DeviceLibrary.Models.Enums.DocumentType.Bill, 100+ VariablesGlobales.c_doscientos);
        DeviceLibrary.Models.Document cienb = new DeviceLibrary.Models.Document(100, DeviceLibrary.Models.Enums.DocumentType.Bill, 100+ VariablesGlobales.c_cien);
        DeviceLibrary.Models.Document quinentos = new DeviceLibrary.Models.Document(500, DeviceLibrary.Models.Enums.DocumentType.Bill, 100+VariablesGlobales.c_quinientos);

        

        public Payment()
        {
            dv.Open();
            VariablesGlobales.c_centavo = 0;
            VariablesGlobales.c_centavo = 0;
            VariablesGlobales.c_dos =0;
           VariablesGlobales.c_cinco = 0;
            VariablesGlobales.c_diez = 0;

            VariablesGlobales.c_veinte = 0;
            VariablesGlobales.c_cincuenta = 0;
            VariablesGlobales.c_doscientos = 0;
            VariablesGlobales.c_cien = 0;
            VariablesGlobales.c_quinientos = 0;


            InitializeComponent();
            VariablesGlobales.ingreso = 0.0;

            var client = new RestClient("http://linkxenter.com:3000/");
            var request = new RestRequest("account_balance", Method.GET);
            request.AddParameter("token", "dfb11a11722164a4e98c2fdb86c48343");
            request.AddParameter("account", VariablesGlobales.NumCuenta);
            var content = client.Execute(request).Content;

            var jobj = (JObject)JsonConvert.DeserializeObject(content.ToString());
            string debt = jobj.SelectToken("debt").ToString();

            Deuda_blue.Content = "$" + debt;
            VariablesGlobales.ingreso = 0;
            Depositado_blue.Content = "$" + VariablesGlobales.ingreso;
            VariablesGlobales.deuda = Double.Parse(debt);
            VariablesGlobales.restante = VariablesGlobales.deuda - VariablesGlobales.ingreso;

            dv.AcceptedDocument += Dv_AcceptedDocument;
        }

        

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {

            cmd.CommandText = @" INSERT INTO cliente (Customer, Account, Debt, Paid, Datee)
            VALUES  (@usuario, @cuenta, @deuda, @pagado, GETDATE())";


            cmd.CommandType = System.Data.CommandType.Text;
         

            cmd.Parameters.AddWithValue("@deuda", VariablesGlobales.restante);

            if (VariablesGlobales.deuda > VariablesGlobales.ingreso)
            {
                cmd.Parameters.AddWithValue("@pagado", VariablesGlobales.ingreso);
            }
            else
            {
                cmd.Parameters.AddWithValue("@pagado", VariablesGlobales.deuda);
            }    
            cmd.Parameters.AddWithValue("@cuenta", VariablesGlobales.NumCuenta);
            cmd.Parameters.AddWithValue("@usuario", VariablesGlobales.usuario);


            cmd.Connection = conection;
            conection.Open();
            reader = cmd.ExecuteReader();
            cmd.Parameters.Clear();
            conection.Close();

            ServiceReference.Service1Client Client = new ServiceReference.Service1Client();
            Client.Open();

            //POST
            var client = new RestClient("http://linkxenter.com:3000/transaction?token=dfb11a11722164a4e98c2fdb86c48343");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Content-Length", "51");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Host", "linkxenter.com:3000");
            request.AddHeader("Postman-Token", "745a33dd-f50b-4066-bd02-71198ee4740f,a7eb45a4-3fc6-4ae8-a59b-b332522a3ee6");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("User-Agent", "PostmanRuntime/7.15.2");
            request.AddHeader("Content-Type", "application/json");

            if (VariablesGlobales.deuda > VariablesGlobales.ingreso)
            {
              request.AddParameter("undefined", "{\r\n    \"account\": \"" + VariablesGlobales.NumCuenta + "\",\r\n    \"paid\": " + VariablesGlobales.ingreso + "\r\n}\r\n", ParameterType.RequestBody);
            }
            else
            {
              request.AddParameter("undefined", "{\r\n    \"account\": \"" + VariablesGlobales.NumCuenta + "\",\r\n    \"paid\": " + VariablesGlobales.deuda + "\r\n}\r\n", ParameterType.RequestBody);
            }
            
            IRestResponse response = client.Execute(request);


            if (VariablesGlobales.restante <= 0)
            {
                dv.Dispense((decimal)(VariablesGlobales.restante) * -1);
                MessageBox.Show("Pago realizado con exito, su cambio es de: $"+ VariablesGlobales.restante*-1);

            }
            else
            {
                MessageBox.Show("Pago realizado con exito");
            }

            MainWindow subwindow = new MainWindow();
            subwindow.Show();
            dv.Disable();
            dv.Close();
            this.Close();


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            VariablesGlobales.c_quinientos += 1;
            dv.Enable();
            dv.SimulateInsertion(quinentos);
           
     
        }

        private void Dv_AcceptedDocument(DeviceLibrary.Models.Document obj)
        {
            double suma = 
            VariablesGlobales.c_centavo*0.5+
            VariablesGlobales.c_uno +
            VariablesGlobales.c_dos*2 +
            VariablesGlobales.c_cinco*5 +
            VariablesGlobales.c_diez*10 +
            VariablesGlobales.c_veinte*20 +
            VariablesGlobales.c_cincuenta*50+ 
            VariablesGlobales.c_doscientos*200 +
            VariablesGlobales.c_cien*100 +
            VariablesGlobales.c_quinientos*500;
            VariablesGlobales.ingreso = suma;
            
            Depositado_blue.Content = "$"+VariablesGlobales.ingreso;
            VariablesGlobales.restante = VariablesGlobales.deuda - VariablesGlobales.ingreso;
            if (VariablesGlobales.restante <= 0)
            {
                Restante_blue.Content = "$"+0;
            }
            else
            {
                Restante_blue.Content = "$"+ VariablesGlobales.restante;
            }
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            VariablesGlobales.c_diez += 1;
            dv.Enable();
            dv.SimulateInsertion(diez);
            

        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            VariablesGlobales.c_doscientos += 1;
            dv.Enable();
            dv.SimulateInsertion(doscientos);
           
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            VariablesGlobales.c_cien += 1;
            dv.Enable();
            dv.SimulateInsertion(cienb);
          

        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            VariablesGlobales.c_cincuenta += 1;
            dv.Enable();
            dv.SimulateInsertion(cincuenta);
           

        }

        private void Button_Click4(object sender, RoutedEventArgs e)
        {
            VariablesGlobales.c_veinte += 1;
            dv.Enable();
            dv.SimulateInsertion(veinte);
            

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            VariablesGlobales.c_cinco += 1;
            dv.Enable();
            dv.SimulateInsertion(cinco);
           
            
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            VariablesGlobales.c_dos += 1;
            dv.Enable();
            dv.SimulateInsertion(dos);
          

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            VariablesGlobales.c_uno += 1;
            dv.Enable();
            dv.SimulateInsertion(uno);
           
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            VariablesGlobales.c_centavo += 1;
            dv.Enable();
            dv.SimulateInsertion(centavo);
            

        }

        private void Realcancelar_Click(object sender, RoutedEventArgs e)
        {
            if (VariablesGlobales.ingreso > 0 )
            {
                MessageBox.Show("Ingresaste dinero, no puedes volver");
            }
            else
            {
                MainWindow subwindow = new MainWindow();
                subwindow.Show();
                this.Close();
            }
        }
    }
}
