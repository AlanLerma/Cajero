﻿using Newtonsoft.Json;
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
    /// Lógica de interacción para Balance.xaml
    /// </summary>
    /// 
    
    public partial class Balance : Window
    {
        SqlConnection conection = new SqlConnection("server=DESKTOP-GS3N4RQ\\SQLEXPRESS; database=JonWick; user = DESKTOP-GS3N4RQ\\ALA_A; integrated security = true");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;

        public Balance()
        {
            InitializeComponent();

            var client = new RestClient("http://linkxenter.com:3000/");
            var request = new RestRequest("account_balance", Method.GET);
            request.AddParameter("token", "dfb11a11722164a4e98c2fdb86c48343");
            request.AddParameter("account", VariablesGlobales.NumCuenta);
            var content = client.Execute(request).Content;

            var jobj = (JObject)JsonConvert.DeserializeObject(content.ToString());
            string user = jobj.SelectToken("user").ToString();
            string debt = jobj.SelectToken("debt").ToString();
            VariablesGlobales.usuario = user;

            cuenta_blue.Content = VariablesGlobales.NumCuenta;
            deuda_blue.Content = "$" + debt;
            usuario_blue.Content = user;
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow subwindow = new MainWindow();
            subwindow.Show();
            this.Close();
        }

        private void Pagar_Click(object sender, RoutedEventArgs e)
        {

         
           

            Payment subwindow = new Payment();
            subwindow.Show();
            this.Close();
        }
    }
}



//basura
//cmd.CommandText = "select Customer, Debt from cliente where Account = " + VariablesGlobales.NumCuenta;
//cmd.CommandType = System.Data.CommandType.Text;
//cmd.Connection = conection;

//conection.Open();
//reader = cmd.ExecuteReader();

//if (reader.Read()) {
//    string cliente = reader["Customer"].ToString();
//    string deuda = reader["Debt"].ToString();

//}
//conection.Close();